using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System.IO;
using System.Linq.Expressions;
using System.Threading;

namespace NaukriJobUpdate
{
    public class VishalaIndeed
    {
        IWebDriver driver = new EdgeDriver();

        [SetUp]
        public void Setup()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://in.indeed.com/");

            Thread.Sleep(5000);
            try
            {
                Thread.Sleep(5000);
                if (driver.FindElement(By.XPath("//input[@type='checkbox']")).Displayed)
                {
                    driver.FindElement(By.XPath("//input[@type='checkbox']")).Click();
                }
            }
            catch { }

            //Sign in
            driver.FindElement(By.LinkText("Sign in")).Click();
            Thread.Sleep(2000);
            try
            {
                Thread.Sleep(5000);
                if (driver.FindElement(By.XPath("//input[@type='checkbox']")).Displayed)
                {
                    driver.FindElement(By.XPath("//input[@type='checkbox']")).Click();
                }
            }
            catch { }

            driver.FindElement(By.Name("__email")).SendKeys("bvishala233@gmail.com");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.LinkText("Log in with a password instead")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.Name("__password")).SendKeys("Cutevishala@23");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            Thread.Sleep(5000);  
        }

        [Test]
        public void VishalaIndeedTest()
        {
            Thread.Sleep(5000);

            driver.FindElement(By.XPath("//a[@href='/mnjuser/profile']")).Click();
            Thread.Sleep(5000);

            try
            {
                var chatBotflag = driver.FindElement(By.ClassName("chatbot_Nav")).Displayed;
                if (chatBotflag)
                    driver.FindElement(By.XPath("//div[@class='chatbot_Nav']/div")).Click();
            }
            catch
            {

            }

            for (int i = 0; i < 5; i++)
            {
                driver.FindElement(By.XPath("//div[@id='lazyResumeHead']//span[@class='edit icon']")).Click();
                Thread.Sleep(3000);
                driver.FindElement(By.XPath("//form[@name='resumeHeadlineForm']//button[@type='submit']")).Click();

                Thread.Sleep(2000);

                string currentDir = Directory.GetCurrentDirectory();
                string resumePath = Path.Combine(currentDir, "TestData", "Resumes", "Srishti_Resume.pdf");

                if (!File.Exists(resumePath))
                {
                    string projectDir = Directory.GetParent(currentDir).Parent.Parent.FullName;
                    resumePath = Path.Combine(projectDir, "TestData", "Resumes", "Dr.Vishala_Profile.pdf");
                }

                if (!File.Exists(resumePath))
                {
                    throw new FileNotFoundException($"Resume not found at: {resumePath}");
                }

                IWebElement fileUpload = driver.FindElement(By.Id("attachCV"));
                fileUpload.SendKeys(resumePath);
                Thread.Sleep(5000);
            }
    
            driver.Quit();
        }
    }
}