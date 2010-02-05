namespace ZoosManagementSystem.Interfaces
{
    using System;
    using System.ServiceModel;
    using ZoosManagementSystem.Interfaces.DataContracts;

    [ServiceContract]
    public interface IEnvironmentConditionsService
    {
        [OperationContract]
        EnvironmentConditions GetEnvironmentConditions(Guid environmentId);
    }
}
