using System;

namespace Problema_Rey
{
    class Program
    {
        static void Main()
        {
            var rey = new Rey(new int[] {0, 0});
            var tablero = new int[8, 8];
            
            for (int i = 1; i <= 64; i++)
            {
                // Actualiza la posición del rey en el tablero
                tablero[rey.Posicion[0],rey.Posicion[1]] = i;

                foreach (var movimiento in rey.MovimientosDisponibles)
                {
                    var nuevaPosX = rey.Posicion[0] + movimiento[0];
                    var nuevaPosY = rey.Posicion[1] + movimiento[1];
                    
                    // Si el rey no ha visitado esa posición, mueve el rey ahí
                    if (tablero[nuevaPosX, nuevaPosY]==0)
                    {
                        rey.Posicion = new[] {nuevaPosX, nuevaPosY};
                        break;
                    }
                }
            }
            
            PrintTablero(tablero);
        }

        private static void PrintTablero(int[,] tablero)
        {
            // Dimensión xX del array en 'x' (0)
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
                    Console.Write(" " + tablero[i, j] + " ");
                }

                Console.WriteLine("");
            }
        }
    }
}