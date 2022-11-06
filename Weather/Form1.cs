using System;
using System.Windows.Forms;
using System.Drawing;
using Weather.Properties;

namespace Weather
{
    public partial class Form1 : Form
    {
        private SettingsForm f;

        private AnimationUI ui;

        private Panel _sliderPanel;
        private Button _controlButton;
        private Button _settingsButton;

        public Form1()
        {
            InitializeComponent();
            ui = new AnimationUI();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _sliderPanel = new Panel
            {
                Parent = this,
                Location = new Point(0, 0),
                Size = new Size(60, 500),
                BackColor = Color.FromArgb(20, 70, 130, 180)
            };

            _controlButton = new Button
            {
                Location = new Point(0, 0),
                Size = new Size(60, 60),
                BackgroundImage = Resources.lines,
                BackgroundImageLayout = ImageLayout.Stretch,
                Font = new Font(DefaultFont.Name, 16f)
            };

            _settingsButton = new Button
            {
                Location = new Point(0, 400),
                Size = new Size(60, 60),
                BackgroundImage = Resources.settings,
                BackgroundImageLayout = ImageLayout.Stretch,
                Font = new Font(DefaultFont.Name, 16f)
            };

            _sliderPanel.Controls.Add(_controlButton);
            _sliderPanel.Controls.Add(_settingsButton);

            _controlButton.MouseClick += controlButton_MouseClick;
            _settingsButton.MouseClick += settingsButton_MouseClick;
        }

        private void settingsButton_MouseClick(object sender, MouseEventArgs e)
        {
            f = new SettingsForm();
            f.Show();
        }

        private async void controlButton_MouseClick(object sender, EventArgs e)
        {
            await ui.SliderAnimationAsync(_sliderPanel, new Button[2] {_controlButton, _settingsButton});
        }
    }
}