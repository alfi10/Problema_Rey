using System;
using System.Collections.Generic;

namespace Problema_Rey
{
    class Program
    {
        public static List<Estado> soluciones = new List<Estado>();
        
        static void Main()
        {
            Bacotraco(new Estado());
            Console.WriteLine("\nSoluciones: "+soluciones.Count);
        }

        private static void Bacotraco(Estado nodo)
        {
            foreach (var movimiento in nodo.Rey.MovimientosDisponibles)
            {
                // Crea un siguiente estado con el rey en la posición dada por su movimiento
                var siguiente = new Estado(nodo, movimiento);
                
                // Si la nueva posición no está en un punto visitado previamente por el rey, entra en el backtrack
                if (NoHaySolapamiento(nodo.Tablero, siguiente.Rey.Posicion))
                {
                    Bacotraco(siguiente);
                }
            }

            if (nodo.Pasos != Estado.LengthTablero) return;
            PrintTablero(nodo.Tablero);
            soluciones.Add(nodo);
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
        
        private static bool NoHaySolapamiento(int[,] tableroPrevio, IReadOnlyList<int> posicionReyActual)
        {
            return tableroPrevio[posicionReyActual[0], posicionReyActual[1]] == 0;
        }
    }
}