using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoosManagementSystem.Web.ViewData
{
    public class HealthMeasureViewData
    {
        public HealthMeasureViewData()
        {
            this.AnimalsAvailable = new List<AnimalViewData>();
        }

        public string HealthMeasureId
        { get; set; }

        public string AnimalId
        { get; set; }

        public string HealthMeasureStatus
        { get; set; }

        public string MeasurementDate
        { get; set; }

        public string Vaccine
        { get; set; }

        public string Notes
        { get; set; }

        public int Weight
        { get; set; }

        public int Height
        { get; set; }

        public double Temperature
        { get; set; }

        public IList<AnimalViewData> AnimalsAvailable
        { get; set; }
    }
}
