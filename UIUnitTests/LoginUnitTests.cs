using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Firefox;
using UIUnitTests.PageObjects;
using System;

namespace UIUnitTests
{
    public class LoginUnitTests
    { 
        private IWebDriver _driver;
        private string _baseURL = "http://localhost:56938/";
        private string pathToDebug = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
                                                      AppDomain.CurrentDomain.RelativeSearchPath ?? "");
        private LogInPage loginPage;

        [SetUp]
        public void SetUpBrowser()
        {
            _driver = new FirefoxDriver(pathToDebug)
            {
                Url = _baseURL + "Account/Login"
            };
            loginPage = new LogInPage(_driver);
            _driver.Manage().Window.Maximize();
        }

        [Test]
        [Category("UI Redirect to Index")]
        public void AfterPassingCorrectData()
        {
            loginPage.Email.SendKeys("admin@admin.com");
            loginPage.Password.SendKeys("Test!23");
            loginPage.Submit.Click();

            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            string newURL = _driver.Url;

            StringAssert.AreEqualIgnoringCase(_baseURL, newURL);
        }

        [Test]
        [Category("UI Field Validation")]
        public void GivesErrorAfterPassingInCorrectData()
        {
            loginPage.Email.SendKeys("admin@");
            loginPage.Password.SendKeys("Test!23");
            loginPage.Submit.Click();

            StringAssert.Contains("not a valid e-mail address.", loginPage.EmailErrorMessage.Text);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Close();
            _driver.Dispose();
        }
    }
}
