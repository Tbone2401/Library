using System;
using Library.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Library.Tests
{
    [TestClass]
    public class ISBNValidatorTests
    {
        [TestMethod]
        public void CheckValidation()
        {
            //Arrange
            IsbnValidator validator = new IsbnValidator();

            //Act
            //Assert
            Assert.IsTrue(validator.IsValid("0-306-40615-2"));
            Assert.IsFalse(validator.IsValid("0-306-40015-2"));
            Assert.IsTrue(validator.IsValid("978-0-306-40615-7"));
            Assert.IsFalse(validator.IsValid("978-0-306-40615-4"));
            Assert.IsFalse(validator.IsValid("978-0-306-4061588-4"));
        }
    }
}
