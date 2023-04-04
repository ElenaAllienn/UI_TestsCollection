using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace UI_TestsCollection
{
    public class LoginTests
    {
        private WebDriver driver;
        private const string BaseUrl = "http://softuni.bg";


        [SetUp]
        public void BrowserOpen()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = BaseUrl;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

        }

        [TearDown]
        public void BrowserClose()
        {
            driver.Quit();
        }

        [Test]
        public void Test_Login_InvalidPassword()
        {
            driver.FindElement(By.XPath("//a[@href='/login']")).Click();
            var pageSource = driver.PageSource;

            Assert.False(pageSource.Contains("Невалидно потребителско име или парола"));
        }

        [Test]
        public void Test_AccountLogin_ValidCredentials()
        {
            driver.FindElement(By.XPath("//a[contains(.,'Вход')]")).Click();
            driver.FindElement(By.CssSelector(".softuni-btn-primary:nth-child(1)")).Click();
            driver.FindElement(By.Id("username")).SendKeys("..........");//validCred
            driver.FindElement(By.Id("password-input")).SendKeys("............");//validCred
            driver.FindElement(By.CssSelector(".softuni-btn")).Click();
            driver.FindElement(By.CssSelector(".page-header-items-list-element:nth-child(2) > .page-header-items-list-element-link > .main-title")).Click();

            Assert.That("username", Is.EqualTo("username"));
            Assert.That("password", Is.EqualTo("password"));
            Assert.That(driver.FindElement(By.XPath("//h3[@class='box-title'][contains(.,'МОИТЕ КУРСОВЕ')]")).Text, Is.EqualTo("МОИТЕ КУРСОВЕ"));
            Assert.That(driver.FindElement(By.XPath("//h3[@id='upcoming-exams-box-title']")).Text, Is.EqualTo("МОЯТ ГРАФИК"));
        }
        [Test]
        public void Test_AccountLogin_InvalidUsername()
        {
            driver.FindElement(By.XPath("//a[contains(.,'Вход')]")).Click();
            driver.FindElement(By.CssSelector(".softuni-btn-primary:nth-child(1)")).Click();
            driver.FindElement(By.Id("username")).SendKeys("Lllll");
            driver.FindElement(By.Id("password-input")).SendKeys("...........");//validCred
            driver.FindElement(By.CssSelector(".softuni-btn")).Click();


            //Assert.That("username", Is.EqualTo("Невалидно потребителско име или парола"));
            //Assert.That("password", Is.EqualTo("password"));
            Assert.That(driver.FindElement(By.CssSelector("li")).Text, Is.EqualTo("Невалидно потребителско име или парола"));
        }
        [Test]
        public void Test_AccountLogin_InvalidCredentials()
        {
            driver.FindElement(By.XPath("//a[contains(.,'Вход')]")).Click();
            driver.FindElement(By.CssSelector(".softuni-btn-primary:nth-child(1)")).Click();
            driver.FindElement(By.Id("username")).SendKeys("Mmmm");
            driver.FindElement(By.Id("password-input")).SendKeys("Mmmmmm");
            driver.FindElement(By.CssSelector(".softuni-btn")).Click();


            //Assert.That("username", Is.EqualTo("Невалидно потребителско име или парола"));
            //Assert.That("password", Is.EqualTo("password"));
            Assert.That(driver.FindElement(By.CssSelector("li")).Text, Is.EqualTo("Невалидно потребителско име или парола"));
        }

        [Test]
        public void Test_AccountLogin_For_SpecialSymbols()
        {
            driver.FindElement(By.XPath("//a[contains(.,'Вход')]")).Click();
            driver.FindElement(By.CssSelector(".softuni-btn-primary:nth-child(1)")).Click();
            driver.FindElement(By.Id("username")).SendKeys("..........");//validCred
            driver.FindElement(By.Id("password-input")).SendKeys("#23&*$!@^+-");
            driver.FindElement(By.CssSelector(".softuni-btn")).Click();


            //Assert.That("username", Is.EqualTo("Невалидно потребителско име или парола"));
            //Assert.That("password", Is.EqualTo("password"));
            Assert.That(driver.FindElement(By.CssSelector("li")).Text, Is.EqualTo("Невалидно потребителско име или парола"));
        }

        [Test]
        public void Test_AccountLogin_EmptyFields()
        {
            driver.FindElement(By.XPath("//a[contains(.,'Вход')]")).Click();
            driver.FindElement(By.CssSelector(".softuni-btn-primary:nth-child(1)")).Click();
            driver.FindElement(By.Id("username")).SendKeys("");
            driver.FindElement(By.Id("password-input")).SendKeys("");
            driver.FindElement(By.CssSelector(".softuni-btn")).Click();


            //Assert.That("username", Is.EqualTo("Невалидно потребителско име или парола"));
            //Assert.That("password", Is.EqualTo("password"));
            Assert.That(driver.FindElement(By.Id("username-error")).Text, Is.EqualTo("Моля, въведете потребителско име"));
            Assert.That(driver.FindElement(By.Id("password-input-error")).Text, Is.EqualTo("Моля, въведете паролата си"));
        }


        [Test]
        public void Test_Account_Logout()
        {
            driver.FindElement(By.XPath("//a[contains(.,'Вход')]")).Click();
            driver.FindElement(By.CssSelector(".softuni-btn-primary:nth-child(1)")).Click();
            driver.FindElement(By.Id("username")).SendKeys(".......");//validCred
            driver.FindElement(By.Id("password-input")).SendKeys("...........");//validCred
            driver.FindElement(By.CssSelector(".softuni-btn")).Click();
            driver.FindElement(By.XPath("//*[@id=\"show-profile-menu\"]/span/span[2]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"logoutForm\"]/input[2]"));

            Assert.That(BaseUrl, Is.EqualTo("http://softuni.bg"));
        }
    }
}

