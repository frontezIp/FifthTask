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
    public class ReflectionHelperTests
    {
        

        /// <summary>
        /// Tests GetNamesOfThePropertiesWithOutId method
        /// </summary>
        [TestMethod]
        public void GetNamesOfPropertiesWithoutId_ShouldReturnAllPropertiesOfTheTypeWithoutId()
        {
            // Arrange
            ReflectionHelper<Book> helper = new ReflectionHelper<Book>();
            string expected = "Author, Title, Genre";

            // Act
            string actual = helper.GetNamesOfPropertiesWithoutId(typeof(Book));

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests GetNamesOfThePropertiesWithId method
        /// </summary>
        [TestMethod]
        public void GetNamesOfPropertiesWithId_ShouldReturnAllPropertiesOfTheTypeWithId()
        {
            // Arrange
            string expected = "Id, Author, Title, Genre";
            ReflectionHelper<Book> helper = new ReflectionHelper<Book>();

            // Act
            Type type = typeof(Book);
            string actual = helper.GetNamesOfPropertiesWithId(typeof(Book));

            // Assert
            Assert.AreEqual(expected, actual);


        }

        /// <summary>
        /// Tests FindIdOfTheEntity methdo
        /// </summary>
        [TestMethod]
        public void FindIdOfTheEntity_ShouldFindRightId()
        {
            // Arrange
            Book book = new Book();
            book.Id = 4;
            int expected = 4;
            ReflectionHelper<Book> helper = new ReflectionHelper<Book>();

            // Act
            int actual = helper.FindIdOfTheEntity(book);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        /// Tests TableName method
        /// </summary>
        [TestMethod]
        public void TableName_ShouldReturnNameOfTable()
        {
            // Arrange
            ReflectionHelper <Book> helper = new ReflectionHelper<Book>();
            string expected = "Book";

            //  Act
            string actual = helper.NameOfTheTable();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests NamesOfPropertiesInSqlFormat method
        /// </summary>
        [TestMethod]
        public void NamesOfPropertiesInSqlFormat_ShouldReturnStringInSqlFormat()
        {
            // Arrange
            ReflectionHelper<Book> helper = new ReflectionHelper<Book>();
            string expected = "@age, @name";

            // Act
            string actual = helper.NamesOfPropertiesInSqlFormat("Age, Name");

            // Assert
            Assert.AreEqual(expected, actual);
 
        }
        /// <summary>
        /// Tests GenerateSqlParameters method
        /// </summary>
        [TestMethod]
        public void GenerateSqlParameters_ShouldReturnArrayOfSqlParameters()
        {
            // Arrange
            bool actual = false;
            bool expected = true;
            Book book = new Book();
            book.Id = 5;
            book.Title = "Punishment";
            book.Genre = "Triller";
            book.Author = "Dostoevsky";
            ReflectionHelper<Book> helper = new ReflectionHelper<Book>();
            SqlParameter[] parameters;
            
            // Act
            parameters = helper.GenerateSqlParameters(book);
            if (parameters != null)
                actual = true;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests Generate UpdateSetString method
        /// </summary>
        [TestMethod]
        public void GenerateUpdateSetString_ShouldReturnSetString()
        {
            // Arrange
            ReflectionHelper<Book> helper = new ReflectionHelper<Book>();
            string expected = "Author=@author, Title=@title, Genre=@genre";

            // Act
            string actual = helper.GenerateUpdateSetString();
            // Assert
            Assert.AreEqual(expected, actual);
        }

    }
}
