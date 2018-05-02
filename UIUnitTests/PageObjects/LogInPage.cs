using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace UIUnitTests.PageObjects
{
    public class LogInPage
    {
        private IWebDriver _driver;
        public LogInPage(IWebDriver driver) { _driver = driver; }

        [CacheLookup]
        public IWebElement Email => _driver.FindElement(By.XPath("//*[@id='Email']"));

        [CacheLookup]
        public IWebElement Password => _driver.FindElement(By.XPath("//*[@id='Password']"));

        [CacheLookup]
        public IWebElement Submit => _driver.FindElement(By.XPath("/html/body/div[1]/div/div/section/form/div[5]/button"));

        [CacheLookup]
        public IWebElement EmailErrorMessage => _driver.FindElement(By.XPath("//*[@id='Email-error']"));

        [CacheLookup]
        public IWebElement InvalidLoginMessage => _driver.FindElement(By.XPath("/html/body/div[1]/div/div/section/form/div[1]/ul/li"));
    }
}
