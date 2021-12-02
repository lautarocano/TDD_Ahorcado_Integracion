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
            model.ChancesLeft = Juego.intentosRestantes;
            return Json(model);
        }

        [HttpPost]
        public JsonResult TryLetter(Hangman model)
        {
            Juego.validarLetra(Convert.ToChar(model.LetterTyped));
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