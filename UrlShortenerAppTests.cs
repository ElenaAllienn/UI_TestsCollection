using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace UI_TestsCollection
{
    public class UrlShortenerAppTests
    {
        private WebDriver driver;
        private const string BaseUrl = "https://shorturl.softuniqa.repl.co/";

        [SetUp]
        public void BrowserOpen()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = BaseUrl;

        }

        [TearDown]
        public void BrowserClose()
        {
            driver.Quit();
        }

        //[Test]
        //public void Test_Redirect_ToShortUrls()
        //{
        //    var urls = driver.FindElement(By.LinkText("Short URLs"));
        //    urls.Click();

        //    Assert.That("urls", Is.EqualTo("urls"));
        //}
        [Test]
        public void Test_UrlShortPageTitle()
        {
            var title = driver.FindElement(By.CssSelector("body > main > h1")).Text;
            Assert.That(title, Is.EqualTo("URL Shortener"));
        }

        [TestCase("https://nakov.com", 0)]
        [TestCase("http://shorturl.softuniqa.repl.co/go/nak", 1)]
        public void Test_Links(string url, int index)
        {
            driver.FindElement(By.CssSelector("body > header > a:nth-child(3)")).Click();
            var tableCells = driver.FindElements(By.CssSelector("body > main > table > tbody > tr > td"));
            Assert.That(tableCells[index].Text, Is.EqualTo(url));
        }

    }
}
