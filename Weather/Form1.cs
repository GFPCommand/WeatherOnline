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

        private DayInfoPanel[] _dayInfoPanels;

        private Panel _sliderPanel;

        private Button _controlButton;
        private Button _settingsButton;
        private Button _weekTemperatureButton;
        private Button _currentTemperatureButton;

        private Image[] _weatherImages;

        public int State { get; private set; }

        public Form1()
        {
            InitializeComponent();

            _uiAnim = new AnimationUI();

            _weatherManager = new WeatherManager();

            _dayInfoPanels = new DayInfoPanel[7];

            _weatherImages = new Image[7];
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            int posX = 80;
            int diffX = 160;
            for (int i = 0; i < _dayInfoPanels.Length; i++)
            {
                _dayInfoPanels[i] = new DayInfoPanel(i)
                {
                    Location = new Point(posX, 280),
                    Size = new Size(150, 240),
                    BackColor = Color.White,
                };

                Controls.Add(_dayInfoPanels[i]);

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

            for (int i = 0; i < _dayInfoPanels.Length; i++)
            {
                _dayInfoPanels[i].Visible = true;
            }

            SetWeekWeather();
        }

        private void CurrentWeather_MouseClick(object sender, MouseEventArgs e)
        {
            if (State == (int)Settings.WeatherWindowState.Current) return;

            State = (int)Settings.WeatherWindowState.Current;

            for (int i = 0; i < _dayInfoPanels.Length; i++)
            {
                _dayInfoPanels[i].Visible = false;
            }

            SetCurrentWeather();
        }

        public void SetWeekWeather()
        {
            string temperatureStr, feelTempStr, windStr, humidityStr, pressureStr;

            _weatherManager.GetWeekWeatherFromServer();

            aboutLocation.Text = $"Weather in {Settings.s_SelectedLocation}";

            temperatureLabel.Text = $"Temperature: {_weatherManager.WeekTemperature[0]}";
            feelTemperatureLabel.Text = $"Feel Temperature: {_weatherManager.WeekFeel[0]}";
            windLabel.Text = $"Wind: {_weatherManager.WeekWind[0]}";
            humidityLabel.Text = $"Humidity: {_weatherManager.WeekHumidity[0]}";
            pressureLabel.Text = $"Pressure: {_weatherManager.WeekPressure[0]}";

            ImagesWeekWeatherType();

            for (int i = 0; i < _dayInfoPanels.Length; i++)
            {
                temperatureStr = $"T: {_weatherManager.WeekTemperature[i]}";
                feelTempStr = $"Feel: {_weatherManager.WeekFeel[i]}";
                windStr = $"Wind: {_weatherManager.WeekWind[i]}";
                humidityStr = $"Hum: {_weatherManager.WeekHumidity[i]}";
                pressureStr = $"Press: {_weatherManager.WeekPressure[i]}";

                _dayInfoPanels[i].ChangeLabelsText(temperatureStr, feelTempStr, windStr, humidityStr, pressureStr);

                _dayInfoPanels[i].WeatherPicture.BackgroundImage = _weatherImages[i];
            }

            mainPic.BackgroundImage = _weatherImages[0];
        }

        public void SetCurrentWeather()
        {
            _weatherManager.GetCurrentWeatherFromServer();

            aboutLocation.Text = $"Weather in {Settings.s_SelectedLocation}";

            temperatureLabel.Text = $"Temperature: {_weatherManager.Temperature}";
            feelTemperatureLabel.Text = $"Feel Temperature: {_weatherManager.Feel}";
            windLabel.Text = $"Wind: {_weatherManager.Wind}";
            humidityLabel.Text = $"Humidity: {_weatherManager.Humidity}";
            pressureLabel.Text = $"Pressure: {_weatherManager.Pressure}";

            mainPic.BackgroundImage = ImageWeatherType();
        }

        private void ImagesWeekWeatherType()
        {
            for (int i = 0; i < _weatherImages.Length; i++)
            {
                if (_weatherManager.WeekWeatherType[i].Contains("Дождь") ||
                _weatherManager.WeekWeatherType[i].Contains("дождь") ||
                _weatherManager.WeekWeatherType[i].Contains("Морось") ||
                _weatherManager.WeekWeatherType[i].Contains("морось")) _weatherImages[i] = Resources.rainy;

                if (_weatherManager.WeekWeatherType[i].Contains("Снег") ||
                _weatherManager.WeekWeatherType[i].Contains("снег")) _weatherImages[i] = Resources.snowy;

                switch (_weatherManager.WeekWeatherType[i])
                {
                    case "Ясно":
                        _weatherImages[i] = Resources.clear;
                        break;
                    case "Ясно, перистые облака":
                        _weatherImages[i] = Resources.clear;
                        break;
                    case "Ясная погода, дымка":
                        _weatherImages[i] = Resources.clear;
                        break;
                    case "Облачно":
                        _weatherImages[i] = Resources.cloudy;
                        break;
                    case "Малооблачно, кучевые облака":
                        _weatherImages[i] = Resources.cloudy;
                        break;
                    case "Переменная облачность":
                        _weatherImages[i] = Resources.cloudy;
                        break;
                    case "Сплошная облачность с просветами":
                        _weatherImages[i] = Resources.cloudy;
                        break;
                    case "Сплошная облачность без просветов":
                        _weatherImages[i] = Resources.too_cloudy;
                        break;
                    case "Туман или низкая облачность":
                        _weatherImages[i] = Resources.cloudy;
                        break;
                    case "Дымка":
                        _weatherImages[i] = Resources.cloudy;
                        break;
                    case "Пасмурно":
                        _weatherImages[i] = Resources.too_cloudy;
                        break;
                    case "Небо без изменений":
                        _weatherImages[i] = Resources.too_cloudy;
                        break;
                }
            }
        }

        private Image ImageWeatherType()
        {
            if (_weatherManager.WeatherType.Contains("Дождь") ||
                _weatherManager.WeatherType.Contains("дождь") ||
                _weatherManager.WeatherType.Contains("Морось") ||
                _weatherManager.WeatherType.Contains("морось")) return _ = Resources.rainy;

            if (_weatherManager.WeatherType.Contains("Снег") ||
                _weatherManager.WeatherType.Contains("снег")) return _ = Resources.snowy;

            switch (_weatherManager.WeatherType)
            {
                case "Ясно":
                    return _ = Resources.clear;
                case "Ясно, перистые облака":
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
                case "Сплошная облачность без просветов":
                    return _ = Resources.too_cloudy;
                case "Туман или низкая облачность":
                    return _ = Resources.cloudy;
                case "Дымка":
                    return _ = Resources.cloudy;
                case "Пасмурно":
                    return _ = Resources.too_cloudy;
                case "Небо без изменений":
                    return _ = Resources.too_cloudy;
                default:
                    MessageBox.Show(_weatherManager.WeatherType);
                    return null;
            }
        }
    }
}