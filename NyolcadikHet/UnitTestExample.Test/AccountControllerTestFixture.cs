using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NuGet;
using NUnit.Framework;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        [
            Test,
            TestCase("abcd1234", false), //jelszó
            TestCase("irf@uni-corvinus", false), //nincs domain
            TestCase("irf.uni-corvinus.hu", false), //nincs @
            TestCase("irf@uni-corvinus.hu", true) //megfelelő
        ]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            //Arrange
            var accountController = new AccountController();

            //Act
            var result = accountController.ValidateEmail(email);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [
            Test,
            TestCase("Aaaaaaaa", false), //nincs szám
            TestCase("AAAAAAA1", false), //nincs kisbetű
            TestCase("aaaaaaa1", false), //nincs nagybetű
            TestCase("abc1234", false), //túl rövid
            TestCase("Abc45678", true) //megfelelő
        ]
        public void TestValidatePassword(string password, bool expectedResult)
        {
            //Arrange
            var accountController = new AccountController();

            //Act
            var result = accountController.ValidatePassword(password);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [
            Test,
            TestCase("irf@uni-corvinus.hu", "Abcd1234"),
            TestCase("irf@uni-corvinus.hu", "Abcd1234567")
        ]
        public void TestRegisterHappyPath(string email, string password)
        {
            //Arrange
            var accountController = new AccountController();

            //Act
            var result = accountController.Register(email, password);

            //Assert
            Assert.AreEqual(email, result.Email);
            Assert.AreEqual(password, result.Password);
            Assert.AreNotEqual(Guid.Empty, result.ID);
        }

        [
            Test,
            TestCase("irf@uni-corvinus", "Abcd1234"),
            TestCase("irf.uni-corvinus.hu", "Abcd1234"),
            TestCase("irf@uni-corvinus.hu", "abcd1234"),
            TestCase("irf@uni-corvinus.hu", "ABCD1234"),
            TestCase("irf@uni-corvinus.hu", "abcdABCD"),
            TestCase("irf@uni-corvinus.hu", "Ab1234"),
        ]
        public void TestRegisterValidateException(string email, string password)
        {
            //Arrange
            var accountController = new AccountController();

            //Act
            try
            {
                var result = accountController.Register(email, password);
                Assert.Fail();
            }
            catch (Exception ex)
            {

                Assert.IsInstanceOf<ValidationException>(ex);
            }

            //Assert
            /*Assert.AreEqual(email, result.Email);
            Assert.AreEqual(password, result.Password);
            Assert.AreEqual(Guid.Empty, result.ID);*/
        }
    }
}
