﻿using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Weather.Properties;

namespace Weather
{
    class AnimationUI
    {
        private bool _isOpen = false;

        public AnimationUI() { }

        public async Task SliderAnimationAsync(Panel sliderPanel, Button[] sliderButtons)
        {
            bool isDone = false;

            int width = 200;

            int coeff = 5;

            if (_isOpen)
            {
                isDone = false;

                while (sliderPanel.Size.Width > 60)
                {
                    sliderPanel.Size = new Size(sliderPanel.Width - coeff, sliderPanel.Height);
                    sliderButtons[0].Size = new Size(sliderButtons[0].Width - coeff, sliderButtons[0].Height);
                    sliderButtons[1].Size = new Size(sliderButtons[1].Width - coeff, sliderButtons[1].Height);

                    if (sliderPanel.Width <= 100 && !isDone)
                    {
                        sliderButtons[0].Text = "";
                        sliderButtons[1].Text = "";
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
                sliderButtons[0].BackgroundImage = null;
                sliderButtons[1].BackgroundImage = null;

                while (sliderPanel.Size.Width <= width)
                {
                    sliderPanel.Size = new Size(sliderPanel.Width + coeff, sliderPanel.Height);
                    sliderButtons[0].Size = new Size(sliderButtons[0].Width + coeff, sliderButtons[0].Height);
                    sliderButtons[1].Size = new Size(sliderButtons[1].Width + coeff, sliderButtons[1].Height);

                    if (sliderPanel.Width >= 100 && !isDone)
                    {
                        sliderButtons[0].Text = "Buttons";
                        sliderButtons[1].Text = "Settings";
                        isDone = true;
                    }

                    await Task.Delay(1);
                }

                _isOpen = true;
            }
        }
    }
}