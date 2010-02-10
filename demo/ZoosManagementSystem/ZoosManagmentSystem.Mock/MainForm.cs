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
            this.LaunchService();
        }

        private void LaunchService()
        {
            Type serviceType = typeof(MockEnvironmentActionsService);

            ServiceHost host = new ServiceHost(serviceType, new Uri[] { new Uri("http://localhost:8080/") });

            ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
            behavior.HttpGetEnabled = true;
            host.Description.Behaviors.Add(behavior);

            host.AddServiceEndpoint(typeof(IEnvironmentActionsService), new BasicHttpBinding(), "MockEnvironmentActionsService");
            host.AddServiceEndpoint(typeof(IMetadataExchange), new BasicHttpBinding(), "MEX");

            host.Open();
        }
    }
}
