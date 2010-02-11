using System.ServiceProcess;
using ZoosManagmentSystem.Core.Switch;
using ZoosManagementSystem.Core.Storage;
using System.Collections.Generic;
using ZoosManagmentSystem.Core.Foundation;

namespace ZoosManagementSystem.Core
{
    public partial class ZoosManagementSystemService : ServiceBase
    {
        private List<SensorManager> sensorManagers;
        private DbHelper dbHelper;
       
        public ZoosManagementSystemService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            this.LoadDataFromStorage();
            this.StartSensorManagers();
        }

        private void StartSensorManagers()
        {
            foreach (SensorManager sensorManager in this.sensorManagers)
            {
                sensorManager.Start();
            }
        }

        private void LoadDataFromStorage()
        {
            this.dbHelper = new DbHelper();

            List<Sensor> sensors = this.dbHelper.GetSensors();

            foreach (Sensor sensor in sensors)
            {
                this.sensorManagers.Add(new MockSensorManager(sensor.Name, 3000, sensor.Environment.Id));
            }
        }

        protected override void OnStop()
        {
        }
    }
}
