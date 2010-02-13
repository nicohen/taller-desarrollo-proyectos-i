using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZoosManagmentSystem.Core.Foundation;
using ZoosManagementSystem.Core.MockEnvironmentActionsService;
using ZoosManagementSystem.Core.MockEnvironmentConditionsService;
using System.Threading;
using ZoosManagementSystem.Core.Storage;
using ZoosManagmentSystem.Core.Foundation.Sensor;

namespace ZoosManagmentSystem.Core.Switch.Sensor
{
    public class MockSensorManager : SensorManager
    {
        #region Fields

        EnvironmentActionsServiceClient environmentActionsClient;
        EnvironmentConditionsServiceClient environmentConditionsServiceClient;
        DbHelper dbHelper;
        Timer poolingTimer;

        #endregion

        #region Methods

        #region SensorManager Members

        public MockSensorManager(string name, int actionExecutionDelay, Guid environmentId)
            : base(name, actionExecutionDelay, environmentId)
        {
            this.environmentActionsClient = new EnvironmentActionsServiceClient();
            this.environmentConditionsServiceClient = new EnvironmentConditionsServiceClient();
        }

        public override void Start()
        {
            try
            {
                this.dbHelper = new DbHelper();
                this.poolingTimer = new Timer(new TimerCallback(this.PoolEnvironment), null, 0, this.ActionExecutionDelay);
            }
            catch (Exception)
            {

            }
        }

        public override void Stop()
        {
            base.Stop();
            this.poolingTimer.Dispose();
        }

        #endregion

        protected override void CheckEnvironmentAndExecute()
        {
            TimeSlot currentTimeSlot;

            currentTimeSlot = this.GetCurrentTimeSlot();

            if (currentTimeSlot != null)
            {
                EnvironmentConditions environmentConditions = this.environmentConditionsServiceClient.GetEnvironmentConditions(this.environmentId);

                try
                {
                    this.dbHelper.UpdateEnvironmentMeasures(this.environmentId, environmentConditions.Humidity, environmentConditions.Temperature, environmentConditions.Luminosity);
                    if (environmentConditions.Humidity >= currentTimeSlot.HumidityMax)
                    {
                        this.environmentActionsClient.StartWatering(this.environmentId);
                    }

                    if ((environmentConditions.Humidity >= currentTimeSlot.HumidityMin && environmentConditions.Humidity <= currentTimeSlot.HumidityMax) || (environmentConditions.Humidity < currentTimeSlot.HumidityMin))
                    {
                        this.environmentActionsClient.StopWatering(this.environmentId);
                    }

                    if (environmentConditions.Luminosity <= currentTimeSlot.LuminosityMin || environmentConditions.Luminosity >= currentTimeSlot.LuminosityMax)
                    {
                        this.environmentActionsClient.ModifyLuminosity(this.environmentId, (float)(currentTimeSlot.LuminosityMin + currentTimeSlot.LuminosityMax) / 2);
                    }

                    if (environmentConditions.Temperature <= currentTimeSlot.TemperatureMin || environmentConditions.Temperature >= currentTimeSlot.TemperatureMax)
                    {
                        this.environmentActionsClient.ModifyTemperature(this.environmentId, (float)(currentTimeSlot.TemperatureMin + currentTimeSlot.TemperatureMax) / 2);
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void PoolEnvironment(object state)
        {
            this.CheckEnvironmentAndExecute();
        }

        #endregion
    }
}
