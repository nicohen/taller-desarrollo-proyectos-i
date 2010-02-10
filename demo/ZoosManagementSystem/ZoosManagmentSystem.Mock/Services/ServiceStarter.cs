using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;

using ZoosManagementSystem.Interfaces;

namespace ZoosManagmentSystem.Mock.Services
{
    public class ServiceStarter
    {
        public static void StartEnvironmentActionsService()
        {
            Type serviceType = typeof(MockEnvironmentActionsService);

            ServiceHost host = new ServiceHost(serviceType, new Uri[] { new Uri("http://localhost:8081/") });

            ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
            behavior.HttpGetEnabled = true;
            host.Description.Behaviors.Add(behavior);

            host.AddServiceEndpoint(typeof(IEnvironmentActionsService), new BasicHttpBinding(), "MockEnvironmentActionsService");
            host.AddServiceEndpoint(typeof(IMetadataExchange), new BasicHttpBinding(), "MEX");

            host.Open();

        }

        public static void StartEnvironmentConditionsService()
        {
            Type serviceType = typeof(MockEnvironmentConditionsService);

            ServiceHost host = new ServiceHost(serviceType, new Uri[] { new Uri("http://localhost:8080/") });

            ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
            behavior.HttpGetEnabled = true;
            host.Description.Behaviors.Add(behavior);

            host.AddServiceEndpoint(typeof(IEnvironmentConditionsService), new BasicHttpBinding(), "MockEnvironmentConditionsService");
            host.AddServiceEndpoint(typeof(IMetadataExchange), new BasicHttpBinding(), "MEX");
            host.Open();
        }
    }
}
