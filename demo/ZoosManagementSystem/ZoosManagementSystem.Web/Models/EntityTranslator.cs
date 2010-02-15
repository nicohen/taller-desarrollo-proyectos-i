namespace ZoosManagementSystem.Web.Models
{
    using System.Globalization;
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
                    FreeAnimals = repository.GetFreeAnimals().Select(a => a.ToViewData()).ToList()
                };
        }

        public static AnimalViewData ToViewData(this Animal animalModel)
        {
            return new AnimalViewData
                {
                    AnimalId = animalModel.Id.ToString(),
                    Name = animalModel.Name,
                    Species = animalModel.Species,
                    Sex = (animalModel.Sex.ToLowerInvariant() == "m") ? "Macho" : "Hembra",
                    AnimalStatus = "Original"
                };
        }

        public static TimeSlotViewData ToViewData(this TimeSlot timeSlotModel)
        {
            return new TimeSlotViewData
                {
                    TimeSlotId = timeSlotModel.Id.ToString(),
                    InitialTime = string.Format(CultureInfo.CurrentCulture, "{0}:{1}", timeSlotModel.InitialTime.Hours.ToString("D2"), timeSlotModel.InitialTime.Minutes.ToString("D2")),
                    FinalTime = string.Format(CultureInfo.CurrentCulture, "{0}:{1}", timeSlotModel.FinalTime.Hours.ToString("D2"), timeSlotModel.FinalTime.Minutes.ToString("D2")),
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
