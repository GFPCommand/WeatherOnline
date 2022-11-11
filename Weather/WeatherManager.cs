using System;
using System.Data;
using System.Windows.Forms;

namespace Weather
{
    class WeatherManager
    {
        private const string CommonLink = "https://client.meteoservice.ru/export/daily/309e5a304823303c3c0c574342a743aa/point/";
        private const string VLG_LINK_XML_ID = "5358.xml";
        private const string LED_LINK_XML_ID = "5357.xml";
        private const string AER_LINK_XML_ID = "5356.xml";
        private const string MOW_LINK_XML_ID = "5355.xml";

        private string result = "";

        private const float MetersToMilesCoefficient = 0.000621371192237334f;

        private const string N = "N";
        private const string S = "S";
        private const string W = "W";
        private const string E = "E";

        private string _windDirection;

        private float _minTemp;
        private float _maxTemp;

        private float _minFeelTemp;
        private float _maxFeelTemp;

        private float _minWindSpeed;
        private float _maxWindSpeed;

        private int _minHumidity;
        private int _maxHumidity;

        private int _minPressure;
        private int _maxPressure;

        DataSet ds;

        public WeatherManager() { }

        public string Temperature { get; private set; }
        public string Feel { get; private set; }
        public string Wind { get; private set; }
        public string Humidity { get; private set; }
        public string Pressure { get; private set; }
        public string WeatherType { get; private set; }

        public void GetWeatherFromServer()
        {
            string temperature_symbol = Settings.s_TempSymbol;
            string wind_symbol = Settings.s_WindSymbol;

            switch (Settings.s_SelectedLocation)
            {
                case "Volgograd":
                    result = $"{CommonLink}{VLG_LINK_XML_ID}";
                    break;
                case "Moscow":
                    result = $"{CommonLink}{MOW_LINK_XML_ID}";
                    break;
                case "Saint-Petersburg":
                    result = $"{CommonLink}{LED_LINK_XML_ID}";
                    break;
                case "Sochi":
                    result = $"{CommonLink}{AER_LINK_XML_ID}";
                    break;
                default:
                    break;
            }

            try
            {
                ds = new DataSet();
                ds.ReadXml(result);

                switch (int.Parse(ds.Tables[2].Rows[0][8].ToString().Replace('.', ',')))
                {
                    case 0:
                        _windDirection = N;
                        break;
                    case 45:
                        _windDirection = N + E;
                        break;
                    case 90:
                        _windDirection = E;
                        break;
                    case 135:
                        _windDirection = S + E;
                        break;
                    case 180:
                        _windDirection = S;
                        break;
                    case 225:
                        _windDirection = S + W;
                        break;
                    case 270:
                        _windDirection = W;
                        break;
                    case 315:
                        _windDirection = N + W;
                        break;
                    case 360:
                        _windDirection = N;
                        break;
                    default:
                        break;
                }

                _minTemp = float.Parse(ds.Tables[2].Rows[0][3].ToString().Replace('.', ','));
                _maxTemp = float.Parse(ds.Tables[2].Rows[1][4].ToString().Replace('.', ','));

                if (!Settings.isCelsius)
                {
                    _minTemp = _minTemp * 1.8f + 32;
                    _maxTemp = _maxTemp * 1.8f + 32;
                }

                _minFeelTemp = float.Parse(ds.Tables[2].Rows[0][16].ToString().Replace('.', ','));
                _maxFeelTemp = float.Parse(ds.Tables[2].Rows[1][17].ToString().Replace('.', ','));

                if (!Settings.isCelsius)
                {
                    _minFeelTemp = _minTemp * 1.8f + 32;
                    _maxFeelTemp = _maxTemp * 1.8f + 32;
                }

                _minWindSpeed = float.Parse(ds.Tables[2].Rows[0][5].ToString().Replace('.', ','));
                _maxWindSpeed = float.Parse(ds.Tables[2].Rows[1][6].ToString().Replace('.', ','));

                if (!Settings.isMetersSeconds)
                {
                    _minWindSpeed = _minWindSpeed * MetersToMilesCoefficient;
                    _maxWindSpeed = _maxWindSpeed * MetersToMilesCoefficient;
                }

                _minHumidity = int.Parse(ds.Tables[2].Rows[0][14].ToString().Replace('.', ','));
                _maxHumidity = int.Parse(ds.Tables[2].Rows[1][15].ToString().Replace('.', ','));

                _minPressure = int.Parse(ds.Tables[2].Rows[0][12].ToString().Replace('.',','));
                _maxPressure = int.Parse(ds.Tables[2].Rows[1][13].ToString().Replace('.',','));

                Temperature = $"{Math.Round(_minTemp)}/{Math.Round(_maxTemp)}{temperature_symbol}".Replace(',', '.');
                Feel = $"{Math.Round(_minFeelTemp)}/{Math.Round(_maxFeelTemp)}{temperature_symbol}".Replace(',', '.');
                Wind = $"{Math.Round(_minWindSpeed)}/{Math.Round(_maxWindSpeed)} {wind_symbol} {_windDirection}";
                Humidity = $"{_minHumidity}/{_maxHumidity} %";
                Pressure = $"{_minPressure}/{_maxPressure} mmHg";
                WeatherType = ds.Tables[2].Rows[0][20].ToString();
            } catch (Exception e) {
                MessageBox.Show(e.Message);
            }
        }
    }
}
