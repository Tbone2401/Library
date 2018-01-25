using System;
using System.Collections.Generic;
using Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Library.Tests
{
    [TestClass]
    public class BookTests
    {
        [TestMethod]
        public void CreateBook()
        {
            //Arrange
            Book book = new Book
            {
                Title = "MyTestBook",
                Pages = 120,
                Genres = new List<Genre>()
                {
                    Genre.Action,
                    Genre.Adventure
                }
            };
            //Act
            book.Pages = -100;
            //Assert
            Assert.IsFalse(book.Pages == -100);
        }
    }
}
