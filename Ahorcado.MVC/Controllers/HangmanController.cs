using Ahorcado.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BibliotecaClases;

namespace Ahorcado.MVC.Controllers
{
    public class HangmanController : Controller
    {
        public static Juego Juego { get; set; }

        // GET: Hangman
        public ActionResult Index()
        {
            return View(new Hangman());
        }

        [HttpPost]
        public JsonResult InsertWordToGuess(Hangman model)
        {
            Juego = new Juego(model.WordToGuess);
            model.Message = Juego.validarSecretWord();
            if (model.Message == "Palabra secreta invalida")
            {
                Juego = null;
            }
            else
            {
                model.ChancesLeft = Juego.intentosRestantes;
                foreach (var rLetter in Juego.estadoAux)
                {
                    model.GuessingWord += rLetter + " ";
                }
                model.Message = null;
            }
            return Json(model);
        }

        [HttpPost]
        public JsonResult TryLetter(Hangman model)
        {
            char letra;
            try
            {
                letra = Convert.ToChar(model.LetterTyped);
                model.Message = Juego.validarLetra(letra);
                model.Win = Juego.checkearEstadoActual();
                model.ChancesLeft = Juego.intentosRestantes;
                model.WrongLetters = string.Empty;
                foreach (var wLetter in Juego.letrasErradas)
                {
                    model.WrongLetters += wLetter + ",";
                }
                model.GuessingWord = string.Empty;
                foreach (var rLetter in Juego.estadoAux)
                {
                    model.GuessingWord += rLetter + " ";
                }
                model.LetterTyped = string.Empty;
            }
            catch (FormatException e)
            {
                model.Message = "Ingrese un caracter válido";
            }
            catch (ArgumentNullException e)
            {
                model.Message = "Ingrese una letra o palabra";
            }
            return Json(model);
        }

        [HttpPost]
        public JsonResult TryWord(Hangman model)
        {
            model.Message = Juego.arriesgarPalabra(model.LetterTyped);
            model.Win = Juego.checkearEstadoActual();
            model.ChancesLeft = Juego.intentosRestantes;
            model.WrongLetters = string.Empty;
            foreach (var wLetter in Juego.letrasErradas)
            {
                model.WrongLetters += wLetter + ",";
            }
            model.GuessingWord = string.Empty;
            foreach (var rLetter in Juego.estadoAux)
            {
                model.GuessingWord += rLetter + " ";
            }
            model.LetterTyped = string.Empty;
            return Json(model);
        }
    }
}