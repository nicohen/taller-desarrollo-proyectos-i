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
            lock (EnvironmentSimulator.environmentsMeasures)
            {
                return EnvironmentSimulator.environmentsMeasures[environmentId];
            }
        }

        public static void SetTemperature(Guid environmentId, float temperatureOffset)
        {
            double firstIncrement, secondIncrement, thirdIncrement;
            double environmentConditionValue = EnvironmentSimulator.GetEnvironmentMeasures(environmentId).Luminosity;

            Thread.Sleep(2000);
            firstIncrement = (environmentConditionValue + temperatureOffset) / 2;
            EnvironmentSimulator.environmentsMeasures[environmentId].Temperature = firstIncrement;

            Thread.Sleep(2000);
            secondIncrement = (firstIncrement + temperatureOffset) / 2;
            EnvironmentSimulator.environmentsMeasures[environmentId].Temperature = secondIncrement;

            Thread.Sleep(2000);
            thirdIncrement = (secondIncrement + temperatureOffset) / 2;
            EnvironmentSimulator.environmentsMeasures[environmentId].Temperature = thirdIncrement;
        }

        public static void SetLuminosity(Guid environmentId, float luminosityOffset)
        {
            double firstIncrement, secondIncrement, thirdIncrement;
            double environmentConditionValue = EnvironmentSimulator.GetEnvironmentMeasures(environmentId).Luminosity;

            Thread.Sleep(2000);
            firstIncrement = (environmentConditionValue + luminosityOffset) / 2;
            EnvironmentSimulator.environmentsMeasures[environmentId].Luminosity = firstIncrement;


            Thread.Sleep(2000);
            secondIncrement = (firstIncrement + luminosityOffset) / 2;
            EnvironmentSimulator.environmentsMeasures[environmentId].Luminosity = secondIncrement;

            Thread.Sleep(2000);
            thirdIncrement = (secondIncrement + luminosityOffset) / 2;
            EnvironmentSimulator.environmentsMeasures[environmentId].Luminosity = thirdIncrement;
        }

        public static void SetHumidity(Guid environmentId, float humidityIncrement)
        {
            //double firstIncrement, secondIncrement, thirdIncrement;
            //double environmentConditionValue = EnvironmentSimulator.GetEnvironmentMeasures(environmentId).Luminosity;

            //Thread.Sleep(2000);
            //firstIncrement = (environmentConditionValue + luminosityOffset) / 2;
            //EnvironmentSimulator.environmentsMeasures[environmentId].Luminosity = firstIncrement;


            //Thread.Sleep(2000);
            //secondIncrement = (firstIncrement + luminosityOffset) / 2;
            //EnvironmentSimulator.environmentsMeasures[environmentId].Luminosity = secondIncrement;

            //Thread.Sleep(2000);
            //thirdIncrement = (secondIncrement + luminosityOffset) / 2;
            //EnvironmentSimulator.environmentsMeasures[environmentId].Luminosity = thirdIncrement;
        }
    }
}
