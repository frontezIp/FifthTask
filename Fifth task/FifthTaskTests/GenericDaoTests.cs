using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fifth_task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace FifthTaskTests
{
    [TestClass]
    public class GenericDaoTests
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FifthTaskDbb;Integrated Security=True";
        DataBaseLauncher launcher;
        Book bookOne;
        Book bookTwo;
        Book bookThree;
        Subscriber subscriberOne;
        Subscriber subscriberTwo;
        Subscriber subscriberThree;
        BookReport bookReportOne;
        BookReport bookReportTwo;
        BookReport bookReportThree;
        BookReport bookReportFour;
        DAOFactory _daoCreate;

        [TestInitialize]
        public void TestInitialize()
        {
            launcher = new DataBaseLauncher(connectionString);
            _daoCreate = new DAOFactory();
            List<BookReportDto> bookReportDtos = BookReportDtoService.GetAllReports();
            foreach (var report in bookReportDtos)
            {
                _daoCreate.Create<BookReport>(connectionString).Delete(report.Id);
            }
            List<BookDto> bookDtos = BookDtoService.GetAllBooks();
            foreach (var book in bookDtos)
            {
                _daoCreate.Create<Book>(connectionString).Delete(book.Id);
            }
            List<SubscriberDto> subscriberDtos = SubscriberDtoService.GetAllSubcribers();
            foreach (var subscriber in subscriberDtos)
            {
                _daoCreate.Create<Subscriber>(connectionString).Delete(subscriber.Id);
            }
            bookOne = new Book();
            bookOne.Author = "Dostoevksky";
            bookOne.Genre = "Novel";
            bookOne.Title = "Brothers Karamazov";
            bookOne.Id = _daoCreate.Create<Book>(connectionString).Create(bookOne);
            bookTwo = new Book();
            bookTwo.Author = "Dostoevsky";
            bookTwo.Genre = "Novel";
            bookTwo.Title = "Crime and Punishment";
            bookTwo.Id = _daoCreate.Create<Book>(connectionString).Create(bookTwo);
            bookThree = new Book();
            bookThree.Author = "Platon";
            bookThree.Genre = "Dialogue";
            bookThree.Title = "The Republic";
            bookThree.Id = _daoCreate.Create<Book>(connectionString).Create(bookThree);
            subscriberOne = new Subscriber();
            subscriberOne.FirstName = "Michael";
            subscriberOne.LastName = "Jordan";
            subscriberOne.MiddleName = "Eliot";
            subscriberOne.Sex = true;
            subscriberOne.DateOfBirth = new DateTime(2002, 5, 16);
            subscriberOne.Id = _daoCreate.Create<Subscriber>(connectionString).Create(subscriberOne);
            subscriberTwo = new Subscriber();
            subscriberTwo.FirstName = "Alex";
            subscriberTwo.LastName = "Celiot";
            subscriberTwo.MiddleName = "Cile";
            subscriberTwo.Sex = true;
            subscriberTwo.DateOfBirth = new DateTime(2000, 4, 13);
            subscriberTwo.Id = _daoCreate.Create<Subscriber>(connectionString).Create(subscriberTwo);
            subscriberThree = new Subscriber();
            subscriberThree.FirstName = "Jersey";
            subscriberThree.LastName = "Diana";
            subscriberThree.MiddleName = "Cristina";
            subscriberThree.Sex = false;
            subscriberThree.DateOfBirth = new DateTime(1998, 4, 13);
            subscriberThree.Id = _daoCreate.Create<Subscriber>(connectionString).Create(subscriberThree);
            bookReportOne = new BookReport();
            bookReportOne.DateOfGiving = new DateTime(2017, 5, 14);
            bookReportOne.ReturnStatus = true;
            bookReportOne.StateOfBook = "Good";
            bookReportOne.SubscriberId = subscriberOne.Id;
            bookReportOne.BookId = bookOne.Id;
            bookReportOne.Id = _daoCreate.Create<BookReport>(connectionString).Create(bookReportOne);
            bookReportTwo = new BookReport();
            bookReportTwo.ReturnStatus = true;
            bookReportTwo.DateOfGiving = new DateTime(2016, 5, 11);
            bookReportTwo.StateOfBook = "Bad";
            bookReportTwo.SubscriberId = subscriberOne.Id;
            bookReportTwo.BookId = bookTwo.Id;
            bookReportTwo.Id = _daoCreate.Create<BookReport>(connectionString).Create(bookReportTwo);
            bookReportThree = new BookReport();
            bookReportThree.ReturnStatus = true;
            bookReportThree.DateOfGiving = new DateTime(2019, 6, 14);
            bookReportThree.StateOfBook = "Medium";
            bookReportThree.SubscriberId = subscriberTwo.Id;
            bookReportThree.BookId = bookTwo.Id;
            bookReportThree.Id = _daoCreate.Create<BookReport>(connectionString).Create(bookReportThree);
            bookReportFour = new BookReport();
            bookReportFour.ReturnStatus = true;
            bookReportFour.DateOfGiving = new DateTime(2020, 6, 14);
            bookReportFour.StateOfBook = "Good";
            bookReportFour.SubscriberId = subscriberThree.Id;
            bookReportFour.BookId = bookThree.Id;
            bookReportFour.Id = _daoCreate.Create<BookReport>(connectionString).Create(bookReportFour);
        }

        /// <summary>
        /// Tests Create method in scenario creating new row of the book table in DB
        /// </summary>
        [TestMethod]
        public void Create_ShouldAddNewBookInTheDb()
        {
            // Arrange
            bool actual = false;
            bool expected = true;
            Book book = new Book();
            book.Author = "Chitov";
            book.Genre = "Criminal";
            book.Title = "Fury";


            // Act
            book.Id = _daoCreate.Create<Book>(connectionString).Create(book);
            Book actualBook = _daoCreate.Create<Book>(connectionString).Get(book.Id);
            if (book.Author == actualBook.Author && book.Genre == actualBook.Genre && book.Title == actualBook.Title)
                actual = true;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests Create method in scenario creating new row of the subscriber Table in DB
        /// </summary>
        [TestMethod]
        public void Create_ShouldAddNewSubscriberInTheBd()
        {
            // Arrange
            bool actual = false;
            bool expected = true;
            Subscriber subscriber = new Subscriber();
            subscriber.FirstName = "Joly";
            subscriber.LastName = "Tony";
            subscriber.MiddleName = "Trump";
            subscriber.DateOfBirth = new DateTime(2001, 4, 5);
            subscriber.Sex = true;

            // Act
            subscriber.Id = _daoCreate.Create<Subscriber>(connectionString).Create(subscriber);
            Subscriber actualSubscriber = _daoCreate.Create<Subscriber>(connectionString).Get(subscriber.Id);
            if (subscriber.FirstName == actualSubscriber.FirstName && subscriber.LastName == actualSubscriber.LastName && subscriber.MiddleName == actualSubscriber.MiddleName && subscriber.Sex && subscriber.Sex)
                actual = true;

            // Assert
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        /// Tests Create method in scenario creating new row of the book Report Table in DB
        /// </summary>
        [TestMethod]
        public void Create_ShouldAddNewReportInTheDb()
        {
            // Arrange
            bool actual = false;
            bool expected = true;
            BookReport bookReport = new BookReport() {BookId = bookOne.Id, DateOfGiving = new DateTime(2019, 6, 14), ReturnStatus = true, StateOfBook ="Medium", SubscriberId = subscriberOne.Id  };
            

            // Act
            bookReport.Id = _daoCreate.Create<BookReport>(connectionString).Create(bookReport);
            BookReport actualReport = _daoCreate.Create<BookReport>(connectionString).Get(bookReport.Id);
            if (bookReport.ReturnStatus == actualReport.ReturnStatus && bookReport.StateOfBook == actualReport.StateOfBook && bookReport.SubscriberId == actualReport.SubscriberId && bookReport.DateOfGiving == actualReport.DateOfGiving)
                actual = true;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests Get method 
        /// </summary>
        [TestMethod]
        public void Get_ShouldGetRowByGivenId()
        {
            // Arrange
            bool actual = false;
            bool expected = true;


            // Act
            BookReport bookReport = _daoCreate.Create<BookReport>(connectionString).Get(bookReportOne.Id);
            if (bookReport.ReturnStatus == bookReportOne.ReturnStatus && bookReport.StateOfBook == bookReportOne.StateOfBook && bookReport.SubscriberId == bookReportOne.SubscriberId && bookReport.DateOfGiving == bookReportOne.DateOfGiving)
                actual = true;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests Delete method
        /// </summary>
        [TestMethod]
        public void Delete_ShouldDeleteRowFromDbById()
        {
            // Arrange
            int expected = 3;
            int actual;

            // Act
            _daoCreate.Create<BookReport>(connectionString).Delete(bookReportOne.Id);
            actual = _daoCreate.Create<BookReport>(connectionString).GetAll().Count;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests Update method
        /// </summary>
        [TestMethod]
        public void Update_ShouldUpdateRowWithTheSameIdThatGivenItemHave()
        {
            // Arrange
            bool expected = true;
            bool actual = false;
            string Author = "Vasim";
            string Genre = "War";
            string Title = "Peace";
            int id = bookOne.Id;
            Book book = new Book() { Author = Author, Genre = Genre, Id = id, Title = Title };

            // Act
            _daoCreate.Create<Book>(connectionString).Update(book);
            Book actualBook = _daoCreate.Create<Book>(connectionString).Get(id);
            if (actualBook.Title == Title && actualBook.Genre == Genre && actualBook.Author == Author)
                actual = true;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests GetAll method
        /// </summary>
        [TestMethod]
        public void GetAll_ShouldReturnFourReportBooks()
        {
            // Arrange
            bool expected = true;
            bool actual = true;

            // Act
            List<BookReport> books = _daoCreate.Create<BookReport>(connectionString).GetAll();
            if (books == null || books.Count != 4)
                actual = false;
            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
