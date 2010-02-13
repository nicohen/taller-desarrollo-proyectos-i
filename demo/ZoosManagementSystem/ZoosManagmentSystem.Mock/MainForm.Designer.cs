namespace ZoosManagmentSystem.Mock
{
    partial class MainForm
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
            this.startServiceButton = new System.Windows.Forms.Button();
            MainForm.statusMessagesTextbox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.luminosityGaugeBar = new VerticalProgressBar.VerticalProgressBar();
            this.humidityGaugeBar = new VerticalProgressBar.VerticalProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.temperatureGaugeBar = new VerticalProgressBar.VerticalProgressBar();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // startServiceButton
            // 
            this.startServiceButton.Location = new System.Drawing.Point(16, 29);
            this.startServiceButton.Name = "startServiceButton";
            this.startServiceButton.Size = new System.Drawing.Size(75, 23);
            this.startServiceButton.TabIndex = 0;
            this.startServiceButton.Text = "Start services";
            this.startServiceButton.UseVisualStyleBackColor = true;
            this.startServiceButton.Click += new System.EventHandler(this.StartServiceButtonClick);
            // 
            // statusMessagesTextbox
            // 
            MainForm.statusMessagesTextbox.Location = new System.Drawing.Point(9, 18);
            MainForm.statusMessagesTextbox.Multiline = true;
            MainForm.statusMessagesTextbox.Name = "statusMessagesTextbox";
            MainForm.statusMessagesTextbox.Size = new System.Drawing.Size(237, 146);
            MainForm.statusMessagesTextbox.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(MainForm.statusMessagesTextbox);
            this.groupBox1.Location = new System.Drawing.Point(15, 116);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 174);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Consola de mensajes";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.startServiceButton);
            this.groupBox2.Location = new System.Drawing.Point(17, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(591, 72);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Controls";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.pictureBox3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.pictureBox2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.pictureBox1);
            this.groupBox3.Location = new System.Drawing.Point(278, 116);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(225, 174);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Acciones";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Temperatura";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(12, 115);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(32, 33);
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Riego";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(12, 73);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 33);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Iluminación";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 33);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.luminosityGaugeBar);
            this.groupBox4.Controls.Add(this.humidityGaugeBar);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.temperatureGaugeBar);
            this.groupBox4.Location = new System.Drawing.Point(514, 116);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(242, 174);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Condiciones";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(171, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Luminosidad";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(96, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Humedad";
            // 
            // luminosityGaugeBar
            // 
            this.luminosityGaugeBar.BorderStyle = VerticalProgressBar.BorderStyles.Classic;
            this.luminosityGaugeBar.Color = System.Drawing.Color.Blue;
            this.luminosityGaugeBar.Location = new System.Drawing.Point(195, 22);
            this.luminosityGaugeBar.Maximum = 100;
            this.luminosityGaugeBar.Minimum = 0;
            this.luminosityGaugeBar.Name = "luminosityGaugeBar";
            this.luminosityGaugeBar.Size = new System.Drawing.Size(10, 120);
            this.luminosityGaugeBar.Step = 10;
            this.luminosityGaugeBar.Style = VerticalProgressBar.Styles.Classic;
            this.luminosityGaugeBar.TabIndex = 3;
            this.luminosityGaugeBar.Value = 50;
            // 
            // humidityGaugeBar
            // 
            this.humidityGaugeBar.BorderStyle = VerticalProgressBar.BorderStyles.Classic;
            this.humidityGaugeBar.Color = System.Drawing.Color.Blue;
            this.humidityGaugeBar.Location = new System.Drawing.Point(113, 22);
            this.humidityGaugeBar.Maximum = 100;
            this.humidityGaugeBar.Minimum = 0;
            this.humidityGaugeBar.Name = "humidityGaugeBar";
            this.humidityGaugeBar.Size = new System.Drawing.Size(10, 120);
            this.humidityGaugeBar.Step = 10;
            this.humidityGaugeBar.Style = VerticalProgressBar.Styles.Classic;
            this.humidityGaugeBar.TabIndex = 2;
            this.humidityGaugeBar.Value = 50;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "T (ºC)";
            // 
            // temperatureGaugeBar
            // 
            this.temperatureGaugeBar.BorderStyle = VerticalProgressBar.BorderStyles.Classic;
            this.temperatureGaugeBar.Color = System.Drawing.Color.Blue;
            this.temperatureGaugeBar.Location = new System.Drawing.Point(29, 22);
            this.temperatureGaugeBar.Maximum = 100;
            this.temperatureGaugeBar.Minimum = 0;
            this.temperatureGaugeBar.Name = "temperatureGaugeBar";
            this.temperatureGaugeBar.Size = new System.Drawing.Size(10, 120);
            this.temperatureGaugeBar.Step = 10;
            this.temperatureGaugeBar.Style = VerticalProgressBar.Styles.Classic;
            this.temperatureGaugeBar.TabIndex = 0;
            this.temperatureGaugeBar.Value = 50;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 311);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "MainForm";
            this.Text = "Emulador de Acciones/Condiciones";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startServiceButton;
        private static System.Windows.Forms.TextBox statusMessagesTextbox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private VerticalProgressBar.VerticalProgressBar luminosityGaugeBar;
        private VerticalProgressBar.VerticalProgressBar humidityGaugeBar;
        private System.Windows.Forms.Label label4;
        private VerticalProgressBar.VerticalProgressBar temperatureGaugeBar;
    }
}

