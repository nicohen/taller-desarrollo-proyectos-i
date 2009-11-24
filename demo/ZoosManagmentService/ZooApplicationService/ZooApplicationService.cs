using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;

namespace ZooApplicationService
{
    public partial class ZooApplicationService: ServiceBase
    {
        private List<SensorManager> sensorManagers;

        /// <summary>
        /// Loads and configures sensor managers from storage provider
        /// </summary>
        private void InitializeManagers()
        {
            
        }

        public ZooApplicationService()
        {
            InitializeComponent();
            this.sensorManagers = new List<SensorManager>();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
