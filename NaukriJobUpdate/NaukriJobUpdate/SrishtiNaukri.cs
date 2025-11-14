using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Threading;

namespace NaukriJobUpdate
{
    public class SrishtiNaukri
    {
        IWebDriver driver;
        private string email;
        private string password;

        [SetUp]
        public void Setup()
        {
            email = Environment.GetEnvironmentVariable("SRISHTI_NAUKRI_EMAIL") ?? throw new Exception("SRISHTI_NAUKRI_EMAIL not set");
            password = Environment.GetEnvironmentVariable("SRISHTI_NAUKRI_PASSWORD") ?? throw new Exception("SRISHTI_NAUKRI_PASSWORD not set");

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--headless");
            chromeOptions.AddArguments("--no-sandbox");
            chromeOptions.AddArguments("--disable-dev-shm-usage");
            chromeOptions.AddArguments("--disable-gpu");
            chromeOptions.AddArguments("--window-size=1920,1080");

            driver = new ChromeDriver(chromeOptions);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.naukri.com/");

            Thread.Sleep(5000);

            //Login
            driver.FindElement(By.Id("login_Layer")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//input[@placeholder='Enter your active Email ID / Username']")).SendKeys(email);
            driver.FindElement(By.XPath("//input[@placeholder='Enter your password']")).SendKeys(password);
            driver.FindElement(By.XPath("//button[@class='btn-primary loginButton']")).Click();

            Thread.Sleep(5000);

            try
            {
                var chatBotflag = driver.FindElement(By.ClassName("chatbot_Nav")).Displayed;
                Thread.Sleep(6000);
                if (chatBotflag)
                    driver.FindElement(By.XPath("//div[@class='chatbot_Nav']/div")).Click();
            }
            catch
            {
                // Chatbot not found, continue
            }
        }

        [Test]
        public void SrishtiNaukriTest()
        {
            Thread.Sleep(5000);

            driver.FindElement(By.XPath("//a[@href='/mnjuser/profile']")).Click();
            Thread.Sleep(5000);

            // Get resume path relative to project - Updated to match folder structure
            string projectDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string resumePath = Path.Combine(projectDir, "TestData", "Resumes", "Srishti_Resume.pdf");

            if (!File.Exists(resumePath))
            {
                throw new FileNotFoundException($"Resume not found at: {resumePath}");
            }

            Console.WriteLine($"Using resume from: {resumePath}");

            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine($"Update iteration: {i + 1}/50");

                driver.FindElement(By.XPath("//div[@id='lazyResumeHead']//span[@class='edit icon']")).Click();
                Thread.Sleep(3000);
                driver.FindElement(By.XPath("//form[@name='resumeHeadlineForm']//button[@type='submit']")).Click();

                Thread.Sleep(2000);

                IWebElement fileUpload = driver.FindElement(By.Id("attachCV"));
                fileUpload.SendKeys(resumePath);
                Thread.Sleep(5000);
            }

            Console.WriteLine("Profile update completed successfully!");
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
        }
    }
}