using System;
using System.Collections.Generic;

namespace Problema_Rey
{
    public class Rey
    {
        // Constructores
        public Rey()
        {
            Posicion = GenerarPosicionAleat();
        }
        public Rey(int[] posicion)
        {
            Posicion = posicion;
        }

        // Constantes
        private static readonly int[,] Movimientos = new int[,]
        {
            // Arriba: izq, cent, dch
            {-1, -1},
            {0, -1},
            {1, -1},

            // Dch
            {1, 0},

            // Abajo: dch, cent, izq
            {1, 1},
            {0, 1},
            {-1, 1},

            // Izq
            {-1, 0}
        };
        
        // Variables
        private readonly int[] _posicion;

        // Propiedades
        public int[] Posicion
        {
            get => _posicion;
            private init
            {
                _posicion = value;
                MovimientosDisponibles = AsignarDisponibles(Movimientos);
            }
        }
        public List<int[]> MovimientosDisponibles { get; private init; } = new List<int[]>();

        // Métodos públicos
        public override string ToString()
        {
            return "X: " + Posicion[0] + ", Y: " + Posicion[1];
        }
        public int[] NuevaPosicion(int[] movimiento)
        {
            // Devuelve la posición del rey  en función de posición previa más el movimiento introducido
           
            if (!MovimientosDisponibles.Contains(movimiento))
            {
                throw new Exception("MovimientoImposible");
            }
            
            return new [] {Posicion[0] + movimiento[0], Posicion[1] + movimiento[1]};
        }  

        // Métodos privados
        private List<int[]> AsignarDisponibles(int[,] movimientos)
        {
            var disponibles = new List<int[]>();
            
            // Recorre todos los movimientos posibles del rey para determinar si no te sacan del tablero
            for (var i = 0; i < movimientos.GetLength(0); i++)
            {
                var movimientoAux = new int[2];

                movimientoAux[0] = movimientos[i, 0];
                movimientoAux[1] = movimientos[i, 1];

                var mueveFueraIzq = _posicion[0] == 0 && movimientoAux[0] == -1;
                var mueveFueraDch = _posicion[0] == Estado.DimensionesTablero[0]-1 && movimientoAux[0] == 1;
                var mueveFueraArr = _posicion[1] == 0 && movimientoAux[1] == -1;
                var mueveFueraAbj = _posicion[1] == Estado.DimensionesTablero[1]-1 && movimientoAux[1] == 1;

                if (mueveFueraIzq || mueveFueraDch || mueveFueraArr || mueveFueraAbj)
                {
                    continue;
                }

                disponibles.Add(movimientoAux);
            }

            return disponibles;
        }
        private static int[] GenerarPosicionAleat()
        {
            // Generador de números aleatorios
            var rand = new Random();

            return new[] {rand.Next(8), rand.Next(8)};
        }
    }

    public class Estado
    {
        // Constructores
        public Estado(int posicionInicialReyX = 0, int posicionInicialReyY = 0)
        {
            Rey = new Rey(new []{posicionInicialReyX, posicionInicialReyY});
            Tablero = new int[DimensionesTablero[0], DimensionesTablero[1]];
            Pasos = 1;
            Tablero.SetValue(Pasos, Rey.Posicion[0], Rey.Posicion[1]);
        }
        public Estado(Estado previo, int[] movimiento)
        {
            Rey = new Rey(previo.Rey.NuevaPosicion(movimiento));
            Tablero = ActualizarTablero(previo.Tablero);
            Pasos = previo.Pasos + 1;
            Tablero.SetValue(Pasos, Rey.Posicion[0], Rey.Posicion[1]);
        }
        
        // Constante
        public static readonly int[] DimensionesTablero = new[]
        {
            // Dimensión X:
            4,
            // Dimensión Y:
            3
        };

        public static readonly int LengthTablero = DimensionesTablero[0] * DimensionesTablero[1];
        
        // Variables

        // Propiedades
        public Rey Rey { get; private init; }

        public int[,] Tablero { get; private init; }

        public int Pasos { get; private init; }

        // Métodos privados
        private static int[,] ActualizarTablero(int[,] tableroPrevio)
        {
            var devolver = new int[tableroPrevio.GetLength(0), tableroPrevio.GetLength(1)];
            Array.Copy(tableroPrevio, 0, devolver, 0, tableroPrevio.Length);
            return devolver;
        }
    }
}