using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifth_task
{
    public class BookReportDtoService
    {
        private static DAOFactory _daoCreate = new DAOFactory();



        /// <summary>
        /// Gets all book reports from DB and transform them in DTO
        /// </summary>
        /// <returns></returns>
        public static List<BookReportDto> GetAllReports()
        {
            List<BookReport> bookReports = _daoCreate.Create<BookReport>(DataBaseLauncher.StringConnection).GetAll();
            List<BookDto> bookDtos = BookDtoService.GetAllBooks();
            List<SubscriberDto> subscriberDtos = SubscriberDtoService.GetAllSubcribers();
            List<BookReportDto> bookReportDtos = new List<BookReportDto>();
            var linqRequest = from report in bookReports
                              join book in bookDtos on report.BookId equals book.Id
                              join subscriber in subscriberDtos on report.SubscriberId equals subscriber.Id
                              select new BookReportDto
                              {
                                  Id = report.Id,
                                  Book = book,
                                  StateOfBook = report.StateOfBook,
                                  Subscriber = subscriber,
                                  DateOfGiving = report.DateOfGiving,
                                  ReturnSatus = report.ReturnStatus
                              };
            bookReportDtos = linqRequest.ToList();
            return bookReportDtos;
        }

        /// <summary>
        /// Gets all reports in given timeline
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static List<BookReportDto> GetAllReportInCertainTimeLine(DateTime begin, DateTime end)
        {
            List<BookReportDto> reports = GetAllReports();
            var selectedReports = from report in reports
                                  where report.DateOfGiving > begin
                                  where report.DateOfGiving < end
                                  select report;
            return selectedReports.ToList();
        }

        /// <summary>
        /// Group books of each subscriber
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static Dictionary<SubscriberDto,IEnumerable<IGrouping<string, BookDto>>> GroupBooksOfEachSubscriberByGenre(DateTime begin, DateTime end)
        {
            List<BookReportDto> bookReportDtos = GetAllReportInCertainTimeLine(begin, end);
            Dictionary<SubscriberDto, List<BookDto>> subscriberBooks = new Dictionary<SubscriberDto, List<BookDto>> { };
            foreach(var report in bookReportDtos)
            {
                DictionaryHelper.AddToDictWithValueList(subscriberBooks, report.Subscriber, report.Book);
            }
            Dictionary<SubscriberDto,IEnumerable<IGrouping<string, BookDto>>> dictToReturn = new Dictionary<SubscriberDto,IEnumerable<IGrouping<string, BookDto>>> { };
            foreach(KeyValuePair<SubscriberDto,List<BookDto>> element in subscriberBooks)
            {
                dictToReturn[element.Key] = BookDtoService.GroupCertainBooksByGenre(element.Value);
            }
            return dictToReturn;
        }
    }
}
