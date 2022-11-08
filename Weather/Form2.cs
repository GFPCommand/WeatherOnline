using System.Windows.Forms;

namespace Weather
{
    public partial class SettingsForm : Form
    {
        private Location _location;

        public SettingsForm()
        {
            InitializeComponent();
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
            Settings.s_SelectedLocation = cities.SelectedItem.ToString();
        }

        private void temperatureUnits_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Settings.isCelsius = (string)temperatureUnits.SelectedItem == "Celsius";
        }

        private void windUnits_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Settings.isMetersSeconds = (string)windUnits.SelectedItem == "meters";
        }
    }
}
