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

            comboBox1.SelectedItem = LocationSettings.s_SelectedLocation;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            _location.Find(comboBox1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            LocationSettings.s_SelectedLocation = comboBox1.SelectedItem.ToString();
        }
    }
}
