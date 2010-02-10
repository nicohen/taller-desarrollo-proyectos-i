using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZoosManagmentSystem.Mock.EnvironmentEmulation
{
    public class EnvironmentConditions
    {
        private static float currentTemperature;
        private static float currentHumidity;
        private static float currentLuminosity;

        public static float CurrentTemperature
        {
            get
            {
                return EnvironmentConditions.currentTemperature;
            }
            set
            {
                EnvironmentConditions.currentTemperature = value;
            }
        }

        public static float CurrentHumidity
        {
            get
            {
                return EnvironmentConditions.currentHumidity;
            }
            set
            {
                EnvironmentConditions.currentHumidity = value;
            }
        }

        public static float CurrentLuminosity
        {
            get
            {
                return EnvironmentConditions.currentLuminosity;
            }
            set
            {
                EnvironmentConditions.currentLuminosity = value;
            }
        }
    }
}
