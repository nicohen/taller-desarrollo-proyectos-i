using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZooApplicationService.Foundation;
using System.ServiceModel;
using System.ServiceModel.Description;
using ZooApplicationService.Foundation.Sensor;

namespace ZooApplicationService.Switch.Sensor.MockSensor
{
    public class MockSensorManager : SensorManager
    {
        public override void Start()
        {
            Type serviceType = typeof(MockSensorService);

            ServiceHost host = new ServiceHost(serviceType, new Uri[] { new Uri("http://localhost:8080/") });

            // Add behavior for our MEX endpoint   
            ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
            behavior.HttpGetEnabled = true;
            host.Description.Behaviors.Add(behavior);

            host.AddServiceEndpoint(serviceType, new BasicHttpBinding(), "MockSensorService");
            // Add MEX endpoint at http://localhost:8080/MEX/
            host.AddServiceEndpoint(typeof(IMetadataExchange), new BasicHttpBinding(), "MEX");

            host.Open();
        }

        protected override void ProcessData()
        {
            base.ProcessData();
        }
    }
}
