using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZoosManagmentSystem.Mock.EnvironmentEmulation;

namespace ZoosManagmentSystem.Mock
{
    public partial class VariableInputForm : Form
    {
        private Guid environmentId;
        private EnvironmentVariable variable;

        public VariableInputForm()
        {
            InitializeComponent();
        }

        public VariableInputForm(Guid environmentId, EnvironmentVariable variable)
        {
            this.environmentId = environmentId;
            this.variable = variable;
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            switch (this.variable)
            {
                case EnvironmentVariable.Humidity:
                    EnvironmentSimulator.SetHumidity(this.environmentId, float.Parse(this.variableInputTextBox.Text));
                    break;
                case EnvironmentVariable.Luminosity:
                    EnvironmentSimulator.SetLuminosity(this.environmentId, float.Parse(this.variableInputTextBox.Text));
                    break;
                case EnvironmentVariable.Temperature:
                    EnvironmentSimulator.SetTemperature(this.environmentId, float.Parse(this.variableInputTextBox.Text));
                    break;
                default:
                    break;
            }

            this.Close();
        }


    }
}

