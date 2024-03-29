﻿namespace ZoosManagementSystem.Web.ViewData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EnvironmentViewData
    {
        public EnvironmentViewData()
        {
            this.Animals = new List<AnimalViewData>();
            this.FreeAnimals = new List<AnimalViewData>();
            this.TimeSlots = new List<TimeSlotViewData>();
        }

        public string EnvironmentId
        { get; set; }

        public string Name
        { get; set; }

        public string Description
        { get; set; }

        public int Surface
        { get; set; }

        public string Type
        { get; set; }

        public IList<AnimalViewData> Animals
        { get; set; }

        public IList<TimeSlotViewData> TimeSlots
        { get; set; }

        public IList<AnimalViewData> FreeAnimals
        { get; set; }
    }
}
