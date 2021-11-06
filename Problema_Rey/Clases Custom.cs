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
        private int[] _posicion;
        private List<int[]> _movimientosDisponibles = new List<int[]>();

        // Propiedades
        public int[] Posicion
        {
            get => _posicion;
            set
            {
                _posicion = value;
                _movimientosDisponibles = AsignarDisponibles(Movimientos);
            }
        }
        public List<int[]> MovimientosDisponibles => _movimientosDisponibles;

        // Métodos públicos
        public override string ToString()
        {
            return "X: " + Posicion[0] + ", Y: " + Posicion[1];
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
                var mueveFueraDch = _posicion[0] == 7 && movimientoAux[0] == 1;
                var mueveFueraArr = _posicion[1] == 0 && movimientoAux[1] == -1;
                var mueveFueraAbj = _posicion[1] == 7 && movimientoAux[1] == 1;

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
        public Estado()
        {
            Rey = new Rey(new []{0,0});
            Tablero = new int[8, 8];
            Pasos = 1;
            Tablero.SetValue(Pasos, Rey.Posicion[0], Rey.Posicion[1]);
        }
        public Estado(Estado previo, int[] movimiento)
        {
            // Rey = rey;
            // Tablero = tablero;
            // Pasos = pasos;
        }
        
        // Variables
        private Rey _rey;
        private int[,] _tablero;
        private int _pasos;

        // Propiedades
        public Rey Rey
        {
            get => _rey;
            set => _rey = value;
        }
        public int[,] Tablero
        {
            get => _tablero;
            set => _tablero = value;
        }
        public int Pasos
        {
            get => _pasos;
            set => _pasos = value;
        }
    }
}