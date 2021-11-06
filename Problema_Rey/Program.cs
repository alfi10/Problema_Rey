using System;
using System.Collections.Generic;

namespace Problema_Rey
{
    class Program
    {
        static void Main()
        {
            Bacotraco(new Estado());
            Console.WriteLine("Acabó!");
        }

        private static void Bacotraco(Estado nodo)
        {
            // Copia el nodo en una nueva variable que almacenará cada ramificación posible desde el nodo actual
            // en función del movimiento que haga el rey
            var siguiente = new Estado();

            foreach (var movimiento in nodo.Rey.MovimientosDisponibles)
            {
                siguiente.Rey = ActualizarRey(nodo.Rey.Posicion, movimiento);
                siguiente.Pasos = nodo.Pasos+1;
                siguiente.Tablero = ActualizarTablero(nodo.Tablero, siguiente.Rey.Posicion, siguiente.Pasos);
                
                if (NoHaySolapamiento(nodo.Tablero, siguiente.Rey.Posicion))
                {
                    // Crea un siguiente estado con el rey en la posición dada por su movimiento
                    Bacotraco(siguiente);
                }
            }

            if (nodo.Pasos != 64) return;
            PrintTablero(nodo.Tablero);
        }

        private static void PrintTablero(int[,] tablero)
        {
            // Dimensión X del array en 'x' (0)
            // Dimensión Y del array en 'y' (1)
            const int x = 0;
            const int y = 1;

            for (var i = 0; i < tablero.GetLength(x); i++)
            {
                for (var j = 0; j < tablero.GetLength(y); j++)
                {
                    // Escribe el contenido de cada casilla del tablero
                    // El tablero almacena si el rey ha pasado por una casilla y en que movimiento
                    // 0: No ha pasado
                    // [1, 64]: el movimiento en el que pasó
                    Console.Write("\t" + tablero[i, j]);
                }

                Console.WriteLine("");
            }
            Console.WriteLine("");
        }

        private static Rey ActualizarRey(IReadOnlyList<int> posicion, IReadOnlyList<int> movimiento)
        {
            var nuevaPosicion = new [] {posicion[0] + movimiento[0], posicion[1] + movimiento[1]};
            return new Rey(nuevaPosicion);
        }

        private static int[,] ActualizarTablero(int[,] tablero, IReadOnlyList<int> posicionRey, int paso)
        {
            // Crea el tablero que se devolverá.
            var nuevoTablero = new int[8, 8];
            // Lo llena con los valores del tablero previo
            Array.Copy(tablero, 0, nuevoTablero, 0, tablero.Length);
            // introduce el dato del nuevo movimiento en el tablero nuevo
            nuevoTablero.SetValue(paso, posicionRey[0], posicionRey[1]);
            return nuevoTablero;
        }

        private static bool NoHaySolapamiento(int[,] tableroPrevio, IReadOnlyList<int> posicionReyActual)
        {
            return tableroPrevio[posicionReyActual[0], posicionReyActual[1]] == 0;
        }
    }
}