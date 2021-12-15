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
    public class DAOFactoryTests
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FifthTaskDbb;Integrated Security=True";
        /// <summary>
        /// Tests Create Factory method
        /// </summary>
        [TestMethod]
        public void Create_ShouldCreateSingleInstanceOfTheDaoObject()
        {
            // Arrange
            DAOFactory factory = new DAOFactory();
            IDao<Book> expected = factory.Create<Book>(connectionString);

            // Act
            IDao<Book> actual = factory.Create<Book>(connectionString);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
