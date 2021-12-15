using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace Fifth_task
{
    public class PDFWriter
    {
        /// <summary>
        /// Writes in the pdf file grouped books by genre of each subscriber
        /// </summary>
        /// <param name="path"></param>
        /// <param name="subcribersWithBooksGroupedByGenre"></param>
        public static void WriteSubscribersAndBooksGroupedByGenre(string path, Dictionary<SubscriberDto, IEnumerable<IGrouping<string, BookDto>>> subcribersWithBooksGroupedByGenre)
        {

            iTextSharp.text.Document odoc = new iTextSharp.text.Document();
            iTextSharp.text.pdf.PdfWriter.GetInstance(odoc, new FileStream(path, FileMode.Create));
            odoc.Open();
            odoc.Add(new Paragraph("Subscriber Name\tSubscriber Last Name\tGenre\tName of thebook\tAuthor of the book"));
            foreach (var elem in subcribersWithBooksGroupedByGenre)
            {
                odoc.Add(new Paragraph($"{elem.Key.FirstName}\t{elem.Key.LastName}"));
                foreach (IGrouping<string, BookDto> valueElem in elem.Value)
                {
                    odoc.Add(new Paragraph($"\t\t{valueElem.Key}"));
                    foreach (var book in valueElem)
                    {
                        odoc.Add(new Paragraph($"\t\t\t{book.Title}\t{book.Author}"));
                    }
                }
            }
            odoc.Close();

        }
        /// <summary>
        /// Writes usage of each book in the pdf file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="groupedBooks"></param>
        public static void WriteHowManyTimesBooksWasTaken(string path, IEnumerable<BookWithUsageDto> groupedBooks)
        {
            iTextSharp.text.Document odoc = new iTextSharp.text.Document();
            iTextSharp.text.pdf.PdfWriter.GetInstance(odoc, new FileStream(path, FileMode.Create));
            odoc.Open();
            odoc.Add(new Paragraph($"Title\tAuthor\tGenre\tUsage"));
            foreach (var elem in groupedBooks)
            {
                odoc.Add(new Paragraph($"{elem.Book.Title}\t{elem.Book.Author}\t{elem.Book.Genre}\t{elem.Count}"));
            }
            odoc.Close();

        }
    }
}
