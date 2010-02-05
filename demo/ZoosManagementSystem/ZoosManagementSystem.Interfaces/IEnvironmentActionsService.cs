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
        void StartWatering(Guid environmentId);

        [OperationContract]
        void StopWatering(Guid environmentId);

        [OperationContract]
        void FeedingAnimal(Guid environmentId, Guid animalId, Guid feedingId, int amount);
    }
}
