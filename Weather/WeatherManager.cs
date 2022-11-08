using System.Data;

namespace Weather
{
    class WeatherManager
    {
        private string _commonLink = "https://client.meteoservice.ru/export/daily/309e5a304823303c3c0c574342a743aa/point/";
        private const string VLG_LINK_XML_ID = "5358.xml";
        private const string LED_LINK_XML_ID = "5357.xml";
        private const string AER_LINK_XML_ID = "5356.xml";
        private const string MOW_LINK_XML_ID = "5355.xml";

        public WeatherManager() { }

        public string Temperature { get; private set; }
        public string Feel { get; private set; }
        public string Wind { get; private set; }
        public string Humidity { get; private set; }
        public string Pressure { get; private set; }

        public void GetWeatherFromServer()
        {
            DataSet ds = new DataSet();

            switch (Settings.s_SelectedLocation)
            {
                case "Volgograd":
                    _commonLink += VLG_LINK_XML_ID;
                    break;
                case "Moscow":
                    _commonLink = MOW_LINK_XML_ID;
                    break;
                case "Saint-Petersburg":
                    _commonLink += LED_LINK_XML_ID;
                    break;
                case "Sochi":
                    _commonLink += AER_LINK_XML_ID;
                    break;
                default:
                    break;
            }

            ds.Clear();

            ds.ReadXml(_commonLink);

            Temperature = ds.Tables[2].Rows[0][3].ToString();
            Feel = ds.Tables[2].Rows[0][4].ToString();
            Wind = ds.Tables[2].Rows[0][5].ToString();
            Humidity = ds.Tables[2].Rows[0][6].ToString();
            Pressure = ds.Tables[2].Rows[0][7].ToString();
        }
    }
}
