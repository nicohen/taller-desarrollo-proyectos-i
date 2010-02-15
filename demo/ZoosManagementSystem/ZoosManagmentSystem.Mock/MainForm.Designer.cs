using System.Windows.Forms;
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.modifyHumidityButton = new System.Windows.Forms.Button();
            this.modifyLuminosityButton = new System.Windows.Forms.Button();
            this.modifyTemperatureButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.luminosityGaugeValueTextbox = new System.Windows.Forms.TextBox();
            this.humidityGaugeValueTextbox = new System.Windows.Forms.TextBox();
            this.temperatureGaugeValueTextbox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.luminosityGaugeBar = new VerticalProgressBar.VerticalProgressBar();
            this.humidityGaugeBar = new VerticalProgressBar.VerticalProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.temperatureGaugeBar = new VerticalProgressBar.VerticalProgressBar();
            this.label7 = new System.Windows.Forms.Label();
            this.environmentNameTextBox = new System.Windows.Forms.TextBox();
MainForm.statusConsoleTextbox = new System.Windows.Forms.TextBox();
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
            this.startServiceButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startServiceButton.Location = new System.Drawing.Point(16, 29);
            this.startServiceButton.Name = "startServiceButton";
            this.startServiceButton.Size = new System.Drawing.Size(75, 23);
            this.startServiceButton.TabIndex = 0;
            this.startServiceButton.Text = "Iniciar";
            this.startServiceButton.UseVisualStyleBackColor = true;
            this.startServiceButton.Click += new System.EventHandler(this.StartServiceButtonClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(MainForm.statusConsoleTextbox);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(15, 116);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(441, 471);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Consola de mensajes";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.modifyHumidityButton);
            this.groupBox2.Controls.Add(this.modifyLuminosityButton);
            this.groupBox2.Controls.Add(this.modifyTemperatureButton);
            this.groupBox2.Controls.Add(this.startServiceButton);
            this.groupBox2.Location = new System.Drawing.Point(17, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(803, 74);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Controls";
            // 
            // modifyHumidityButton
            // 
            this.modifyHumidityButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modifyHumidityButton.Location = new System.Drawing.Point(584, 29);
            this.modifyHumidityButton.Name = "modifyHumidityButton";
            this.modifyHumidityButton.Size = new System.Drawing.Size(196, 23);
            this.modifyHumidityButton.TabIndex = 3;
            this.modifyHumidityButton.Text = "Modificar humedad";
            this.modifyHumidityButton.UseVisualStyleBackColor = true;
            this.modifyHumidityButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // modifyLuminosityButton
            // 
            this.modifyLuminosityButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modifyLuminosityButton.Location = new System.Drawing.Point(353, 29);
            this.modifyLuminosityButton.Name = "modifyLuminosityButton";
            this.modifyLuminosityButton.Size = new System.Drawing.Size(196, 23);
            this.modifyLuminosityButton.TabIndex = 2;
            this.modifyLuminosityButton.Text = "Modificar luminosidad";
            this.modifyLuminosityButton.UseVisualStyleBackColor = true;
            this.modifyLuminosityButton.Click += new System.EventHandler(this.modifyLuminosityButton_Click);
            // 
            // modifyTemperatureButton
            // 
            this.modifyTemperatureButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modifyTemperatureButton.Location = new System.Drawing.Point(129, 29);
            this.modifyTemperatureButton.Name = "modifyTemperatureButton";
            this.modifyTemperatureButton.Size = new System.Drawing.Size(196, 23);
            this.modifyTemperatureButton.TabIndex = 1;
            this.modifyTemperatureButton.Text = "Modificar temperatura";
            this.modifyTemperatureButton.UseVisualStyleBackColor = true;
            this.modifyTemperatureButton.Click += new System.EventHandler(this.modifyTemperatureButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.pictureBox3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.pictureBox2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.pictureBox1);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(473, 116);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(235, 471);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Acciones";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(93, 362);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "Climatización";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(12, 338);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(62, 68);
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(117, 248);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Riego";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(12, 221);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(62, 69);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(103, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Iluminación";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 93);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(62, 75);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.luminosityGaugeValueTextbox);
            this.groupBox4.Controls.Add(this.humidityGaugeValueTextbox);
            this.groupBox4.Controls.Add(this.temperatureGaugeValueTextbox);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.luminosityGaugeBar);
            this.groupBox4.Controls.Add(this.humidityGaugeBar);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.temperatureGaugeBar);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(733, 116);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(420, 471);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Condiciones";
            // 
            // luminosityGaugeValueTextbox
            // 
            this.luminosityGaugeValueTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.luminosityGaugeValueTextbox.Location = new System.Drawing.Point(332, 394);
            this.luminosityGaugeValueTextbox.Name = "luminosityGaugeValueTextbox";
            this.luminosityGaugeValueTextbox.Size = new System.Drawing.Size(49, 31);
            this.luminosityGaugeValueTextbox.TabIndex = 8;
            // 
            // humidityGaugeValueTextbox
            // 
            this.humidityGaugeValueTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.humidityGaugeValueTextbox.Location = new System.Drawing.Point(194, 395);
            this.humidityGaugeValueTextbox.Name = "humidityGaugeValueTextbox";
            this.humidityGaugeValueTextbox.Size = new System.Drawing.Size(53, 31);
            this.humidityGaugeValueTextbox.TabIndex = 7;
            // 
            // temperatureGaugeValueTextbox
            // 
            this.temperatureGaugeValueTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.temperatureGaugeValueTextbox.Location = new System.Drawing.Point(49, 395);
            this.temperatureGaugeValueTextbox.Name = "temperatureGaugeValueTextbox";
            this.temperatureGaugeValueTextbox.Size = new System.Drawing.Size(54, 31);
            this.temperatureGaugeValueTextbox.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(303, 370);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Luminosidad";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(177, 370);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Humedad";
            // 
            // luminosityGaugeBar
            // 
            this.luminosityGaugeBar.BorderStyle = VerticalProgressBar.BorderStyles.Classic;
            this.luminosityGaugeBar.Color = System.Drawing.Color.Blue;
            this.luminosityGaugeBar.Location = new System.Drawing.Point(338, 57);
            this.luminosityGaugeBar.Maximum = 100;
            this.luminosityGaugeBar.Minimum = 0;
            this.luminosityGaugeBar.Name = "luminosityGaugeBar";
            this.luminosityGaugeBar.Size = new System.Drawing.Size(28, 308);
            this.luminosityGaugeBar.Step = 10;
            this.luminosityGaugeBar.Style = VerticalProgressBar.Styles.Classic;
            this.luminosityGaugeBar.TabIndex = 3;
            this.luminosityGaugeBar.Value = 50;
            // 
            // humidityGaugeBar
            // 
            this.humidityGaugeBar.BorderStyle = VerticalProgressBar.BorderStyles.Classic;
            this.humidityGaugeBar.Color = System.Drawing.Color.Blue;
            this.humidityGaugeBar.Location = new System.Drawing.Point(200, 57);
            this.humidityGaugeBar.Maximum = 100;
            this.humidityGaugeBar.Minimum = 0;
            this.humidityGaugeBar.Name = "humidityGaugeBar";
            this.humidityGaugeBar.Size = new System.Drawing.Size(28, 308);
            this.humidityGaugeBar.Step = 10;
            this.humidityGaugeBar.Style = VerticalProgressBar.Styles.Classic;
            this.humidityGaugeBar.TabIndex = 2;
            this.humidityGaugeBar.Value = 50;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(49, 370);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 20);
            this.label4.TabIndex = 1;
            this.label4.Text = "T (ºC)";
            // 
            // temperatureGaugeBar
            // 
            this.temperatureGaugeBar.BorderStyle = VerticalProgressBar.BorderStyles.Classic;
            this.temperatureGaugeBar.Color = System.Drawing.Color.Blue;
            this.temperatureGaugeBar.Location = new System.Drawing.Point(59, 57);
            this.temperatureGaugeBar.Maximum = 100;
            this.temperatureGaugeBar.Minimum = 0;
            this.temperatureGaugeBar.Name = "temperatureGaugeBar";
            this.temperatureGaugeBar.Size = new System.Drawing.Size(28, 308);
            this.temperatureGaugeBar.Step = 10;
            this.temperatureGaugeBar.Style = VerticalProgressBar.Styles.Classic;
            this.temperatureGaugeBar.TabIndex = 0;
            this.temperatureGaugeBar.Value = 50;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(847, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 16);
            this.label7.TabIndex = 5;
            this.label7.Text = "Ambiente:";
            // 
            // environmentNameTextBox
            // 
            this.environmentNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.environmentNameTextBox.Location = new System.Drawing.Point(936, 30);
            this.environmentNameTextBox.Name = "environmentNameTextBox";
            this.environmentNameTextBox.Size = new System.Drawing.Size(198, 31);
            this.environmentNameTextBox.TabIndex = 6;
            // 
MainForm.statusConsoleTextbox.Location = new System.Drawing.Point(6, 19);
            MainForm.statusConsoleTextbox.Multiline = true;
            new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            MainForm.statusConsoleTextbox.Name = "statusConsoleTextbox";
            MainForm.statusConsoleTextbox.Size = new System.Drawing.Size(420, 450);
            MainForm.statusConsoleTextbox.TabIndex = 0;
            MainForm.statusConsoleTextbox.ScrollBars = ScrollBars.Vertical;
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1165, 608);
            this.Controls.Add(this.environmentNameTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "MainForm";
            this.Text = "Emulador de Acciones/Condiciones";
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startServiceButton;
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
        private System.Windows.Forms.TextBox luminosityGaugeValueTextbox;
        private System.Windows.Forms.TextBox humidityGaugeValueTextbox;
        private System.Windows.Forms.TextBox temperatureGaugeValueTextbox;
        private static System.Windows.Forms.TextBox statusConsoleTextbox;
        private Button modifyTemperatureButton;
        private Button modifyLuminosityButton;
        private Button modifyHumidityButton;
        private Label label7;
        private TextBox environmentNameTextBox;
    }
}

