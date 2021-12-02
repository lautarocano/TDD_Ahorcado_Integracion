using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases
{
    public class Juego
    {

        public string nombreJugador;
        private string palabraSecreta;
        private string estadoPalabra;
        public int intentosRestantes;
        public char[] estadoAux;
        public List<char> letrasErradas;
        public Juego()
        {
            this.palabraSecreta = "hola";
            this.estadoPalabra = "____";
            this.estadoAux = estadoPalabra.ToCharArray();
            this.intentosRestantes = 5;
            this.letrasErradas = new List<char>();
        }

        public Juego(string palabraAsignada)
        {
            this.palabraSecreta = palabraAsignada.ToLower();
            this.estadoPalabra = "";
            for (int j = 0; j < palabraAsignada.Length; j++)
            {
                this.estadoPalabra += "_";
            }
            this.estadoAux = estadoPalabra.ToCharArray();
            this.intentosRestantes = 5;
            this.letrasErradas = new List<char>();
        }


        public string setName(string nombre)
        {
            if (nombre == "" || nombre.Length > 20 || !nombre.All(char.IsLetterOrDigit))
            {
                return "Nombre invalido";
            }
            else
            {
                this.nombreJugador = nombre;
                return "Nombre valido";
            }
        }

        public string arriesgarPalabra(string palabra)
        {
            if (string.IsNullOrWhiteSpace(palabra))
            {
                return "Palabra invalida";
            }
            else if (palabra.ToLower() == this.palabraSecreta)
            {
                return "Palabra correcta";

            }
            else
            {
                return "Palabra incorrecta";
            }
        }


        public bool arriesgarLetra(char letra)
        {
            if (char.IsLetter(letra))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public string validarLetra(char letra)
        {
            if (arriesgarLetra(letra))
            {
                if (palabraSecreta.Contains(char.ToLower(letra)))
                {
                    int cont = 0;
                    foreach (char c in palabraSecreta)
                    {
                        if (c == letra)
                        {
                            estadoAux[cont] = letra;
                        }
                        cont++;
                    }
                    return "Acierto";
                }
                else
                {
                    intentosRestantes--;
                    letrasErradas.Add(letra); //comprobar que no esté ya ingresada
                    return "Letra incorrecta";
                }
            }
            else
            {
                return "Letra invalida";
            }
        }

        public bool checkearEstadoActual()
        {
            if (estadoAux.Contains('_')) return false;
            else return true;



        }

        public string mostrarEstado()
        {
            string estadoreturn = new string(estadoAux);
            return estadoreturn;

        }
    }
}
