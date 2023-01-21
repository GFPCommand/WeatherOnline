using System;
using System.Windows.Forms;
using System.Xml;
using static Weather.Settings;

namespace Weather
{
    class WeatherManager
    {
        private const string API_KEY = "309e5a304823303c3c0c574342a743aa";
        private readonly string CommonWeekLink = $"https://client.meteoservice.ru/export/daily/{API_KEY}/point/";
        private readonly string CommonCurrentLink = $"https://client.meteoservice.ru/export/current/{API_KEY}/point/";

        private const string VLG_LINK_XML_ID = "5358.xml";
        private const string LED_LINK_XML_ID = "5357.xml";
        private const string AER_LINK_XML_ID = "5356.xml";
        private const string MOW_LINK_XML_ID = "5355.xml";

        private const int DAYS = 7;

        private string _link = "";

        private XmlDocument _doc = new XmlDocument();

        private const float MetersToMilesCoefficient = 0.44704f;

        private const string North = "N";
        private const string South = "S";
        private const string West = "W";
        private const string East = "E";

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

        private string _weatherType;

        private string temperature_symbol = s_TempSymbol;
        private string wind_symbol = s_WindSymbol;

        public WeatherManager() 
        {
            Temperature = "";
            Feel = "";
            Wind = "";
            Humidity = "";
            Pressure = "";
            WeatherType = "";

            for (int i = 0; i < DAYS; i++)
            {
                WeekTemperature[i] = "";
                WeekFeel[i] = "";
                WeekWind[i] = "";
                WeekHumidity[i] = "";
                WeekPressure[i] = "";
                WeekWeatherType[i] = "";
            }
        }

        public string Temperature { get; private set; }
        public string Feel { get; private set; }
        public string Wind { get; private set; }
        public string Humidity { get; private set; }
        public string Pressure { get; private set; }
        public string WeatherType { get; private set; }

        public string[] WeekTemperature { get; private set; } = new string[DAYS];
        public string[] WeekFeel { get; private set; } = new string[DAYS];
        public string[] WeekWind { get; private set; } = new string[DAYS];
        public string[] WeekHumidity { get; private set; } = new string[DAYS];
        public string[] WeekPressure { get; private set; } = new string[DAYS];
        public string[] WeekWeatherType { get; private set; } = new string[DAYS];

        public void GetWeekWeatherFromServer()
        {
            SetCityLink(WeatherWindowState.Week);

            try
            {
                XmlElement root = _doc.DocumentElement;

                if (root != null)
                {
                    XmlWeekWeatherParser(root, DAYS, out int deg);

                    SetWindDirection(deg);
                }
            } catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void GetCurrentWeatherFromServer()
        {
            float tmpTemperature = 0;
            float tmpWindSpeed = 0;

            int deg = 0;

            double feel = 0;

            SetCityLink(WeatherWindowState.Current);

            try
            {
                XmlElement root = _doc.DocumentElement;

                if (root != null)
                {
                    foreach (XmlNode item in root)
                    {
                        if (item.Name.Equals("weather"))
                        {
                            foreach (XmlNode subitem in item.ChildNodes[0])
                            {
                                switch (subitem.Name)
                                {
                                    case "temperature":
                                        tmpTemperature = float.Parse(subitem.ChildNodes[0].InnerText.Replace('.', ','));
                                        Temperature = $"{Math.Round(tmpTemperature)}{temperature_symbol}";
                                        break;
                                    case "wind_speed":
                                        tmpWindSpeed = float.Parse(subitem.ChildNodes[0].InnerText.Replace('.', ','));
                                        break;
                                    case "pressure":
                                        Pressure = $"{Convert.ToInt32(subitem.ChildNodes[0].InnerText)} mmHg";
                                        break;
                                    case "humidity":
                                        Humidity = $"{Convert.ToInt32(subitem.ChildNodes[0].InnerText)} %";
                                        break;
                                    case "cloudness_str":
                                        WeatherType = subitem.InnerText;
                                        break;
                                    case "wind_direction_degr":
                                        deg = int.Parse(subitem.ChildNodes[0].InnerText);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }

                feel = 13.12f + 0.6215f * tmpTemperature - 11.37f * Math.Pow(1.5f * tmpWindSpeed, 0.16f) + 0.3965 * tmpTemperature * Math.Pow(1.5f * tmpWindSpeed, 0.16f);

                Feel = $"{Math.Round(feel)}{temperature_symbol}";

                SetWindDirection(deg);

                Wind = $"{tmpWindSpeed} {wind_symbol} {_windDirection}";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XmlWeekWeatherParser(XmlElement root, short daysCount, out int deg)
        {
            int k = daysCount == 1 ? -2 : 0;
            int maxVal = daysCount == 1 ? 1 : daysCount * 2;

            deg = 0;

            for (int i = 0; i < daysCount; i++)
            {
                for (int j = k + 2; j < maxVal; j++)
                {
                    foreach (XmlNode item in root)
                    {
                        if (item.Name.Equals("forecast"))
                        {
                            foreach (XmlNode subitem in item.ChildNodes[i * 2])
                            {
                                switch (subitem.Name)
                                {
                                    case "min_temperature":
                                        _minTemp = float.Parse(subitem.ChildNodes[0].InnerText.Replace('.', ','));
                                        break;
                                    case "max_temperature":
                                        _maxTemp = float.Parse(subitem.ChildNodes[0].InnerText.Replace('.', ','));
                                        break;
                                    case "min_wind_speed":
                                        _minWindSpeed = float.Parse(subitem.ChildNodes[0].InnerText.Replace('.', ','));
                                        break;
                                    case "max_wind_speed":
                                        _maxWindSpeed = float.Parse(subitem.ChildNodes[0].InnerText.Replace('.', ','));
                                        break;
                                    case "min_pressure":
                                        _minPressure = Convert.ToInt32(subitem.ChildNodes[0].Value);
                                        break;
                                    case "max_pressure":
                                        _maxPressure = Convert.ToInt32(subitem.ChildNodes[0].Value);
                                        break;
                                    case "min_humidity":
                                        _minHumidity = Convert.ToInt32(subitem.ChildNodes[0].Value);
                                        break;
                                    case "max_humidity":
                                        _maxHumidity = Convert.ToInt32(subitem.ChildNodes[0].Value);
                                        break;
                                    case "min_feeled_temperature":
                                        _minFeelTemp = float.Parse(subitem.ChildNodes[0].InnerText.Replace('.', ','));
                                        break;
                                    case "max_feeled_temperature":
                                        _maxFeelTemp = float.Parse(subitem.ChildNodes[0].InnerText.Replace('.', ','));
                                        break;
                                    case "text":
                                        _weatherType = subitem.ChildNodes[0].Value;
                                        break;
                                    case "wind_direction_degr":
                                        deg = Convert.ToInt32(subitem.ChildNodes[0].Value);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }

                if (!isCelsius)
                {
                    _minTemp = _minTemp * 1.8f + 32;
                    _maxTemp = _maxTemp * 1.8f + 32;

                    _minFeelTemp = _minTemp * 1.8f + 32;
                    _maxFeelTemp = _maxTemp * 1.8f + 32;
                }

                if (!isMetersSeconds)
                {
                    _minWindSpeed *= MetersToMilesCoefficient;
                    _maxWindSpeed *= MetersToMilesCoefficient;
                }

                SwitchMinMaxValues();

                WeekTemperature[i] = $"{Math.Round(_minTemp)}/{Math.Round(_maxTemp)}{temperature_symbol}".Replace(',', '.');
                WeekFeel[i] = $"{Math.Round(_minFeelTemp)}/{Math.Round(_maxFeelTemp)}{temperature_symbol}".Replace(',', '.');
                WeekWind[i] = $"{Math.Round(_minWindSpeed)}/{Math.Round(_maxWindSpeed)} {wind_symbol} {_windDirection}";
                WeekHumidity[i] = $"{_minHumidity}/{_maxHumidity} %";
                WeekPressure[i] = $"{_minPressure}/{_maxPressure} mmHg";
                WeekWeatherType[i] = _weatherType;
            }
        }

        private void SetCityLink(WeatherWindowState state)
        {
            switch (s_SelectedLocation)
            {
                case "Volgograd":
                    _link = state == WeatherWindowState.Week ? $"{CommonWeekLink}{VLG_LINK_XML_ID}" : $"{CommonCurrentLink}{VLG_LINK_XML_ID}";
                    break;
                case "Moscow":
                    _link = state == WeatherWindowState.Week ? $"{CommonWeekLink}{MOW_LINK_XML_ID}" : $"{CommonCurrentLink}{MOW_LINK_XML_ID}";
                    break;
                case "Saint-Petersburg":
                    _link = state == WeatherWindowState.Week ? $"{CommonWeekLink}{LED_LINK_XML_ID}" : $"{CommonCurrentLink}{LED_LINK_XML_ID}";
                    break;
                case "Sochi":
                    _link = state == WeatherWindowState.Week ? $"{CommonWeekLink}{AER_LINK_XML_ID}" : $"{CommonCurrentLink}{AER_LINK_XML_ID}";
                    break;
                default:
                    break;
            }

            _doc.Load(_link);
        }

        private void SetWindDirection(int deg)
        {
            switch (deg)
            {
                case 0:
                    _windDirection = North;
                    break;
                case 45:
                    _windDirection = North + East;
                    break;
                case 90:
                    _windDirection = East;
                    break;
                case 135:
                    _windDirection = South + East;
                    break;
                case 180:
                    _windDirection = South;
                    break;
                case 225:
                    _windDirection = South + West;
                    break;
                case 270:
                    _windDirection = West;
                    break;
                case 315:
                    _windDirection = North + West;
                    break;
                case 360:
                    _windDirection = North;
                    break;
                default:
                    if (deg > 0 && deg < 45)
                    {
                        _windDirection = North + North + East;
                    }
                    else if (deg > 45 && deg < 90)
                    {
                        _windDirection = East + North + East;
                    }
                    else if (deg > 90 && deg < 135)
                    {
                        _windDirection = East + South + East;
                    }
                    else if (deg > 135 && deg < 180)
                    {
                        _windDirection= South + South + East;
                    }
                    else if (deg > 180 && deg < 225)
                    {
                        _windDirection= South + South + West;
                    }
                    else if (deg > 225 && deg < 270)
                    {
                        _windDirection= West + South + West;
                    }
                    else if (deg > 270 && deg < 315)
                    {
                        _windDirection= West + North + West;
                    }
                    else if (deg > 315 && deg < 360)
                    {
                        _windDirection= North + North + West;
                    }
                    break;
            }
        }

        private void SwitchMinMaxValues()
        {
            (_minTemp, _maxTemp) = _minTemp > _maxTemp ? (_maxTemp, _minTemp) : (_minTemp, _maxTemp);
            (_minFeelTemp, _maxFeelTemp) = _minFeelTemp > _maxFeelTemp ? (_maxFeelTemp, _minFeelTemp) : (_minFeelTemp, _maxFeelTemp);
            (_minWindSpeed, _maxWindSpeed) = _minWindSpeed > _maxWindSpeed ? (_maxWindSpeed, _minWindSpeed) : (_minWindSpeed, _maxWindSpeed);
            (_minHumidity, _maxHumidity) = _minHumidity > _maxHumidity ? (_maxHumidity, _minHumidity) : (_minHumidity, _maxHumidity);
            (_minPressure, _maxPressure) = _minPressure > _maxPressure ? (_maxPressure, _minPressure) : (_minPressure, _maxPressure);
        }
    }
}
