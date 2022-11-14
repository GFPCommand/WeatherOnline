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
        private Button _weekTemperatureButton;
        private Button _currentTemperatureButton;

        private readonly string _fontName;
        private readonly Font _defaultFont;

        public int State { get; private set; }

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
            _defaultFont = new Font(_fontName, 11f);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            int posX = 80;
            int diffX = 160;
            for (int i = 0; i < _daySmallInfo.Length; i++)
            {
                _daySmallInfo[i] = new Panel
                {
                    Location = new Point(posX, 280),
                    Size = new Size(150, 240),
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
                    Location = new Point(70, 25),
                    Font = new Font(_fontName, 14f)
                };

                _temperatureLabels[i] = new Label
                {
                    Parent = _daySmallInfo[i],
                    Text = "Temperature: ",
                    Location = new Point(5, 70),
                    Font = _defaultFont,
                    AutoSize = false,
                    Size = new Size(140, 15)
                };

                _feelTemperatureLabels[i] = new Label
                {
                    Parent = _daySmallInfo[i],
                    Text = "Feel temp: ",
                    Location = new Point(5, 100),
                    Font = _defaultFont,
                    AutoSize = false,
                    Size = new Size(140, 15)
                };

                _windLabels[i] = new Label
                {
                    Parent = _daySmallInfo[i],
                    Text = "Wind: ",
                    Location = new Point(5, 130),
                    Font = _defaultFont,
                    AutoSize = false,
                    Size = new Size(140, 15)
                };

                _humidityLabels[i] = new Label
                {
                    Parent = _daySmallInfo[i],
                    Text = "Humidity: ",
                    Location = new Point(5, 160),
                    Font = _defaultFont,
                    AutoSize = false,
                    Size = new Size(140, 15)
                };

                _pressureLabels[i] = new Label
                {
                    Parent = _daySmallInfo[i],
                    Text = "Pressure: ",
                    Location = new Point(5, 190),
                    Font = _defaultFont,
                    AutoSize = false,
                    Size = new Size(140, 15)
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

            _weekTemperatureButton = new Button
            {
                Location = new Point(0, 60),
                Size = new Size(60, 60),
                BackgroundImageLayout = ImageLayout.Stretch,
                Font = new Font(DefaultFont.Name, 16f)
            };

            _currentTemperatureButton = new Button
            {
                Location = new Point(0, 120),
                Size = new Size(60, 60),
                BackgroundImageLayout = ImageLayout.Stretch,
                Font = new Font(DefaultFont.Name, 16f)
            };

            _sliderPanel.BringToFront();

            _sliderPanel.Controls.Add(_controlButton);
            _sliderPanel.Controls.Add(_settingsButton);
            _sliderPanel.Controls.Add(_weekTemperatureButton);
            _sliderPanel.Controls.Add(_currentTemperatureButton);

            _controlButton.MouseClick += controlButton_MouseClick;
            _settingsButton.MouseClick += settingsButton_MouseClick;
            _weekTemperatureButton.MouseClick += WeekWeather_MouseClick;
            _currentTemperatureButton.MouseClick += CurrentWeather_MouseClick;

            SetWeekWeather();

            State = (int)Settings.WeatherWindowState.Week;

            await _uiAnim.UpDownSliderAsync(mainPic);
        }

        private void settingsButton_MouseClick(object sender, MouseEventArgs e)
        {
            _settingsForm = new SettingsForm(this);
            _settingsForm.Show();
        }

        private async void controlButton_MouseClick(object sender, EventArgs e)
        {
            await _uiAnim.SliderAnimationAsync(_sliderPanel, _controlButton, _settingsButton, _weekTemperatureButton, _currentTemperatureButton);
        }

        private void WeekWeather_MouseClick(object sender, MouseEventArgs e)
        {
            if (State == (int)Settings.WeatherWindowState.Week) return;

            State = (int)Settings.WeatherWindowState.Week;

            for (int i = 0; i < _daySmallInfo.Length; i++)
            {
                _daySmallInfo[i].Visible = true;
            }

            SetWeekWeather();
        }

        private void CurrentWeather_MouseClick(object sender, MouseEventArgs e)
        {
            if (State == (int)Settings.WeatherWindowState.Current) return;

            State = (int)Settings.WeatherWindowState.Current;

            for (int i = 0; i < _daySmallInfo.Length; i++)
            {
                _daySmallInfo[i].Visible = false;
            }

            SetCurrentWeather();
        }

        public void SetWeekWeather()
        {
            _weatherManager.GetWeekWeatherFromServer();

            aboutLocation.Text = $"Weather in {Settings.s_SelectedLocation}";

            temperatureLabel.Text = $"Temperature: {_weatherManager.Temperature}";
            feelTemperatureLabel.Text = $"Feel Temperature: {_weatherManager.Feel}";
            windLabel.Text = $"Wind: {_weatherManager.Wind}";
            humidityLabel.Text = $"Humidity: {_weatherManager.Humidity}";
            pressureLabel.Text = $"Pressure: {_weatherManager.Pressure}";

            for (int i = 0; i < _daySmallInfo.Length; i++)
            {
                _temperatureLabels[i].Text = $"T: {_weatherManager.Temperature}";
                _feelTemperatureLabels[i].Text = $"Feel: {_weatherManager.Feel}";
                _windLabels[i].Text = $"Wind: {_weatherManager.Wind}";
                _humidityLabels[i].Text = $"Hum: {_weatherManager.Humidity}";
                _pressureLabels[i].Text = $"Press: {_weatherManager.Pressure}";
            }

            mainPic.BackgroundImage = ImageWeatherType();
        }

        public void SetCurrentWeather()
        {
            _weatherManager.GetCurrentWeatherFromServer();

            temperatureLabel.Text = $"Temperature: {_weatherManager.Temperature}";
            feelTemperatureLabel.Text = $"Feel Temperature: {_weatherManager.Feel}";
            windLabel.Text = $"Wind: {_weatherManager.Wind}";
            humidityLabel.Text = $"Humidity: {_weatherManager.Humidity}";
            pressureLabel.Text = $"Pressure: {_weatherManager.Pressure}";

            mainPic.BackgroundImage = ImageWeatherType();
        }

        private Image ImageWeatherType()
        {
            switch (_weatherManager.WeatherType)
            {
                case "Ясно":
                    return _ = Resources.clear;
                case "Ясная погода, дымка":
                    return _ = Resources.clear;
                case "Облачно":
                    return _ = Resources.cloudy;
                case "Малооблачно, кучевые облака":
                    return _ = Resources.cloudy;
                case "Переменная облачность":
                    return _ = Resources.cloudy;
                case "Сплошная облачность с просветами":
                    return _ = Resources.cloudy;
                case "Туман или низкая облачность":
                    return _ = Resources.cloudy;
                case "Пасмурно":
                    return _ = Resources.too_cloudy;
                case "Пасмурно, дождь":
                    return _ = Resources.rainy;
                case "Пасмурно, небольшой дождь":
                    return _ = Resources.rainy;
                case "Пасмурно, ливневый дождь":
                    return _ = Resources.rainy;
                case "Дождь":
                    return _ = Resources.rainy;
                case "Слабый дождь":
                    return _ = Resources.rainy;
                case "Сильный дождь":
                    return _ = Resources.rainy;
                case "Ливневый дождь":
                    return _ = Resources.rainy;
                case "Пасмурно, небольшой снег":
                    return _ = Resources.rainy; //snowy --> TODO
                default:
                    return null;
            }
        }
    }
}