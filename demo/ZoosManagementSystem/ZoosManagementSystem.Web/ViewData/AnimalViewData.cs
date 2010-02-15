namespace ZoosManagementSystem.Web.ViewData
{
    using System.Collections.Generic;

    public class AnimalViewData
    {
        public string AnimalId
        { get; set; }

        public string ResponsibleId
        { get; set; }

        public string EnvironmentId
        { get; set; }

        public string AnimalStatus
        { get; set; }

        public string Name
        { get; set; }

        public string Species
        { get; set; }

        public string Sex
        { get; set; }

        public string BirthDate
        { get; set; }

        public int? Cost
        { get; set; }

        public bool BornInCaptivity
        { get; set; }

        public string NextHealthMeasure
        { get; set; }

        public IList<FeedingTimeViewData> FeedingTimes
        { get; set; }

        public IList<HealthMeasureViewData> HealthMeasures
        { get; set; }

        public IList<FeedingViewData> FeedingsAvailable
        { get; set; }

        public IList<ResponsibleViewData> ResponsiblesAvailable
        { get; set; }

        public IList<EnvironmentViewData> EnvironmentsAvailable
        { get; set; }
    }
}