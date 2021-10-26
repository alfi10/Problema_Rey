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
        public static readonly int[,] Movimientos = new int[,]
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
        private void Mover() // int[]
        {
            // Devuelve la coordenada del Rey por cada 
        }
        private List<int[]> AsignarDisponibles(int[,] movimientos)
        {
            List<int[]> disponibles = new List<int[]>();
            bool mueve_fuera_izq, mueve_fuera_arr, mueve_fuera_dch, mueve_fuera_abj;

            for (int i = 0; i < movimientos.GetLength(0); i++)
            {
                int[] movimientoAux = new int[2];

                movimientoAux[0] = movimientos[i, 0];
                movimientoAux[1] = movimientos[i, 1];

                mueve_fuera_izq = _posicion[0] == 0 && movimientoAux[0] == -1;
                mueve_fuera_dch = _posicion[0] == 7 && movimientoAux[0] == 1;
                mueve_fuera_arr = _posicion[1] == 0 && movimientoAux[1] == -1;
                mueve_fuera_abj = _posicion[1] == 7 && movimientoAux[1] == 1;

                if (mueve_fuera_izq || mueve_fuera_dch || mueve_fuera_arr || mueve_fuera_abj)
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
            Rey = new Rey();
            Tablero = new int[8, 8];
            Pasos = 0;
            Recorrible = true;
        }

        // Propiedades
        public Rey Rey { get; set; }
        public int[,] Tablero { get; set; }
        public int Pasos { get; set; }
        public bool Recorrible { get; set; }

        // Métodos
        private void ComprobarReyBloqueado(Rey rey) // bool
        {
            foreach (var movimiento in rey.MovimientosDisponibles)
            {
                
            }
        }
    }

}