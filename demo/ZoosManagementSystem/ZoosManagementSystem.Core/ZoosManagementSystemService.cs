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

namespace ZoosManagementSystem.Core
{
    public partial class ZoosManagementSystemService : ServiceBase
    {
        private List<SensorManager> sensorManagers;
        private DbHelper dbHelper;

        public ZoosManagementSystemService()
        {
            InitializeComponent();
            this.sensorManagers = new List<SensorManager>();
        }

        protected override void OnStart(string[] args)
        {
            this.LoadDataFromStorage();
            this.StartSensorManagers();

            Thread feedingServiceThread = new Thread(new ThreadStart(this.StartFeedingService));
            Thread animalHealthServiceThread = new Thread(new ThreadStart(this.StartAnimalHealthService));
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
            MockFeedingService mockFeedingService = new MockFeedingService(900000);
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
            AnimalHealthService animalHealthService = new AnimalHealthService(900000);
            try
            {
                animalHealthService.Initialize();
                animalHealthService.Start();
            }
            catch (Exception)
            {

            }
        }

        private void LoadDataFromStorage()
        {
            this.dbHelper = new DbHelper();

            List<Sensor> sensors = this.dbHelper.GetAllSensors();

            foreach (Sensor sensor in sensors)
            {
                SensorManager manager = new MockSensorManager(sensor.Name, 3000, sensor.Environment.Id);

                this.sensorManagers.Add(manager);
            }
        }

        protected override void OnStop()
        {
        }
    }
}
