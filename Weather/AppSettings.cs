namespace Weather
{
    static class Settings
    {
        public static string s_SelectedLocation = "Volgograd";
        public static bool isCelsius = true;
        public static bool isMetersSeconds = true;
        public static string s_TempSymbol = isCelsius ? "°C" : "°F";
        public static string s_WindSymbol = isMetersSeconds ? "m\\s" : "miles\\s";
    }
}
