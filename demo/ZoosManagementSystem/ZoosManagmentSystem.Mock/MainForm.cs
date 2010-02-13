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
            EnvironmentMeasure currentMeasure = EnvironmentSimulator.GetEnvironmentMeasures(this.selectedEnvironmentId);

            this.temperatureGaugeBar.Value = (int)currentMeasure.Temperature;
            this.humidityGaugeBar.Value = (int)currentMeasure.Humidity;
            this.luminosityGaugeBar.Value = (int)currentMeasure.Luminosity;
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
            MainForm.statusMessagesTextbox.Text += message + ".";
            MainForm.statusMessagesTextbox.Text += System.Environment.NewLine;
        }

        public static void LogError(string message, Exception ex)
        {
            MainForm.statusMessagesTextbox.Text += message + ":" + ex.Message + ".";
            MainForm.statusMessagesTextbox.Text += System.Environment.NewLine;
        }

        #endregion

        #region Events

        private void StartServiceButtonClick(object sender, EventArgs e)
        {
            this.LaunchEnvironmentActionsService();
            this.LaunchEnvironmentConditionsService();

            System.Threading.Timer timer = new System.Threading.Timer(new TimerCallback(this.UpdateEnvironmentGauges), null, 0, 3000);
        }

        #endregion

    }
}
