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


namespace ZoosManagmentSystem.Mock
{
    public partial class MainForm : Form
    {
        #region Fields

        #endregion

        #region Methods

        public MainForm()
        {
            InitializeComponent();
            this.LoadDataFromStorage();
        }

        private void LoadDataFromStorage()
        {
            List<ZooEnvironment> environments = new List<ZooEnvironment>();

            environments = DbHelper.GetEnvironments();

            EnvironmentSimulator.Initialize(environments, 500, 32, 150);
        }

        private void LaunchEnvironmentConditionsService()
        {
            try
            {
                this.LogInfo("Starting environment conditions service...");

                ServiceStarter.StartEnvironmentConditionsService();

                this.LogInfo("Environment conditions service started.");
            }
            catch (Exception ex)
            {
                this.LogError("An error ocurred starting environment conditions service.", ex);
            }
        }

        private void LaunchEnvironmentActionsService()
        {
            try
            {
                this.LogInfo("Starting environment actions service...");

                ServiceStarter.StartEnvironmentActionsService();

                this.LogInfo("Environment actions service started.");
            }
            catch (Exception ex)
            {
                this.LogError("An error ocurred starting environment actions service.", ex);
            }
        }

        private void LogInfo(string message)
        {
            this.statusMessagesTextbox.Text += message + ".";
            this.statusMessagesTextbox.Text += System.Environment.NewLine;
        }

        private void LogError(string message, Exception ex)
        {
            this.statusMessagesTextbox.Text += message + ":" + ex.Message + ".";
            this.statusMessagesTextbox.Text += System.Environment.NewLine;
        }

        #endregion

        #region Events

        private void StartServiceButtonClick(object sender, EventArgs e)
        {
            this.LaunchEnvironmentActionsService();
            this.LaunchEnvironmentConditionsService();
        }

        #endregion
    }
}
