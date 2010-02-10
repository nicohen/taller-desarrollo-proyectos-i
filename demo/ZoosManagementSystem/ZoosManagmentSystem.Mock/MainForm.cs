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

namespace ZoosManagmentSystem.Mock
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void startServiceButton_Click(object sender, EventArgs e)
        {
            this.LaunchEnvironmentActionsService();
            this.LaunchEnvironmentConditionsService();
        }

        private void LaunchEnvironmentConditionsService()
        {
            try
            {
                this.LogInfo("Starting environment conditions service...");

                Type serviceType = typeof(MockEnvironmentConditionsService);

                ServiceHost host = new ServiceHost(serviceType, new Uri[] { new Uri("http://localhost:8080/") });

                ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                behavior.HttpGetEnabled = true;
                host.Description.Behaviors.Add(behavior);

                host.AddServiceEndpoint(typeof(IEnvironmentConditionsService), new BasicHttpBinding(), "MockEnvironmentConditionsService");
                host.AddServiceEndpoint(typeof(IMetadataExchange), new BasicHttpBinding(), "MEX");
                host.Open();

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

                Type serviceType = typeof(MockEnvironmentActionsService);

                ServiceHost host = new ServiceHost(serviceType, new Uri[] { new Uri("http://localhost:8081/") });

                ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                behavior.HttpGetEnabled = true;
                host.Description.Behaviors.Add(behavior);

                host.AddServiceEndpoint(typeof(IEnvironmentActionsService), new BasicHttpBinding(), "MockEnvironmentActionsService");
                host.AddServiceEndpoint(typeof(IMetadataExchange), new BasicHttpBinding(), "MEX");

                host.Open();

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
            this.statusMessagesTextbox.Text += Environment.NewLine;
        }

        private void LogError(string message, Exception ex)
        {
            this.statusMessagesTextbox.Text += message + ":" + ex.Message + ".";
            this.statusMessagesTextbox.Text += Environment.NewLine;
        }
    }
}
