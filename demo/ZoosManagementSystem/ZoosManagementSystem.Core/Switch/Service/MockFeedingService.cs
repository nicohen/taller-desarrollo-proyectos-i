using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using ZoosManagementSystem.Core.Foundation.Service;
using ZoosManagementSystem.Core.Storage;
using ZoosManagementSystem.Core.MockEnvironmentActionsService;
using Environment = ZoosManagementSystem.Core.Storage.Environment;

namespace ZoosManagementSystem.Core.Switch.Service
{
    public class MockFeedingService : BaseService
    {
        DbHelper dbHelper;
        Dictionary<Guid, List<FeedingTime>> environmentFeedingTimes;
        List<Timer> feedingTimers;
        EnvironmentActionsServiceClient environmentActionsServiceClient;

        #region IService Members

        public MockFeedingService()
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

        protected override void OnStart()
        {
            lock (this.environmentFeedingTimes)
            {
                foreach (List<FeedingTime> feedingTimeList in this.environmentFeedingTimes.Values)
                {
                    foreach (FeedingTime feedingTime in feedingTimeList)
                    {
                        this.feedingTimers.Add(new Timer(new TimerCallback(this.FeedAnimal), feedingTime, feedingTime.Time.Ticks, 0));
                    }
                }
            }

            Timer dbUpdateTimer = new Timer(new TimerCallback(this.DbUpdateTimerCallback), null, 10000, 0);
        }

        protected override void OnStop()
        {
            foreach (Timer timer in this.feedingTimers)
            {
                timer.Dispose();
            }
        }

        private void FeedAnimal(object state)
        {
            FeedingTime feedingTime = (FeedingTime)state;
            //TODO: guarda aca, no creo que EF mapée feedingTime.Animal.Environment.Id
            this.environmentActionsServiceClient.FeedingAnimal(feedingTime.Animal.Environment.Id, feedingTime.Animal.Id, feedingTime.Feeding.Id, feedingTime.Amount);
        }

        #endregion
    }
}
