using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;
using System.Threading;
using BibliotecaClases;

namespace Ahorcado.UIAutomation
{
    [Binding]
    public class HangmanSpecflow
    {
        IWebDriver driver;
        String baseURL;
        Juego j;

        [BeforeScenario]
        public void TestInitialize()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + @"\Drivers";
            //driver = new InternetExplorerDriver(path);
            driver  = new ChromeDriver(path);
            baseURL = "http://localhost:39278/";
        }

        [Given(@"I have entered Ahorcado as the wordToGuess")]
        public void GivenIHaveEnteredAhorcadoAsTheWordToGuess()
        {
            j = new Juego("Ahorcado");
            driver.Navigate().GoToUrl(baseURL);

            Thread.Sleep(5000);
            
            var txtPalabra = driver.FindElement(By.Id("WordToGuess"));
            txtPalabra.SendKeys("Ahorcado");

            Thread.Sleep(1000);

            var btnInsertWord = driver.FindElement(By.Id("btnInsertWord"));
            btnInsertWord.SendKeys(Keys.Enter);

            Thread.Sleep(1000);
        }
               
        [When(@"I enter X as the typedLetter five times")]
        public void WhenIEnterXAsTheTypedLetterFiveTimes()
        {
            var letterTyped = driver.FindElement(By.Id("LetterTyped"));
            var btnInsertLetter = driver.FindElement(By.Id("btnInsertLetter"));
            for (int i = 0; i < 7; i++)
            {
                letterTyped.SendKeys("X");
                Thread.Sleep(1000);
                btnInsertLetter.SendKeys(Keys.Enter);
            }
        }

        [Then(@"I should be told that I lost")]
        public void ThenIShouldBeToldThatILost()
        {
            var chancesLeft = driver.FindElement(By.Id("ChancesLeft"));
            var loss = Convert.ToInt32(chancesLeft.GetAttribute("value")) == 0;
            Thread.Sleep(1000);
            Assert.IsTrue(loss);
            Thread.Sleep(1000);
        }

        [AfterScenario]
        public void TestCleanUp()
        {
            driver.Quit();
        }
    }
}
