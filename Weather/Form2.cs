using System.Windows.Forms;

namespace Weather
{
    public partial class SettingsForm : Form
    {
        private Location _location;

        private Form1 form;

        private string _cityLocal;

        private bool _isCelsiusLocal;
        private bool _isMetersLocal;

        private string _windSymbolLocal, _temperatureSymbolLocal;

        public SettingsForm(Form1 mainForm)
        {
            InitializeComponent();

            form = mainForm;
        }

        private void SettingsForm_Load(object sender, System.EventArgs e)
        {
            _location = new Location();
            if (!Properties.Settings.Default.cb.Equals(""))
            {
                cities.Items.Add(Properties.Settings.Default.cb);
            }

            if (Settings.s_SelectedLocation.Equals("")) cities.SelectedIndex = 0;

            cities.SelectedItem = Settings.s_SelectedLocation;

            if (Settings.isCelsius) temperatureUnits.SelectedItem = "Celsius";
            else temperatureUnits.SelectedItem = "Fahrenheit";

            if (Settings.isMetersSeconds) windUnits.SelectedItem = "meters";
            else windUnits.SelectedItem = "miles";
        }

        private void geo_Click(object sender, System.EventArgs e)
        {
            _location.Find(cities);
        }

        private void cities_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            _cityLocal = cities.SelectedItem.ToString();
        }

        private void temperatureUnits_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            _isCelsiusLocal = temperatureUnits.SelectedItem.Equals("Celsius");
            _temperatureSymbolLocal = _isCelsiusLocal ? "°C" : "°F";
        }

        private void windUnits_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            _isMetersLocal = windUnits.SelectedItem.Equals("meters");
            _windSymbolLocal = _isMetersLocal ? "m\\s" : "mph";
        }

        private void saveButton_Click(object sender, System.EventArgs e)
        {
            Settings.isCelsius = _isCelsiusLocal;
            Settings.isMetersSeconds = _isMetersLocal;

            Settings.s_SelectedLocation = _cityLocal;

            Settings.s_TempSymbol = _temperatureSymbolLocal;
            Settings.s_WindSymbol = _windSymbolLocal;

            if (form.State == (int)Settings.WeatherWindowState.Week)
                form.SetWeekWeather();
            else form.SetCurrentWeather();

            Close();
        }

        private void cancelButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
