namespace Weather
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.aboutLocation = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pressureLabel = new System.Windows.Forms.Label();
            this.humidityLabel = new System.Windows.Forms.Label();
            this.windLabel = new System.Windows.Forms.Label();
            this.feelTemperatureLabel = new System.Windows.Forms.Label();
            this.temperatureLabel = new System.Windows.Forms.Label();
            this.mainPic = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainPic)).BeginInit();
            this.SuspendLayout();
            // 
            // aboutLocation
            // 
            this.aboutLocation.AutoSize = true;
            this.aboutLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.aboutLocation.Location = new System.Drawing.Point(400, 10);
            this.aboutLocation.Name = "aboutLocation";
            this.aboutLocation.Size = new System.Drawing.Size(144, 31);
            this.aboutLocation.TabIndex = 0;
            this.aboutLocation.Text = "Weather in";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pressureLabel);
            this.panel1.Controls.Add(this.humidityLabel);
            this.panel1.Controls.Add(this.windLabel);
            this.panel1.Controls.Add(this.feelTemperatureLabel);
            this.panel1.Controls.Add(this.temperatureLabel);
            this.panel1.Controls.Add(this.mainPic);
            this.panel1.Location = new System.Drawing.Point(261, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(602, 176);
            this.panel1.TabIndex = 1;
            // 
            // pressureLabel
            // 
            this.pressureLabel.AutoSize = true;
            this.pressureLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pressureLabel.Location = new System.Drawing.Point(182, 142);
            this.pressureLabel.Name = "pressureLabel";
            this.pressureLabel.Size = new System.Drawing.Size(76, 20);
            this.pressureLabel.TabIndex = 5;
            this.pressureLabel.Text = "Pressure:";
            // 
            // humidityLabel
            // 
            this.humidityLabel.AutoSize = true;
            this.humidityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.humidityLabel.Location = new System.Drawing.Point(182, 111);
            this.humidityLabel.Name = "humidityLabel";
            this.humidityLabel.Size = new System.Drawing.Size(74, 20);
            this.humidityLabel.TabIndex = 4;
            this.humidityLabel.Text = "Humidity:";
            // 
            // windLabel
            // 
            this.windLabel.AutoSize = true;
            this.windLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.windLabel.Location = new System.Drawing.Point(182, 82);
            this.windLabel.Name = "windLabel";
            this.windLabel.Size = new System.Drawing.Size(49, 20);
            this.windLabel.TabIndex = 3;
            this.windLabel.Text = "Wind:";
            // 
            // feelTemperatureLabel
            // 
            this.feelTemperatureLabel.AutoSize = true;
            this.feelTemperatureLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.feelTemperatureLabel.Location = new System.Drawing.Point(182, 53);
            this.feelTemperatureLabel.Name = "feelTemperatureLabel";
            this.feelTemperatureLabel.Size = new System.Drawing.Size(139, 20);
            this.feelTemperatureLabel.TabIndex = 2;
            this.feelTemperatureLabel.Text = "Feel Temperature:";
            // 
            // temperatureLabel
            // 
            this.temperatureLabel.AutoSize = true;
            this.temperatureLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.temperatureLabel.Location = new System.Drawing.Point(182, 24);
            this.temperatureLabel.Name = "temperatureLabel";
            this.temperatureLabel.Size = new System.Drawing.Size(104, 20);
            this.temperatureLabel.TabIndex = 1;
            this.temperatureLabel.Text = "Temperature:";
            // 
            // mainPic
            // 
            this.mainPic.BackgroundImage = global::Weather.Properties.Resources.clear;
            this.mainPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mainPic.InitialImage = null;
            this.mainPic.Location = new System.Drawing.Point(13, 13);
            this.mainPic.Name = "mainPic";
            this.mainPic.Size = new System.Drawing.Size(140, 140);
            this.mainPic.TabIndex = 0;
            this.mainPic.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 611);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.aboutLocation);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Weather Online";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label aboutLocation;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox mainPic;
        private System.Windows.Forms.Label pressureLabel;
        private System.Windows.Forms.Label humidityLabel;
        private System.Windows.Forms.Label windLabel;
        private System.Windows.Forms.Label feelTemperatureLabel;
        private System.Windows.Forms.Label temperatureLabel;
    }
}

