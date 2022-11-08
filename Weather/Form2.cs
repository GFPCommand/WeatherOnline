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
                comboBox1.Items.Add(Properties.Settings.Default.cb);
            }

            if (Settings.s_SelectedLocation.Equals("")) comboBox1.SelectedIndex = 0;

            comboBox1.SelectedItem = Settings.s_SelectedLocation;

            if (Settings.isCelsius) temperatureUnits.SelectedItem = "Celsius";
            else temperatureUnits.SelectedItem = "Fahrenheit";

            if (Settings.isMetersSeconds) windUnits.SelectedItem = "meters";
            else windUnits.SelectedItem = "miles";
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            _location.Find(comboBox1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Settings.s_SelectedLocation = comboBox1.SelectedItem.ToString();
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
