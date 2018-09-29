using System;

namespace ASTU.DotNet.TickTackToe
{
    public class Cell : IEquatable<Cell>
    {
        /// <summary>
        /// Координата по оси X в Поле 
        /// </summary>
        public long X { get; set; }

        /// <summary>
        /// координата по оси Y в Поле
        /// </summary>
        public long Y { get; set; }

        /// <summary>
        /// State of cell
        /// </summary>
        public CellState State { get; set; }

        public Cell() {}

        public Cell(long x, long y)
        {
            X = x;    
            Y = y;
        }

        public Cell(long x, long y, CellState state) : this(x,y)
        {
            State = state;
        }

        public bool Equals(Cell other)
        {
            if (other!=null)
            {
                if (other.X == X && other.Y == Y && other.State == State)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
