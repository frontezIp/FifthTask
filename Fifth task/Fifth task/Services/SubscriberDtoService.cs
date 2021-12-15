using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifth_task
{
    public class SubscriberDtoService
    {
        private static DAOFactory _daoCreate = new DAOFactory();

        /// <summary>
        /// Gets all subscribers from the db and transofrm them into DTOs
        /// </summary>
        /// <returns></returns>
        public static List<SubscriberDto> GetAllSubcribers()
        {

            List<Subscriber> subs = _daoCreate.Create<Subscriber>(DataBaseLauncher.StringConnection).GetAll();
            var linqRequest = from sub in subs
                              select new SubscriberDto
                              {
                                  FirstName = sub.FirstName,
                                  DateOfBirth = sub.DateOfBirth,
                                  LastName = sub.LastName,
                                  MiddleName = sub.MiddleName,
                                  Id = sub.Id,
                                  Sex = sub.Sex
                              };
            return linqRequest.ToList();

        }

        /// <summary>
        /// Return the most reading subscriber
        /// </summary>
        /// <returns></returns>
        public static string GetTheMostReadingSubscriber()
        {
            List<BookReportDto> bookReportDtos = BookReportDtoService.GetAllReports();

            var linqRequest = from report in bookReportDtos
                              group report by report.Subscriber  into g
                              select new { sub = g.Key, Count = g.Count() };
            Dictionary<SubscriberDto, int> subscribers = new Dictionary<SubscriberDto, int> { };
            foreach (var subscriber in linqRequest)
                subscribers.Add(subscriber.sub, subscriber.Count);
            SubscriberDto subscriberToReturn = subscribers.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
            return subscriberToReturn.FirstName + " " + subscriberToReturn.MiddleName + " " + subscriberToReturn.LastName;
        }


    }
}
