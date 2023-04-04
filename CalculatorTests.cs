using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace UI_TestsCollection
{
    public class CalculatorTests
    {
        IWebDriver driver;

        [SetUp]
        public void OpenBrowser()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com/number-calculator/";

        }
        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }

        [Test]
        public void Test_AddTwoNumbers_Valid()
        {

            driver.FindElement(By.CssSelector("input#number1")).SendKeys("15");
            driver.FindElement(By.CssSelector("#operation")).SendKeys("+");
            driver.FindElement(By.CssSelector("input#number2")).SendKeys("7");
            driver.FindElement(By.CssSelector("#calcButton")).Click();

            var resultText = driver.FindElement(By.CssSelector("#result")).Text;
            Assert.That(resultText, Is.EqualTo("Result: 22"));
        }

        [Test]
        public void Test_AddTwoNumbers_Invalid()
        {
            driver.Url = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com/number-calculator/ ";
            driver.FindElement(By.CssSelector("input#number1")).SendKeys("hello");
            driver.FindElement(By.CssSelector("#operation")).SendKeys("+");
            driver.FindElement(By.CssSelector("input#number2")).SendKeys("");

            driver.FindElement(By.CssSelector("#calcButton")).Click();

            var resultText = driver.FindElement(By.CssSelector("#result")).Text;
            Assert.AreEqual("Result: invalid input", resultText);
        }

        [Test]
        public void Test_AddTwoNumbers_Reset()
        {
            driver.FindElement(By.CssSelector("input#number1")).SendKeys("15");
            driver.FindElement(By.CssSelector("#operation")).SendKeys("+");
            driver.FindElement(By.CssSelector("input#number2")).SendKeys("7");
            driver.FindElement(By.CssSelector("#calcButton")).Click();

            var resultText = driver.FindElement(By.CssSelector("#result")).Text;
            Assert.That(resultText, Is.EqualTo("Result: 22"));
            Assert.IsNotEmpty(resultText);

            var firstNumber = driver.FindElement(By.CssSelector("#number1"));
            var operation = driver.FindElement(By.CssSelector("#operation"));
            var secondNumber = driver.FindElement(By.CssSelector("#number2"));
            var reset = driver.FindElement(By.CssSelector("#resetButton"));
            reset.Click();
            Assert.That(firstNumber.GetAttribute("value"), Is.EqualTo(""));
            Assert.That(secondNumber.GetAttribute("value"), Is.EqualTo(""));

        }
    }
}