using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Threading;

namespace NaukriJobUpdate
{
    public class VishalaNaukri
    {
        IWebDriver driver = new ChromeDriver();

        [SetUp]
        public void Setup()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.naukri.com/");

            Thread.Sleep(5000);

            //Login
            driver.FindElement(By.Id("login_Layer")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//input[@placeholder='Enter your active Email ID / Username']")).SendKeys("bvishala233@gmail.com");
            driver.FindElement(By.XPath("//input[@placeholder='Enter your password']")).SendKeys("Cutepasha@23");
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

            }
        }

        [Test]
        public void VishalaNaukriTest()
        {
            Thread.Sleep(5000);

            driver.FindElement(By.XPath("//a[@href='/mnjuser/profile']")).Click();
            Thread.Sleep(5000);

            for (int i = 0; i < 50; i++)
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