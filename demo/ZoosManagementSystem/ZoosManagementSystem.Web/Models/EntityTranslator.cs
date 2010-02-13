namespace ZoosManagementSystem.Web.Models
{
    using System.Linq;
    using ZoosManagementSystem.Web.ViewData;

    public static class EntityTranslator
    {
        public static EnvironmentViewData ToViewData(this Environment environmentModel, IZooCatalogRepository repository)
        {
            return new EnvironmentViewData
                {
                    EnvironmentId = environmentModel.Id.ToString(),
                    Name = environmentModel.Name,
                    Description = environmentModel.Description,
                    Surface = environmentModel.Surface,
                    Type = environmentModel.Type,
                    Animals = environmentModel.Animal.Select(a => a.ToViewData()).ToList(),
                    TimeSlots = environmentModel.TimeSlot.Select(ts => ts.ToViewData()).ToList(),
                };
        }

        public static AnimalViewData ToViewData(this Animal animalModel)
        {
            return new AnimalViewData
                {
                    AnimalId = animalModel.Id.ToString(),
                    Name = animalModel.Name,
                    Species = animalModel.Species,
                    Sex = (animalModel.Sex.ToLowerInvariant()) == "M" ? "Macho" : "Hembra",
                    AnimalStatus = "Original"
                };
        }

        public static TimeSlotViewData ToViewData(this TimeSlot timeSlotModel)
        {
            return new TimeSlotViewData
                {
                    TimeSlotId = timeSlotModel.Id.ToString(),
                    InitialTime = timeSlotModel.InitialTime.ToString(),
                    FinalTime = timeSlotModel.FinalTime.ToString(),
                    TemperatureMin = timeSlotModel.TemperatureMin,
                    TemperatureMax = timeSlotModel.TemperatureMax,
                    HumidityMin = timeSlotModel.HumidityMin,
                    HumidityMax = timeSlotModel.HumidityMax,
                    LuminosityMin = timeSlotModel.LuminosityMin,
                    LuminosityMax = timeSlotModel.LuminosityMax,
                    TimeSlotStatus = "Original"
                };
        }
    }
}
