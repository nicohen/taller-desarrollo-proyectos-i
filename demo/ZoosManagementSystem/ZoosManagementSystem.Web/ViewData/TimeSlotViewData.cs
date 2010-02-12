namespace ZoosManagementSystem.Web.ViewData
{
    public class TimeSlotViewData
    {
        public string TimeSlotId
        { get; set; }

        public string TimeSlotStatus
        { get; set; }

        public string InitialTime
        { get; set; }

        public string FinalTime
        { get; set; }

        public double? TemperatureMin
        { get; set; }

        public double? TemperatureMax
        { get; set; }

        public double? HumidityMin
        { get; set; }

        public double? HumidityMax
        { get; set; }

        public double? LuminosityMin
        { get; set; }

        public double? LuminosityMax
        { get; set; }
    }
}