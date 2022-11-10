using System;
using System.Windows.Forms;
using System.Drawing;
using Weather.Properties;

namespace Weather
{
    public partial class Form1 : Form
    {
        private SettingsForm _settingsForm;

        private AnimationUI _uiAnim;

        private WeatherManager _weatherManager;

        private Panel[] _daySmallInfo;
        private Label[] _daysLabels;
        private Label[] _temperatureLabels;
        private Label[] _feelTemperatureLabels;
        private Label[] _windLabels;
        private Label[] _humidityLabels;
        private Label[] _pressureLabels;
        private PictureBox[] _weatherPictures;

        private Panel _sliderPanel;
        private Button _controlButton;
        private Button _settingsButton;

        private readonly string _fontName;
        private readonly Font _defaultFont;

        public Form1()
        {
            InitializeComponent();

            _uiAnim = new AnimationUI();

            _weatherManager = new WeatherManager();

            _daySmallInfo = new Panel[7];

            _weatherPictures       = new PictureBox[7];
            _daysLabels            = new Label[7];
            _temperatureLabels     = new Label[7];
            _feelTemperatureLabels = new Label[7];
            _windLabels            = new Label[7];
            _humidityLabels        = new Label[7];
            _pressureLabels        = new Label[7];

            _fontName = DefaultFont.Name;
            _defaultFont = new Font(_fontName, 12f);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            int posX = 90;
            int diffX = 160;
            for (int i = 0; i < _daySmallInfo.Length; i++)
            {
                _daySmallInfo[i] = new Panel
                {
                    Location = new Point(posX, 250),
                    Size = new Size(150, 200),
                    BackColor = Color.White,
                };

                _weatherPictures[i] = new PictureBox
                {
                    Parent = _daySmallInfo[i],
                    Size = new Size(50, 50),
                    Location = new Point(10, 10),
                    BackgroundImage = Resources.clear,
                    BackgroundImageLayout = ImageLayout.Stretch
                };

                _daysLabels[i] = new Label {
                    Parent = _daySmallInfo[i],
                    Text = $"{DateTime.Now.Day + i}.{DateTime.Now.Month}",
                    Location = new Point(65, 25),
                    Font = new Font(_fontName, 14f)
                };

                _temperatureLabels[i] = new Label
                {
                    Parent = _daySmallInfo[i],
                    Text = "Temperature: ",
                    Location = new Point(10, 65),
                    Font = _defaultFont
                };

                _feelTemperatureLabels[i] = new Label
                {
                    Parent = _daySmallInfo[i],
                    Text = "Feel temp: ",
                    Location = new Point(10, 90),
                    Font = _defaultFont
                };

                _windLabels[i] = new Label
                {
                    Parent = _daySmallInfo[i],
                    Text = "Wind: ",
                    Location = new Point(10, 115),
                    Font = _defaultFont
                };

                _humidityLabels[i] = new Label
                {
                    Parent = _daySmallInfo[i],
                    Text = "Humidity: ",
                    Location = new Point(10, 140),
                    Font = _defaultFont
                };

                _pressureLabels[i] = new Label
                {
                    Parent = _daySmallInfo[i],
                    Text = "Pressure: ",
                    Location = new Point(10, 165),
                    Font = _defaultFont
                };

                Controls.Add(_daySmallInfo[i]);

                posX += diffX;
            }

            _sliderPanel = new Panel
            {
                Parent = this,
                Location = new Point(0, 0),
                Size = new Size(60, 650),
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
                Location = new Point(0, 550),
                Size = new Size(60, 60),
                BackgroundImage = Resources.settings,
                BackgroundImageLayout = ImageLayout.Stretch,
                Font = new Font(DefaultFont.Name, 16f)
            };

            _sliderPanel.BringToFront();

            _sliderPanel.Controls.Add(_controlButton);
            _sliderPanel.Controls.Add(_settingsButton);

            _controlButton.MouseClick += controlButton_MouseClick;
            _settingsButton.MouseClick += settingsButton_MouseClick;

            SetWeather();

            await _uiAnim.UpDownSliderAsync(mainPic);
        }

        private void settingsButton_MouseClick(object sender, MouseEventArgs e)
        {
            _settingsForm = new SettingsForm(this);
            _settingsForm.Show();
        }

        private async void controlButton_MouseClick(object sender, EventArgs e)
        {
            await _uiAnim.SliderAnimationAsync(_sliderPanel, new Button[2] {_controlButton, _settingsButton});
        }

        public void SetWeather()
        {
            _weatherManager.GetWeekWeatherFromServer();

            temperatureLabel.Text = $"Temperature: {_weatherManager.Temperature}";
            feelTemperatureLabel.Text = $"Feel Temperature: {_weatherManager.Feel}";
            windLabel.Text = $"Wind: {_weatherManager.Wind}";
            humidityLabel.Text = $"Humidity: {_weatherManager.Humidity}";
            pressureLabel.Text = $"Pressure: {_weatherManager.Pressure}";

            aboutLocation.Text = $"Weather in {Settings.s_SelectedLocation}";

            switch (_weatherManager.WeatherType)
            {
                case "Ясная погода, дымка":
                    mainPic.BackgroundImage = Resources.clear;
                    break;
                case "Облачно":
                    mainPic.BackgroundImage = Resources.cloudy;
                    break;
                case "Малооблачно, кучевые облака":
                    mainPic.BackgroundImage = Resources.cloudy;
                    break;
                case "Пасмурно":
                    mainPic.BackgroundImage = Resources.too_cloudy;
                    break;
                case "Пасмурно, дождь":
                    mainPic.BackgroundImage = Resources.rainy;
                    break;
                case "Пасмурно, небольшой дождь":
                    mainPic.BackgroundImage = Resources.rainy;
                    break;
                case "Пасмурно, ливневый дождь":
                    mainPic.BackgroundImage = Resources.rainy;
                    break;
                default:
                    break;
            }
        }
    }
}