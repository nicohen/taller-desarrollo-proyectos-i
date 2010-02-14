using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Net;
using ZoosManagementSystem.Interfaces;
using ZoosManagmentSystem.Mock.Services;
using ZoosManagmentSystem.Mock.Storage;

using ZooEnvironment = ZoosManagmentSystem.Mock.Storage.Environment;
using ZoosManagmentSystem.Mock.EnvironmentEmulation;
using System.Threading;



namespace ZoosManagmentSystem.Mock
{
    public partial class MainForm : Form
    {
        #region Fields

        private Guid selectedEnvironmentId;

        #endregion

        #region Methods

        public MainForm()
        {
            InitializeComponent();
            this.LoadDataFromStorage();
            this.temperatureGaugeBar.Value = 0;
            this.luminosityGaugeBar.Value = 0;
            this.humidityGaugeBar.Value = 0;
        }

        private void LoadDataFromStorage()
        {
            List<ZooEnvironment> environments = new List<ZooEnvironment>();

            environments = DbHelper.GetEnvironments();

            this.selectedEnvironmentId = environments[0].Id;

            EnvironmentSimulator.Initialize(environments, 16, 32, 12);

            //this.LogInfo(string.Format("Initializing environment values: Humidity = {0} - Temperature = {1} - Luminosity = {2}", 16, 32, 12));
        }

        private void UpdateEnvironmentGauges(object state)
        {
            while (true)
            {
                CheckForIllegalCrossThreadCalls = false;

                EnvironmentMeasure currentMeasure = EnvironmentSimulator.GetEnvironmentMeasures(this.selectedEnvironmentId);

                this.temperatureGaugeBar.Value = (int)currentMeasure.Temperature;
                this.humidityGaugeBar.Value = (int)currentMeasure.Humidity;
                this.luminosityGaugeBar.Value = (int)currentMeasure.Luminosity;
                this.temperatureGaugeValueTextbox.Text = currentMeasure.Temperature.ToString();
                this.humidityGaugeValueTextbox.Text = currentMeasure.Humidity.ToString();
                this.luminosityGaugeValueTextbox.Text = currentMeasure.Luminosity.ToString();

                Thread.Sleep(2000);
            }

        }

        private void LaunchEnvironmentConditionsService()
        {
            try
            {
                MainForm.LogInfo("Iniciando servicio de condiciones de ambiente...");

                ServiceStarter.StartEnvironmentConditionsService();

                MainForm.LogInfo("Servicio de condiciones de ambiente iniciado.");
            }
            catch (Exception ex)
            {
                MainForm.LogError("Ocurrio un error al iniciar servicio de condiciones de ambiente.", ex);
            }
        }

        private void LaunchEnvironmentActionsService()
        {
            try
            {
                MainForm.LogInfo("Iniciando servicio de acciones de ambiente...");

                ServiceStarter.StartEnvironmentActionsService();

                MainForm.LogInfo("Servicio de acciones de ambiente iniciado.");
            }
            catch (Exception ex)
            {
                MainForm.LogError("Ocurrio un error al iniciar servicio de acciones de ambiente.", ex);
            }
        }

        public static void LogInfo(string message)
        {
            MainForm.statusConsoleTextbox.Text += message + ".";
            MainForm.statusConsoleTextbox.Text += System.Environment.NewLine;
        }

        public static void LogError(string message, Exception ex)
        {
            MainForm.statusConsoleTextbox.Text += message + ":" + ex.Message + ".";
            MainForm.statusConsoleTextbox.Text += System.Environment.NewLine;
        }

        #endregion

        #region Events

        private void StartServiceButtonClick(object sender, EventArgs e)
        {
            this.LaunchEnvironmentActionsService();
            this.LaunchEnvironmentConditionsService();

            ThreadPool.QueueUserWorkItem(new WaitCallback(this.UpdateEnvironmentGauges));
        }

        #endregion

        private void modifyTemperatureButton_Click(object sender, EventArgs e)
        {
            VariableInputForm variableInputForm = new VariableInputForm(this.selectedEnvironmentId, EnvironmentVariable.Temperature);

            variableInputForm.ShowDialog();
        }

        private void modifyLuminosityButton_Click(object sender, EventArgs e)
        {
            VariableInputForm variableInputForm = new VariableInputForm(this.selectedEnvironmentId, EnvironmentVariable.Luminosity);

            variableInputForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VariableInputForm variableInputForm = new VariableInputForm(this.selectedEnvironmentId, EnvironmentVariable.Humidity);

            variableInputForm.ShowDialog();
        }

    }
}
