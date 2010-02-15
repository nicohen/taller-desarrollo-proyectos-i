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
                    Animals = environmentModel.Animal.Select(a => a.ToViewData(repository)).ToList(),
                    TimeSlots = environmentModel.TimeSlot.Select(ts => ts.ToViewData()).ToList(),
                    FreeAnimals = repository.GetFreeAnimals().Select(a => a.ToViewData(repository)).ToList()
                };
        }

        public static AnimalViewData ToViewData(this Animal animalModel, IZooCatalogRepository repository)
        {
            return new AnimalViewData
                {
                    AnimalId = animalModel.Id.ToString(),
                    EnvironmentId = (animalModel.Environment != null) ? animalModel.Environment.Id.ToString() : string.Empty,
                    ResponsibleId = (animalModel.Responsible != null) ? animalModel.Responsible.Id.ToString() : string.Empty,
                    Name = animalModel.Name,
                    Species = animalModel.Species,
                    Sex = (animalModel.Sex.ToLowerInvariant() == "m") ? "Macho" : "Hembra",
                    BirthDate = animalModel.BirthDate.ToString("yyyy/MM/dd"),
                    NextHealthMeasure = animalModel.NextHealthMeasure.ToString("yyyy/MM/dd"),
                    BornInCaptivity = animalModel.BornInCaptivity,
                    Cost = animalModel.Cost,
                    FeedingTimes = animalModel.FeedingTime.Select(ft => ft.ToViewData()).ToList(),
                    HealthMeasures = animalModel.HealthMeasure.Select(hm => hm.ToViewData(repository)).ToList(),
                    FeedingsAvailable = repository.GetFeedings().Select(f => f.ToViewData()).ToList(),
                    ResponsiblesAvailable = repository.GetResponsibles().Select(r => r.ToViewData()).ToList(),
                    EnvironmentsAvailable = repository.GetEnvironments().Select(env => new EnvironmentViewData
                        {
                            EnvironmentId = env.Id.ToString(),
                            Name = env.Name,
                            Surface = env.Surface
                        }).ToList(),
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

        public static FeedingTimeViewData ToViewData(this FeedingTime feedingTimeModel)
        {
            var feedingTimeViewData = new FeedingTimeViewData
                {
                    FeedingTimeId = feedingTimeModel.Id.ToString(),
                    AnimalId = feedingTimeModel.Animal.Id.ToString(),
                    Amount = feedingTimeModel.Amount,
                    Time = string.Format(CultureInfo.CurrentCulture, "{0}:{1}", feedingTimeModel.Time.Hours.ToString("D2"), feedingTimeModel.Time.Minutes.ToString("D2")),
                    FeedingTimeStatus = "Original"
                };

            if (feedingTimeModel.FeedingReference.IsLoaded)
            {
                feedingTimeViewData.FeedingId = feedingTimeModel.Feeding.Id.ToString();
                feedingTimeViewData.FeedingName = feedingTimeModel.Feeding.Name;
            }

            return feedingTimeViewData;
        }

        public static HealthMeasureViewData ToViewData(this HealthMeasure healthMeasureModel, IZooCatalogRepository repository)
        {
            return new HealthMeasureViewData
                {
                    HealthMeasureId = healthMeasureModel.Id.ToString(),
                    AnimalId = healthMeasureModel.Animal.Id.ToString(),
                    MeasurementDate = healthMeasureModel.MeasurementDate.ToString("yyyy/MM/dd"),
                    Weight = healthMeasureModel.Weight,
                    Height = healthMeasureModel.Height,
                    Temperature = healthMeasureModel.Temperature,
                    Vaccine = healthMeasureModel.Vaccine,
                    Notes = healthMeasureModel.Notes,
                    AnimalsAvailable = repository.GetAnimals().Select(a => new AnimalViewData
                        {
                            AnimalId = a.Id.ToString(),
                            Name = a.Name,
                            Species = a.Species,
                            Sex = (a.Sex.ToLowerInvariant() == "m") ? "Macho" : "Hembra",
                        }).ToList(),
                    HealthMeasureStatus = "Original"
                };
        }

        public static FeedingViewData ToViewData(this Feeding feedingModel)
        {
            return new FeedingViewData
                {
                    FeedingId = feedingModel.Id.ToString(),
                    Name = feedingModel.Name
                };
        }

        public static ResponsibleViewData ToViewData(this Responsible responsibleModel)
        {
            return new ResponsibleViewData
                {
                    ResponsibleId = responsibleModel.Id.ToString(),
                    Name = responsibleModel.Name,
                    LastName = responsibleModel.LastName,
                    Email = responsibleModel.Email
                };
        }
    }
}
