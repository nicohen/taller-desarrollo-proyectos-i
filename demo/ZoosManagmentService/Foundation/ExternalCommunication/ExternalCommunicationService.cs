using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ZooApplicationService.Foundation.Service;

namespace ZooApplicationService.Foundation.ExternalCommunication
{
    [ServiceContract]
    public class ExternalCommunicationService
    {
        [OperationContract]
        public void StartSensors()
        {
            ZooService.StartSensorManagers();
        }

    }
}
