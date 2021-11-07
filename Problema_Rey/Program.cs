using System;
using System.Collections.Generic;

namespace Problema_Rey
{
    class Program
    {
        static void Main()
        {
            var soluciones = Bacotraco(new Estado(), new List<Estado>());

            // for (var x = 0; x < Estado.DimensionesTablero[0]; x++)
            // {
            //     for (var y = 0; y < Estado.DimensionesTablero[1]; y++)
            //     {
            //         Bacotraco(new Estado(x, y));
            //         Console.WriteLine("\nSoluciones: "+soluciones.Count);
            //
            //     }
            // }
            
            PrintSoluciones(soluciones);

            Console.WriteLine("\nSoluciones: "+soluciones.Count);
        }

        private static List<Estado> Bacotraco(Estado nodo,  List<Estado> soluciones)
        {
            foreach (var movimiento in nodo.Rey.MovimientosDisponibles)
            {
                // Crea un siguiente estado con el rey en la posición dada por su movimiento
                var siguiente = new Estado(nodo, movimiento);
                
                // Si la nueva posición no está en un punto visitado previamente por el rey, entra en el backtrack
                if (NoHaySolapamiento(nodo.Tablero, siguiente.Rey.Posicion))
                {
                    Bacotraco(siguiente, soluciones);
                }
            }

            if (nodo.Pasos == Estado.LengthTablero) soluciones.Add(nodo);
            
            return soluciones;

        }

        private static void PrintSoluciones(List<Estado> soluciones)
        {
            // Imprime todas las soluciones de la lista global de soluciones
            foreach (var solucion in soluciones)
            {
                PrintTablero(solucion.Tablero);
            }
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