using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Weather.Properties;

namespace Weather
{
    class AnimationUI
    {
        private bool _isOpen = false;

        public AnimationUI() { }

        public async Task SliderAnimationAsync(Panel sliderPanel, params Button[] sliderButtons)
        {
            bool isDone;

            int width = 200;

            int coeff = 5;

            int count = sliderButtons.Length;

            if (_isOpen)
            {
                isDone = false;

                while (sliderPanel.Size.Width > 60)
                {
                    sliderPanel.Size = new Size(sliderPanel.Width - coeff, sliderPanel.Height);

                    for (int i = 0; i < count; i++)
                    {
                        sliderButtons[i].Size = new Size(sliderButtons[i].Width - coeff, sliderButtons[0].Height);
                    }

                    if (sliderPanel.Width <= 100 && !isDone)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            sliderButtons[i].Text = "";
                        }

                        isDone = true;
                    }

                    await Task.Delay(1);
                }

                _isOpen = false;

                sliderButtons[0].BackgroundImage = Resources.lines;
                sliderButtons[1].BackgroundImage = Resources.settings;
            }
            else
            {
                isDone = false;
                for (int i = 0; i < count; i++)
                {
                    sliderButtons[i].BackgroundImage = null;
                }

                while (sliderPanel.Size.Width <= width)
                {
                    sliderPanel.Size = new Size(sliderPanel.Width + coeff, sliderPanel.Height);

                    for (int i = 0; i < count; i++)
                    {
                        sliderButtons[i].Size = new Size(sliderButtons[i].Width + coeff, sliderButtons[0].Height);
                    }

                    if (sliderPanel.Width >= 100 && !isDone)
                    {
                        sliderButtons[0].Text = "Weather";
                        sliderButtons[1].Text = "Settings";
                        sliderButtons[2].Text = "Week weather";
                        sliderButtons[3].Text = "Current weather";
                        isDone = true;
                    }

                    await Task.Delay(1);
                }

                _isOpen = true;
            }
        }

        public async Task UpDownSliderAsync(object formObj)
        {
            int coeff = 1;

            int delay = 25;

            int maxVal, minVal;

            if (formObj is Control == false || formObj == null)
            {
                MessageBox.Show("Object not a Control element");

                return;
            }

            Control elem = formObj as Control;

            minVal = 0;
            maxVal = elem.Height/10;

            while (true)
            {
                while(elem.Location.Y < maxVal) {
                    elem.Location = new Point(0, elem.Location.Y + coeff);
                    await Task.Delay(delay);
                }

                while (elem.Location.Y > minVal)
                {
                    elem.Location = new Point(0, elem.Location.Y - coeff);
                    await Task.Delay(delay);
                }
            }
        }
    }
}