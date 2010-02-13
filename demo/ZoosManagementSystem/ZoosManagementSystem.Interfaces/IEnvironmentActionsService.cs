namespace ZoosManagementSystem.Interfaces
{
    using System.ServiceModel;
    using System;

    [ServiceContract]
    public interface IEnvironmentActionsService
    {
        [OperationContract]
        void ModifyTemperature(Guid environmentId, float temperatureOffset);

        [OperationContract]
        void ModifyLuminosity(Guid environmentId, float luminosityOffset);

        [OperationContract]
        void ModifyHumidity(Guid environmentId, float humidityOffset);

        [OperationContract]
        void FeedAnimal(Guid environmentId, Guid animalId, Guid feedingId, int amount);
    }
}
