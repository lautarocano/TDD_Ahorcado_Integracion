using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;
using System.Threading;

namespace Ahorcado.UIAutomation
{
    [Binding]
    public class HangmanSpecflow
    {
        IWebDriver driver;
        String baseURL;
        int chancesLeftAnt;

        [BeforeScenario]
        public void TestInitialize()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + @"\Drivers";
            driver = new InternetExplorerDriver(path);
            /*
            ChromeOptions options = new ChromeOptions();
            options.BinaryLocation = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe";
            driver  = new ChromeDriver(path, options);           
            driver = new ChromeDriver(path);
            */
            baseURL = "http://localhost:39278/";
        }

        //Primer test - perder el juego
        [Given(@"I have entered Ahorcado as the wordToGuess")]
        public void GivenIHaveEnteredAhorcadoAsTheWordToGuess()
        {
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
            char letterRisked = 's';
            for (int i = 0; i < 6; i++)
            {
                letterTyped.SendKeys(letterRisked.ToString());
                Thread.Sleep(1000);
                btnInsertLetter.SendKeys(Keys.Enter);
                letterRisked++;
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


        //Segundo test - acertar una letra
        [Given(@"I have entered Hola as the wordToGuess")]
        public void GivenIHaveEnteredHolaAsTheWordToGuess()
        {
            driver.Navigate().GoToUrl(baseURL);

            Thread.Sleep(5000);

            var txtPalabra = driver.FindElement(By.Id("WordToGuess"));
            txtPalabra.SendKeys("Hola");

            Thread.Sleep(1000);

            var btnInsertWord = driver.FindElement(By.Id("btnInsertWord"));
            btnInsertWord.SendKeys(Keys.Enter);

            Thread.Sleep(1000);
        }

        [When(@"I enter A as the typedLetter one time")]
        public void WhenIEnterAAsTheTypedLetterOneTime()
        {
            var chancesLeft = driver.FindElement(By.Id("ChancesLeft"));
            chancesLeftAnt = Convert.ToInt32(chancesLeft.GetAttribute("value"));
            var letterTyped = driver.FindElement(By.Id("LetterTyped"));
            var btnInsertLetter = driver.FindElement(By.Id("btnInsertLetter"));
            letterTyped.SendKeys("A");
            Thread.Sleep(1000);
            btnInsertLetter.SendKeys(Keys.Enter);
        }

        [Then(@"I should be told that I hit the letter")]
        public void ThenIShouldBeToldThatIHitTheLetter()
        {
            var chancesLeft = driver.FindElement(By.Id("ChancesLeft"));
            var hit = Convert.ToInt32(chancesLeft.GetAttribute("value")) == chancesLeftAnt;
            Thread.Sleep(1000);
            Assert.IsTrue(hit);
            Thread.Sleep(1000);
        }


        //Tercer test - Insertar un numero
        [Given(@"I have entered Computadora as the wordToGuess")]
        public void GivenIHaveEnteredComputadoraAsTheWordToGuess()
        {
            driver.Navigate().GoToUrl(baseURL);

            Thread.Sleep(5000);

            var txtPalabra = driver.FindElement(By.Id("WordToGuess"));
            txtPalabra.SendKeys("Computadora");

            Thread.Sleep(1000);

            var btnInsertWord = driver.FindElement(By.Id("btnInsertWord"));
            btnInsertWord.SendKeys(Keys.Enter);

            Thread.Sleep(1000);
        }

        [When(@"I enter 4 as the typedLetter one time")]
        public void WhenIEnter4AsTheTypedLetterOneTime()
        {
            var letterTyped = driver.FindElement(By.Id("LetterTyped"));
            var btnInsertLetter = driver.FindElement(By.Id("btnInsertLetter"));
            letterTyped.SendKeys("4");
            Thread.Sleep(1000);
            btnInsertLetter.SendKeys(Keys.Enter);
        }

        [Then(@"It should tell me that the letter is invalid")]
        public void ThenIShouldBeToldThatTheLetterIsInvalid()
        {
            var mensaje = driver.FindElement(By.ClassName("ui-pnotify-text"));
            var invalid = "Letra invalida" == mensaje.Text;
            Thread.Sleep(1000);
            Assert.IsTrue(invalid);
            Thread.Sleep(1000);
        }



        //Cuarto test - Arriesgar palabra correcta
        [Given(@"I have entered Teclado as the wordToGuess")]
        public void GivenIHaveEnteredTecladoAsTheWordToGuess()
        {
            driver.Navigate().GoToUrl(baseURL);

            Thread.Sleep(5000);

            var txtPalabra = driver.FindElement(By.Id("WordToGuess"));
            txtPalabra.SendKeys("teclado");

            Thread.Sleep(1000);

            var btnInsertWord = driver.FindElement(By.Id("btnInsertWord"));
            btnInsertWord.SendKeys(Keys.Enter);

            Thread.Sleep(1000);
        }

        [When(@"I enter Teclado as the typedLetter")]
        public void WhenIEnterTecladoAsTheTypedLette()
        {
            var letterTyped = driver.FindElement(By.Id("LetterTyped"));
            var btnRiskWord = driver.FindElement(By.Id("btnRiskWord"));
            letterTyped.SendKeys("teclado");
            Thread.Sleep(1000);
            btnRiskWord.SendKeys(Keys.Enter);
        }

        [Then(@"I should be told that I win")]
        public void ThenIShouldBeToldThatIWin()
        {
            var txtPalabra = driver.FindElement(By.Id("WordToGuess"));
            var guessingWord = driver.FindElement(By.Id("GuessingWord"));
            var mensaje = driver.FindElement(By.ClassName("ui-pnotify-text"));
            var win = guessingWord.GetAttribute("value").Replace(" ", String.Empty) == txtPalabra.GetAttribute("value");
            var correctMesagge = "Has ganado!! Enséñales a los perdedores para que aprendan!" == mensaje.Text;
            Thread.Sleep(1000);
            Assert.IsTrue(win && correctMesagge);
            Thread.Sleep(1000);
        }


        //Quinto test - Insertar palabra secreta no alfabetica
        [Given(@"I have entered 123 as the wordToGuess")]
        public void GivenIhaveentered123asthewordToGuess()
        {
            driver.Navigate().GoToUrl(baseURL);

            Thread.Sleep(5000);

            var txtPalabra = driver.FindElement(By.Id("WordToGuess"));
            txtPalabra.SendKeys("123");

            Thread.Sleep(1000);

            var btnInsertWord = driver.FindElement(By.Id("btnInsertWord"));
            btnInsertWord.SendKeys(Keys.Enter);

            Thread.Sleep(1000);
        }

        [Then(@"It should tell me that the word is invalid")]
        public void ThenIShouldBeToldThatTheWordIsInvalid()
        {
            var mensaje = driver.FindElement(By.ClassName("ui-pnotify-text"));
            var invalid = "Palabra secreta invalida" == mensaje.Text;
            Thread.Sleep(1000);
            Assert.IsTrue(invalid);
            Thread.Sleep(1000);
        }


    }
}
