using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace ZooApplicationService.Switch.Sensor.MockSensor
{
    [ServiceContract]
    public class MockSensorService
    {
        [OperationContract]
        public string ProcessData(string sensorName, string data)
        {
            return null;
        }
    }
}
