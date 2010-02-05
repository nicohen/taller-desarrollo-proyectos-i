namespace ZoosManagementSystem.Interfaces.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class EnvironmentConditions
    {
        [DataMember]
        public Guid EnvironmentId
        { get; set; }

        [DataMember]
        public DateTime MeasureTime
        { get; set; }

        [DataMember]
        public float Temperature
        { get; set; }

        [DataMember]
        public float Humidity
        { get; set; }

        [DataMember]
        public float Luminosity
        { get; set; }
    }
}