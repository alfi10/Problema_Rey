using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Problema_Rey
{
    class Program
    {
        static void Main()
        {
            // Backtrack
            var soluciones = Bacotraco(new Estado(), new List<Estado>(), 3000);
            
            // Halla las longitudes distintas entre las soluciones
            var longitudes = new List<double>();
            foreach (var solucion in soluciones.Where(solucion => !longitudes.Contains(solucion.Longitud)))
            {
                longitudes.Add(solucion.Longitud);
            }
            
            // Halla el valor máximo de las longitudes de recorrido
            var max = soluciones.Count(miembro => miembro.Longitud.Equals(longitudes.Max()));
            
            
            // Display de información por consola
            Console.WriteLine("\nSoluciones: "+soluciones.Count);
            Console.WriteLine("Long. max: "+longitudes.Max());
            Console.WriteLine("Soluciones de Long. max: "+max);
        }

        private static List<Estado> Bacotraco(Estado nodo, List<Estado> soluciones, int tiempoEnMs=0, Stopwatch crono=null)
        {
            
            // Si no se le ha pasado un crono, lo instancia y empieza a contar
            if (crono == null)
            {
                crono = new Stopwatch();
                crono.Start();
            }
            // Si el tiempo es 0, la función no parará por tiempo
            else if(tiempoEnMs!= 0)
            {
                // Si el tiempo ejecutado es mayor que el límite establecido, devuelve la solución obtenida hasta el momento
                if (crono.ElapsedMilliseconds > tiempoEnMs) return soluciones;
            }
            
            // Bucle que entra en cada ramificación posible del nodo evaluado
            foreach (var siguiente in nodo.Rey.MovimientosDisponibles.Select(movimiento => new Estado(nodo, movimiento)).Where(siguiente => NoHaySolapamiento(nodo.Tablero, siguiente.Rey.Posicion)))
            {
                Bacotraco(siguiente, soluciones, tiempoEnMs, crono);
            }
            
            // Añade el nodo que corresponde a una solución
            if (nodo.Pasos != Estado.LengthTablero) return soluciones;
            soluciones.Add(nodo);
            return soluciones;
        }

        private static int[] EncontrarCoordenadaInicial(int[,] tablero)
        {
            for (var x = 0; x < tablero.GetLength(0); x++)
            {
                for (var y = 0; y < tablero.GetLength(1); y++)
                {
                    if (tablero[x, y] == 1)
                    {
                        return new[] {x, y};
                    }
                }
            }
        
            throw new Exception("NoHayCoordenadaInicial");
        }
        
        private static void PrintSoluciones(List<Estado> soluciones)
        {
            // Imprime todas las soluciones de la lista global de soluciones
            foreach (var solucion in soluciones)
            {
                PrintTablero(solucion.Tablero);
                Console.WriteLine("\tLongitud recorrida: "+solucion.Longitud);
                Console.WriteLine("");
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