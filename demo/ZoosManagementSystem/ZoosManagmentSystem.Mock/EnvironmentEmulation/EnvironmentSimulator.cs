using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoosManagmentSystem.Mock.Storage;
using System.Threading;

namespace ZoosManagmentSystem.Mock.EnvironmentEmulation
{
    public class EnvironmentSimulator
    {
        private static Dictionary<Guid, EnvironmentMeasure> environmentsMeasures;

        public static void Initialize(List<ZoosManagmentSystem.Mock.Storage.Environment> environments, float humidity, float temperature, float luminosity)
        {
            EnvironmentSimulator.environmentsMeasures = new Dictionary<Guid, EnvironmentMeasure>();

            foreach (ZoosManagmentSystem.Mock.Storage.Environment environment in environments)
            {
                EnvironmentMeasure startMeasures = new EnvironmentMeasure()
                {
                    Humidity = humidity,
                    Temperature = temperature,
                    Luminosity = luminosity
                };

                EnvironmentSimulator.environmentsMeasures.Add(environment.Id, startMeasures);
            }
        }

        public static EnvironmentMeasure GetEnvironmentMeasures(Guid environmentId)
        {
            return EnvironmentSimulator.environmentsMeasures[environmentId];
        }

        public static void SetTemperature(Guid environmentId, float temperatureOffset)
        {
            double environmentConditionValue = EnvironmentSimulator.GetEnvironmentMeasures(environmentId).Temperature;

            EnvironmentChangeItem item = new EnvironmentChangeItem(environmentId, temperatureOffset, environmentConditionValue);

            ThreadPool.QueueUserWorkItem(new WaitCallback(EnvironmentSimulator.SetTemperatureThread), item);
        }

        private static void SetTemperatureThread(object state)
        {
            EnvironmentChangeItem environmentChangeItem = (EnvironmentChangeItem)state;

            double firstIncrement, secondIncrement, thirdIncrement;

            Thread.Sleep(2000);
            firstIncrement = (environmentChangeItem.EnvironmentConditionValue + environmentChangeItem.Offset) / 2;
            EnvironmentSimulator.environmentsMeasures[environmentChangeItem.EnvironmentId].Temperature = firstIncrement;


            Thread.Sleep(2000);
            secondIncrement = (firstIncrement + environmentChangeItem.Offset) / 2;
            EnvironmentSimulator.environmentsMeasures[environmentChangeItem.EnvironmentId].Temperature = secondIncrement;

            Thread.Sleep(2000);
            thirdIncrement = (secondIncrement + environmentChangeItem.Offset) / 2;
            EnvironmentSimulator.environmentsMeasures[environmentChangeItem.EnvironmentId].Temperature = thirdIncrement;

            Thread.Sleep(2000);
            EnvironmentSimulator.environmentsMeasures[environmentChangeItem.EnvironmentId].Temperature = environmentChangeItem.Offset;
        }

        public static void SetLuminosity(Guid environmentId, float luminosityOffset)
        {
            double environmentConditionValue = EnvironmentSimulator.GetEnvironmentMeasures(environmentId).Luminosity;

            EnvironmentChangeItem item = new EnvironmentChangeItem(environmentId, luminosityOffset, environmentConditionValue);

            ThreadPool.QueueUserWorkItem(new WaitCallback(EnvironmentSimulator.SetLuminosityThread), item);
        }

        public static void SetHumidity(Guid environmentId, float humidityOffset)
        {
            double environmentConditionValue = EnvironmentSimulator.GetEnvironmentMeasures(environmentId).Humidity;

            EnvironmentChangeItem item = new EnvironmentChangeItem(environmentId, humidityOffset, environmentConditionValue);

            ThreadPool.QueueUserWorkItem(new WaitCallback(EnvironmentSimulator.SetHumidityThread), item);
        }

        private static void SetLuminosityThread(object state)
        {
            EnvironmentChangeItem environmentChangeItem = (EnvironmentChangeItem)state;

            double firstIncrement, secondIncrement, thirdIncrement;

            Thread.Sleep(2000);
            firstIncrement = (environmentChangeItem.EnvironmentConditionValue + environmentChangeItem.Offset) / 2;
            EnvironmentSimulator.environmentsMeasures[environmentChangeItem.EnvironmentId].Luminosity = firstIncrement;


            Thread.Sleep(2000);
            secondIncrement = (firstIncrement + environmentChangeItem.Offset) / 2;
            EnvironmentSimulator.environmentsMeasures[environmentChangeItem.EnvironmentId].Luminosity = secondIncrement;

            Thread.Sleep(2000);
            thirdIncrement = (secondIncrement + environmentChangeItem.Offset) / 2;
            EnvironmentSimulator.environmentsMeasures[environmentChangeItem.EnvironmentId].Luminosity = thirdIncrement;

            Thread.Sleep(2000);
            EnvironmentSimulator.environmentsMeasures[environmentChangeItem.EnvironmentId].Luminosity = environmentChangeItem.Offset;
        }

        private static void SetHumidityThread(object state)
        {
            EnvironmentChangeItem environmentChangeItem = (EnvironmentChangeItem)state;
            double firstIncrement, secondIncrement, thirdIncrement;

            Thread.Sleep(2000);
            firstIncrement = (environmentChangeItem.EnvironmentConditionValue + environmentChangeItem.Offset) / 2;
            EnvironmentSimulator.environmentsMeasures[environmentChangeItem.EnvironmentId].Humidity = firstIncrement;


            Thread.Sleep(2000);
            secondIncrement = (firstIncrement + environmentChangeItem.Offset) / 2;
            EnvironmentSimulator.environmentsMeasures[environmentChangeItem.EnvironmentId].Humidity = secondIncrement;

            Thread.Sleep(2000);
            thirdIncrement = (secondIncrement + environmentChangeItem.Offset) / 2;
            EnvironmentSimulator.environmentsMeasures[environmentChangeItem.EnvironmentId].Humidity = thirdIncrement;

            EnvironmentSimulator.environmentsMeasures[environmentChangeItem.EnvironmentId].Humidity = environmentChangeItem.Offset;
        }
    }

    public class EnvironmentChangeItem
    {
        private double environmentConditionValue;

        public double EnvironmentConditionValue
        {
            get { return environmentConditionValue; }
            set { environmentConditionValue = value; }
        }
        private Guid environmentId;

        public Guid EnvironmentId
        {
            get { return environmentId; }
            set { environmentId = value; }
        }
        private float offset;

        public float Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        public EnvironmentChangeItem(Guid environmentId, float offset, double environmentConditionValue)
        {
            this.environmentId = environmentId;
            this.offset = offset;
            this.environmentConditionValue = environmentConditionValue;
        }


    }
}
