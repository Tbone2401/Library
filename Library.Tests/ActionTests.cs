using System;
using System.Web.Mvc;
using Library.Controllers;
using Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Library.Tests
{
    [TestClass]
    public class ActionTests
    {
        [TestMethod]
        public void Controllertest()
        {
            //Arrange
            LibraryController target = new LibraryController();
            //Act
            ViewResult result = target.Index();
            //Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void ViewSelectionTest()
        {
            //Arrange
            LibraryController target = new LibraryController();
            //Act
            ViewResult result = target.Index();
            //Assert
            Assert.AreEqual("", result.ViewName);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(Book));
        }
    }
}
