using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Fifth_task
{
    public class TxtWriter
    {
        /// <summary>
        /// Writes in the text file grouped books by genre of each subscriber
        /// </summary>
        /// <param name="path"></param>
        /// <param name="subcribersWithBooksGroupedByGenre"></param>
        public static void WriteSubscribersAndBooksGroupedByGenre(string path, Dictionary<SubscriberDto, IEnumerable<IGrouping<string, BookDto>>> subcribersWithBooksGroupedByGenre)
        {

            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default)) 
            {
                sw.WriteLine("Subscriber Name\tSubscriber Last Name\tGenre\tName of thebook\tAuthor of the book");
                foreach(var elem in subcribersWithBooksGroupedByGenre)
                {
                    sw.WriteLine($"{elem.Key.FirstName}\t{elem.Key.LastName}");
                    foreach(IGrouping<string,BookDto> valueElem in elem.Value)
                    {
                        sw.WriteLine($"\t\t{valueElem.Key}");
                        foreach(var book in valueElem)
                        {
                            sw.WriteLine($"\t\t\t{book.Title}\t{book.Author}");
                        }
                    }
                }
                sw.Close();
            }
        }
        /// <summary>
        /// Writes usage of each book in the text file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="groupedBooks"></param>
        public static void WriteHowManyTimesBooksWasTaken(string path, IEnumerable<BookWithUsageDto> groupedBooks)
        {
            using(StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                sw.WriteLine($"Title\tAuthor\tGenre\tUsage");
                foreach(var elem in groupedBooks)
                {
                    sw.WriteLine($"{elem.Book.Title}\t{elem.Book.Author}\t{elem.Book.Genre}\t{elem.Count}");
                }
                sw.Close();
            }
        }
    }
}
