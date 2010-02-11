using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZoosManagmentSystem.Core.Foundation;
using ZoosManagementSystem.Core.MockEnvironmentActionsService;
using ZoosManagementSystem.Core.MockEnvironmentConditionsService;
using System.Threading;

namespace ZoosManagmentSystem.Core.Switch
{
    public class MockSensorManager : SensorManager
    {
        #region Fields

        EnvironmentActionsServiceClient environmentActionsClient;
        EnvironmentConditionsServiceClient environmentConditionsServiceClient;

        #endregion

        #region Methods

        public MockSensorManager(string name, int actionExecutionDelay, Guid environmentId)
            : base(name, actionExecutionDelay, environmentId)
        {
            this.environmentActionsClient = new EnvironmentActionsServiceClient();
            this.environmentConditionsServiceClient = new EnvironmentConditionsServiceClient();
        }

        public override void Start()
        {
            Thread poolingThread = new Thread(new ThreadStart(this.StartPoolingEnvironment));

            poolingThread.Start();
        }

        protected override void Stop()
        {
            base.Stop();
        }

        protected override void ProcessData()
        {
            base.ProcessData();
        }

        private void StartPoolingEnvironment()
        {
            //sEnvironmentConditions environmentConditions = this.environmentConditionsServiceClient.GetEnvironmentConditions(null);

        }

        #endregion
    }
}
