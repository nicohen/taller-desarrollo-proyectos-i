using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using ZoosManagementSystem.Core.Foundation.Service;
using ZoosManagementSystem.Core.Storage;
using ZoosManagementSystem.Core.MockEnvironmentActionsService;
using Environment = ZoosManagementSystem.Core.Storage.Environment;
using ZoosManagementSystem.Core.Util;

namespace ZoosManagementSystem.Core.Switch.Service
{
    public class MockFeedingService : BaseService
    {
        #region Fields

        DbHelper dbHelper;
        Dictionary<Guid, List<FeedingTime>> environmentFeedingTimes;
        List<Timer> feedingTimers;
        Timer dbUpdateTimer;
        EnvironmentActionsServiceClient environmentActionsServiceClient;

        #endregion

        #region Methods

        #region IService Members

        public MockFeedingService(int poolingInterval)
            : base(poolingInterval)
        {
            this.dbHelper = new DbHelper();
            this.environmentActionsServiceClient = new EnvironmentActionsServiceClient();
            this.feedingTimers = new List<Timer>();
        }

        public override void Initialize()
        {
            this.dbHelper = new DbHelper();
            this.LoadDataFromStorage();
        }

        protected override void OnStart()
        {
            while (this.running)
            {
                lock (this.environmentFeedingTimes)
                {
                    foreach (List<FeedingTime> feedingTimeList in this.environmentFeedingTimes.Values)
                    {
                        foreach (FeedingTime feedingTime in feedingTimeList)
                        {
                            if (DateTimeComparer.CompareDate(DateTime.Now, feedingTime.Time, 10) == 0)
                            {
                                this.FeedAnimal(feedingTime);
                            }
                        }
                    }
                }

                Thread.Sleep(poolingInterval);
            }


            this.dbUpdateTimer = new Timer(new TimerCallback(this.DbUpdateTimerCallback), null, 10000, 0);
        }

        protected override void OnStop()
        {
            foreach (Timer timer in this.feedingTimers)
            {
                timer.Dispose();
            }

            this.dbUpdateTimer.Dispose();
        }

        #endregion

        private void LoadDataFromStorage()
        {
            lock (this.environmentFeedingTimes)
            {
                this.environmentFeedingTimes = new Dictionary<Guid, List<FeedingTime>>();

                List<Environment> environments = this.dbHelper.GetEnvironments();

                foreach (Environment environment in environments)
                {
                    List<Animal> environmentAnimals = this.dbHelper.GetAnimalsFromEnvironment(environment.Id);

                    foreach (Animal animal in environmentAnimals)
                    {
                        List<FeedingTime> feedingTimes = this.dbHelper.GetFeedingTimesFromAnimal(animal.Id);

                        this.environmentFeedingTimes.Add(environment.Id, feedingTimes);
                    }
                }
            }
        }

        private void DbUpdateTimerCallback(object state)
        {
            try
            {
                this.LoadDataFromStorage();
            }
            catch (Exception)
            {
            }
        }

        private void FeedAnimal(FeedingTime feedingTime)
        {
            //TODO: guarda aca, no creo que EF mapée feedingTime.Animal.Environment.Id
            this.environmentActionsServiceClient.FeedingAnimal(feedingTime.Animal.Environment.Id, feedingTime.Animal.Id, feedingTime.Feeding.Id, feedingTime.Amount);
        }

        #endregion
    }
}
