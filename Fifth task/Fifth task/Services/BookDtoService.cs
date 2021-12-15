using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifth_task
{
    /// <summary>
    /// Service BookDtos
    /// </summary>
    public class BookDtoService
    {
        private static DAOFactory _daoCreate = new DAOFactory();

        /// <summary>
        /// Get all books from the data base
        /// </summary>
        /// <returns></returns>
        public static List<BookDto> GetAllBooks()
        {

            List<Book> books = _daoCreate.Create<Book>(DataBaseLauncher.StringConnection).GetAll();
            var linqRequest = from book in books
                              select new BookDto
                              {
                                  Author = book.Author,
                                  Genre = book.Genre,
                                  Id = book.Id,
                                  Title = book.Title,
                              };
            return linqRequest.ToList();

        }

        /// <summary>
        /// Finds the most popular author
        /// </summary>
        /// <returns></returns>
        public static string GetTheMostPopularAuthor()
        {
            List<BookReportDto> bookReportDtos = BookReportDtoService.GetAllReports();

            var linqRequest = from report in bookReportDtos
                              group report by report.Book.Author into g
                              select new { Author = g.Key, Count = g.Count() };
            Dictionary<string, int> authors = new Dictionary<string, int> { };
            foreach (var author in linqRequest)
                authors.Add(author.Author,author.Count);
            return authors.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
        }

        /// <summary>
        /// Finds the most popular genre
        /// </summary>
        /// <returns></returns>
        public static string GetTheMostPopularGenre()
        {
            List<BookReportDto> bookReportDtos = BookReportDtoService.GetAllReports();

            var linqRequest = from report in bookReportDtos
                              group report by report.Book.Genre into g
                              select new { Genre = g.Key, Count = g.Count() };
            Dictionary<string, int> books = new Dictionary<string, int> { };
            foreach (var book in linqRequest)
                books.Add(book.Genre, book.Count);
            return books.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
        }

        /// <summary>
        /// Group given books by genre
        /// </summary>
        /// <param name="books"></param>
        /// <returns></returns>
        public static IEnumerable<IGrouping<string,BookDto>> GroupCertainBooksByGenre(List<BookDto> books)
        {
            var groupedBooks = from book in books
                               group book by book.Genre;
            return groupedBooks;
        }

        /// <summary>
        /// Calculates usage of each book
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<BookWithUsageDto> CalculateHowManyTimesBooksWasTaken()
        {
            List<BookReportDto> reportDtos = new List<BookReportDto> { };
            reportDtos = BookReportDtoService.GetAllReports();
            var linqRequest = from report in reportDtos
                              group report by report.Book into book
                              select new BookWithUsageDto() { Book = book.Key, Count = book.Count() };
            return linqRequest;

        }

        /// <summary>
        /// Finds all books that need to be recovered
        /// </summary>
        /// <returns></returns>
        public static List<BookDto> BooksToRecover()
        {
            List<BookReportDto> bookReportDtos = new List<BookReportDto> { };
            bookReportDtos = BookReportDtoService.GetAllReports();
            var linqRequest = from report in bookReportDtos
                              where report.StateOfBook == "Bad"
                              select report.Book;
            return linqRequest.ToList();
        }

    }
}
