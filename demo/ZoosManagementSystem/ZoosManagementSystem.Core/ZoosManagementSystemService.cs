using System.ServiceProcess;
using ZoosManagmentSystem.Core.Switch;
using ZoosManagementSystem.Core.Storage;
using System.Collections.Generic;
using ZoosManagmentSystem.Core.Foundation;
using ZoosManagmentSystem.Core.Switch.Sensor;
using ZoosManagmentSystem.Core.Foundation.Sensor;
using ZoosManagementSystem.Core.Switch.Service;
using System;
using System.Threading;
using System.Diagnostics;
using System.Configuration;

namespace ZoosManagementSystem.Core
{
    public partial class ZoosManagementSystemService : ServiceBase
    {
        private List<SensorManager> sensorManagers;
        private DbHelper dbHelper;

        private const string MockSensorPoolingTimeConfigurationKey = "MockSensorPoolingTime";
        private const string AnimalHealthPoolingTimeConfigurationKey = "AnimalHealthPoolingTime";
        private const string FeedingServicePoolingTimeConfigurationKey = "FeedingServicePoolingTime";

        public ZoosManagementSystemService()
        {
            InitializeComponent();
            this.sensorManagers = new List<SensorManager>();
        }

        protected override void OnStart(string[] args)
        {
#if DEBUG
            while (!Debugger.IsAttached)      // Waiting until debugger is attached
            {
                RequestAdditionalTime(8000);  // Prevents the service from timeout
                Thread.Sleep(7000);           // Gives you time to attach the debugger   
            }
            RequestAdditionalTime(20000);     // for Debugging the OnStart method,
            // increase as needed to prevent timeouts
#endif

            this.LoadDataFromStorage();
            this.StartSensorManagers();

            Thread feedingServiceThread = new Thread(new ThreadStart(this.StartFeedingService));
            Thread animalHealthServiceThread = new Thread(new ThreadStart(this.StartAnimalHealthService));

            feedingServiceThread.Start();
            animalHealthServiceThread.Start();
        }

        private void StartSensorManagers()
        {
            foreach (SensorManager sensorManager in this.sensorManagers)
            {
                sensorManager.Start();
            }
        }

        private void StartFeedingService()
        {
            MockFeedingService mockFeedingService = new MockFeedingService(int.Parse(ConfigurationManager.AppSettings[ZoosManagementSystemService.FeedingServicePoolingTimeConfigurationKey]));
            try
            {
                mockFeedingService.Initialize();
                mockFeedingService.Start();
            }
            catch (Exception)
            {

            }
        }

        private void StartAnimalHealthService()
        {
            MockAnimalHealthService animalHealthService = new MockAnimalHealthService(int.Parse(ConfigurationManager.AppSettings[ZoosManagementSystemService.AnimalHealthPoolingTimeConfigurationKey]));
            try
            {
                animalHealthService.Initialize();
                animalHealthService.Start();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void LoadDataFromStorage()
        {
            this.dbHelper = new DbHelper();

            List<Sensor> sensors = this.dbHelper.GetAllSensors();

            foreach (Sensor sensor in sensors)
            {
                SensorManager manager = new MockSensorManager(sensor.Name, int.Parse(ConfigurationManager.AppSettings[ZoosManagementSystemService.MockSensorPoolingTimeConfigurationKey]), sensor.Environment.Id);

                this.sensorManagers.Add(manager);
            }
        }

        protected override void OnStop()
        {
        }
    }
}
