﻿namespace Weather
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.cities = new System.Windows.Forms.ComboBox();
            this.geo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.windUnits = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.temperatureUnits = new System.Windows.Forms.ComboBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cities
            // 
            this.cities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cities.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cities.FormattingEnabled = true;
            this.cities.Items.AddRange(new object[] {
            "Moscow",
            "Saint-Petersburg",
            "Sochi"});
            this.cities.Location = new System.Drawing.Point(13, 26);
            this.cities.Name = "cities";
            this.cities.Size = new System.Drawing.Size(142, 24);
            this.cities.Sorted = true;
            this.cities.TabIndex = 0;
            this.cities.SelectedIndexChanged += new System.EventHandler(this.cities_SelectedIndexChanged);
            // 
            // geo
            // 
            this.geo.BackgroundImage = global::Weather.Properties.Resources.location_pointer;
            this.geo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.geo.Location = new System.Drawing.Point(161, 17);
            this.geo.Name = "geo";
            this.geo.Size = new System.Drawing.Size(40, 40);
            this.geo.TabIndex = 1;
            this.geo.UseVisualStyleBackColor = true;
            this.geo.Click += new System.EventHandler(this.geo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(39, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Find location";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.windUnits);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.temperatureUnits);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(12, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "units of measurement";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Wind units:";
            // 
            // windUnits
            // 
            this.windUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.windUnits.FormattingEnabled = true;
            this.windUnits.Items.AddRange(new object[] {
            "meters",
            "miles"});
            this.windUnits.Location = new System.Drawing.Point(140, 62);
            this.windUnits.Name = "windUnits";
            this.windUnits.Size = new System.Drawing.Size(121, 24);
            this.windUnits.TabIndex = 2;
            this.windUnits.SelectedIndexChanged += new System.EventHandler(this.windUnits_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Temperature units:";
            // 
            // temperatureUnits
            // 
            this.temperatureUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.temperatureUnits.FormattingEnabled = true;
            this.temperatureUnits.Items.AddRange(new object[] {
            "Celsius",
            "Fahrenheit"});
            this.temperatureUnits.Location = new System.Drawing.Point(140, 32);
            this.temperatureUnits.Name = "temperatureUnits";
            this.temperatureUnits.Size = new System.Drawing.Size(121, 24);
            this.temperatureUnits.TabIndex = 0;
            this.temperatureUnits.SelectedIndexChanged += new System.EventHandler(this.temperatureUnits_SelectedIndexChanged);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(71, 182);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(152, 182);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 217);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.geo);
            this.Controls.Add(this.cities);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cities;
        private System.Windows.Forms.Button geo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox temperatureUnits;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox windUnits;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
    }
}