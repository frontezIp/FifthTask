using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Cells;
using System.IO;

namespace Fifth_task
{
    public class ExcelWriter
    {
        /// <summary>
        /// Writes in the excel file grouped books by genre of each subscriber
        /// </summary>
        /// <param name="path"></param>
        /// <param name="subcribersWithBooksGroupedByGenre"></param>
        public static void WriteSubscribersAndBooksGroupedByGenre(string path, Dictionary<SubscriberDto, IEnumerable<IGrouping<string, BookDto>>> subcribersWithBooksGroupedByGenre)
        {
            using (FileStream fstream = new FileStream(path, FileMode.Create))
            {
                int i = 2;
                Workbook wb = new Workbook(fstream);
                Worksheet worksheet = wb.Worksheets[0];
                worksheet.Cells[1, 1].PutValue("Subscriber Name");
                worksheet.Cells[1, 2].PutValue("Subscriber Last Name");
                worksheet.Cells[1, 3].PutValue("Genre");
                worksheet.Cells[1, 4].PutValue("Name of the book");
                worksheet.Cells[1, 5].PutValue("Author of the book");
                foreach(var elem in subcribersWithBooksGroupedByGenre)
                {
                    worksheet.Cells[i, 1].PutValue(elem.Key.FirstName);
                    worksheet.Cells[i, 2].PutValue(elem.Key.LastName);
                    i++;
                    foreach(IGrouping<string,BookDto> valueElem in elem.Value)
                    {
                        worksheet.Cells[i, 3].PutValue(valueElem.Key);
                        i++;
                        foreach(var book in valueElem)
                        {
                            worksheet.Cells[i, 4].PutValue(book.Title);
                            worksheet.Cells[i, 5].PutValue(book.Author);
                            i++;
                        }
                        
                    }
                }
                wb.Save(fstream,SaveFormat.Xlsx);
            }
        }
        /// <summary>
        /// Writes usage of each book in the excel file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="groupedBooks"></param>
        public static void WriteHowManyTimesBooksWasTaken(string path, IEnumerable<BookWithUsageDto> groupedBooks)
        {
            using (FileStream fstream = new FileStream(path, FileMode.Create))
            {
                int i = 2;
                Workbook wb = new Workbook(fstream);
                Worksheet worksheet = wb.Worksheets[0];
                worksheet.Cells[1, 1].PutValue("Title");
                worksheet.Cells[1, 2].PutValue("Author");
                worksheet.Cells[1, 3].PutValue("Genre");
                worksheet.Cells[1, 4].PutValue("Usage");
                foreach (var elem in groupedBooks)
                {
                    worksheet.Cells[i, 1].PutValue(elem.Book.Title);
                    worksheet.Cells[i, 2].PutValue(elem.Book.Author);
                    worksheet.Cells[i, 3].PutValue(elem.Book.Genre);
                    worksheet.Cells[i, 4].PutValue(elem.Count);

                }
                wb.Save(fstream, SaveFormat.Xlsx);
            }
        }
    }
}
