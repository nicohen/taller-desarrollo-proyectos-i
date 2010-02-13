using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Threading;

using ZoosManagementSystem.Core.Foundation.Service;
using ZoosManagementSystem.Core.Storage;
using ZoosManagementSystem.Core.Util;

namespace ZoosManagementSystem.Core.Switch.Service
{
    public class MockAnimalHealthService : BaseService
    {
        private DbHelper dbHelper;
        private List<Animal> animals;

        public MockAnimalHealthService(int poolingInterval)
            : base(poolingInterval)
        {
            this.animals = new List<Animal>();
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
                try
                {
                    foreach (Animal animalToMeasure in this.animals)
                    {
                        if (DateTimeComparer.CompareDate(DateTime.Now, animalToMeasure.NextHealthMeasure) == 0)
                        {
                            Responsible responsible = this.dbHelper.GetResponsible(animalToMeasure.Responsible.Id);
                            this.SendNotificationEmail(responsible, animalToMeasure, animalToMeasure.NextHealthMeasure);
                        }
                    }

                    Thread.Sleep(this.poolingInterval);
                }
                catch (Exception)
                {
                }
            }
        }

        protected override void OnStop()
        {

        }

        private void LoadDataFromStorage()
        {
            try
            {
                this.animals = this.dbHelper.GetAllAnimals();
            }
            catch (Exception)
            {
            }
        }

        private void SendNotificationEmail(Responsible responsible, Animal animal, DateTime healthMeasureDate)
        {
            string notificationSubject = String.Format("Health Measure notification. Date: [{0}] - Animal '{1}'.", healthMeasureDate, animal.Id);
            string notificationBody = string.Empty;

            MailMessage message = new MailMessage("from@address", responsible.Email, notificationSubject, notificationBody);

            SmtpClient mailClient = new SmtpClient("Email Server Name");
            mailClient.Send(message);
        }
    }
}
