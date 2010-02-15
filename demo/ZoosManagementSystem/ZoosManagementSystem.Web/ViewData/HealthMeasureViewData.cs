using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoosManagementSystem.Web.ViewData
{
    public class HealthMeasureViewData
    {
        public string HealthMeasureId
        { get; set; }

        public string AnimalId
        { get; set; }

        public string HealthMeasureStatus
        { get; set; }

        public string MeasurementDate
        { get; set; }
    }
}
