using BibliotecaClases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestings
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IngresarNombreVacio()
        {
            Juego game = new Juego();
            string nombre = "";
            Assert.AreEqual(game.setName(nombre), "Nombre invalido");
        }

        [TestMethod]
        public void IngresarNombreLargo()
        {
            Juego game = new Juego();
            string nombre = "Guidolautarojuanmartin";
            Assert.AreEqual(game.setName(nombre), "Nombre invalido");
        }

        [TestMethod]
        public void IngresarNombreConEspacios()
        {
            Juego game = new Juego();
            string nombre = "Tigre Bengala";
            Assert.AreEqual(game.setName(nombre), "Nombre invalido");
        }

        [TestMethod]
        public void IngresarNombreConCaracEspeciales()
        {
            Juego game = new Juego();
            string nombre = "Martin!!#$%&";
            Assert.AreEqual(game.setName(nombre), "Nombre invalido");
        }

        [TestMethod]
        public void ArriesgarPalabraVacio()
        {
            Juego game = new Juego();
            game.setName("Martin");
            Assert.AreEqual(game.arriesgarPalabra(""), "Palabra invalida");
        }

        [TestMethod]
        public void ArriesgarPalabraIncorrecta()
        {
            Juego game = new Juego();
            game.setName("Martin");
            Assert.AreEqual(game.arriesgarPalabra("derrota"), "Palabra incorrecta");
        }

        [TestMethod]
        public void ArriesgarPalabraCorrecta()
        {
            Juego game = new Juego("Adios");
            game.setName("Martin");
            Assert.AreEqual(game.arriesgarPalabra("Adios"), "Palabra correcta");
        }

        [TestMethod]
        public void ArriesgarEspacioBlanco()
        {
            Juego game = new Juego("Adios");
            game.setName("Martin");
            Assert.AreEqual(game.arriesgarLetra(' '), false);
        }

        [TestMethod]
        public void ArriesgarNumero()
        {
            Juego game = new Juego("Adios");
            game.setName("Martin");
            Assert.AreEqual(game.arriesgarLetra('3'), false);
        }

        [TestMethod]
        public void ArriesgarLetraValida()
        {
            Juego game = new Juego("Adios");
            game.setName("Martin");
            Assert.AreEqual(game.arriesgarLetra('d'), true);
        }


        [TestMethod]
        public void ValidarLetraIncorrecta()
        {
            Juego game = new Juego("Adios");
            game.setName("Martin");
            Assert.AreEqual(game.validarLetra('z'), "Letra incorrecta");
        }

        [TestMethod]
        public void ValidarLetraCorrecta()
        {
            Juego game = new Juego("Adios");
            game.setName("Martin");
            Assert.AreEqual(game.validarLetra('d'), "Acierto");
        }


        [TestMethod]
        public void ValidarLetraCorrectaNoCaseSensitive()
        {
            Juego game = new Juego("Adios");
            game.setName("Martin");
            Assert.AreEqual(game.validarLetra('D'), "Acierto");
        }


        [TestMethod]
        public void ValidarEstadoLetra()
        {
            Juego game = new Juego("Adios");
            game.setName("Martin");
            game.validarLetra('d');
            Assert.AreEqual(game.mostrarEstado(), "_d___");
        }


        [TestMethod]
        public void EstadoLetraDistintoLugar()
        {
            Juego game = new Juego("Adios");
            game.setName("Martin");
            game.validarLetra('d');
            Assert.AreNotEqual(game.mostrarEstado(), "__d__");
        }


        [TestMethod]
        public void UnIntentoMenos()
        {
            Juego game = new Juego("Adios");
            game.setName("Martin");
            game.validarLetra('e');
            Assert.AreEqual(game.intentosRestantes, 5);
        }


        [TestMethod]
        public void NoRestaIntento()
        {
            Juego game = new Juego("Adios");
            game.setName("Martin");
            game.validarLetra('d');
            Assert.AreEqual(game.intentosRestantes, 6);
        }




    }
}
