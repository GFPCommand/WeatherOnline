using System;
using System.Drawing;
using System.Windows.Forms;
using Weather.Properties;

namespace Weather
{
    class DayInfoPanel : Panel
    {
        private Label _dayLabel;
        private Label _temperatureLabel;
        private Label _feelTemperatureLabel;
        private Label _windLabel;
        private Label _humidityLabel;
        private Label _pressureLabel;
        private PictureBox _weatherPicture;

        private readonly string _fontName;
        private readonly Font _defaultFont;

        public PictureBox WeatherPicture { 
            get 
            {
                return _weatherPicture;
            }
            set 
            {
                _weatherPicture = value;
            }
        }

        public DayInfoPanel(int dayDiff)
        {
            int dayNow = DateTime.Now.Day + dayDiff;
            int month = DateTime.Now.Month;

            _fontName = DefaultFont.Name;
            _defaultFont = new Font(_fontName, 11f);

            _weatherPicture = new PictureBox
            {
                Size = new Size(50, 50),
                Location = new Point(10, 10),
                BackgroundImage = Resources.clear,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            if ((month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12) && dayNow > 31)
            {
                month++;
                dayNow -= 31;
            } 
            else if ((month == 4 || month == 6 || month == 9 || month == 11) && dayNow > 30)
            {
                month++;
                dayNow -= 30;
            } else if (month == 2 && dayNow > 28)
            {
                month++;
                dayNow -= 28;
            }

            _dayLabel = new Label
            {
                Text = $"{dayNow}.{month}",
                Location = new Point(70, 25),
                Font = new Font(_fontName, 14f)
            };

            _temperatureLabel = new Label
            {
                Text = "T: ",
                Location = new Point(5, 70),
                Font = _defaultFont,
                AutoSize = false,
                Size = new Size(140, 15)
            };

            _feelTemperatureLabel = new Label
            {
                Text = "Feel: ",
                Location = new Point(5, 100),
                Font = _defaultFont,
                AutoSize = false,
                Size = new Size(140, 15)
            };

            _windLabel = new Label
            {
                Text = "Wind: ",
                Location = new Point(5, 130),
                Font = _defaultFont,
                AutoSize = false,
                Size = new Size(140, 15)
            };

            _humidityLabel = new Label
            {
                Text = "Hum: ",
                Location = new Point(5, 160),
                Font = _defaultFont,
                AutoSize = false,
                Size = new Size(140, 15)
            };

            _pressureLabel = new Label
            {
                Text = "Press: ",
                Location = new Point(5, 190),
                Font = _defaultFont,
                AutoSize = false,
                Size = new Size(140, 15)
            };

            Controls.Add(_weatherPicture);
            Controls.Add(_dayLabel);
            Controls.Add(_temperatureLabel);
            Controls.Add(_feelTemperatureLabel);
            Controls.Add(_windLabel);
            Controls.Add(_humidityLabel);
            Controls.Add(_pressureLabel);
        }

        public void ChangeLabelsText(string temperature, string feelTemperature, string wind, string humidity, string pressure)
        {
            _temperatureLabel.Text = temperature;
            _feelTemperatureLabel.Text = feelTemperature;
            _windLabel.Text = wind;
            _humidityLabel.Text = humidity;
            _pressureLabel.Text = pressure;
        }
    }
}
