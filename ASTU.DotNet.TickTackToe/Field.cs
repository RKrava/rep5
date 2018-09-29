using System;
using System.Collections.Generic;
using System.Linq;

namespace ASTU.DotNet.TickTackToe
{
    public class Field 
    {
        public Cell CurrentCell { get; set; }

        private List<Cell> _cells = new List<Cell>();

        private bool _isEnd = false;

        public Cell this[int x, int y]
        {
            get
            {
                var cellNow = _cells.Find(cell => cell.X == x && cell.Y == y);
                if (cellNow == null)
                {
                    return new Cell(x, y, CellState.Empty);
                }
                return cellNow;
            }
        }

        public void Turn(CellState cellState, int x, int y)
        {
            if (cellState == CellState.Empty)
                throw new ApplicationException("Impossible to turn using empty state");

            if (this[x, y].State != CellState.Empty)
                throw new ApplicationException("Impossible to turn to used cell");

            if (CurrentCell!=null && CurrentCell.State == cellState )
                throw new ApplicationException("Impossible to turn twice");


            CurrentCell = new Cell(x, y, cellState);
            _cells.Add(CurrentCell);
            CheckFinish();
        }

        /// <summary>
        /// Checks if some player has won
        /// </summary>
        void CheckFinish()
        {
            if (IsHorizontal() || IsVertical() || IsDiagonalMain() || IsDiagonalSecondary())
                _isEnd = true;
        }

        /// <summary>
        /// Checks of Main Diagonal
        /// </summary>
        /// <returns></returns>
        private bool IsDiagonalMain()
        {
            int counter = 0;
            for (long x = CurrentCell.X - 5, y = CurrentCell.Y - 5; x <= CurrentCell.X + 5 && y <= CurrentCell.Y + 5; x++, y++)
            {
                if (this[(int)x, (int)y].State == CurrentCell.State)
                    ++counter;
                else
                    counter = 0;

                if (counter == 5)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Checks of Secondary Diagonal
        /// </summary>
        /// <returns></returns>
        private bool IsDiagonalSecondary()
        {
            int counter = 0;
            for (long x = CurrentCell.X - 5, y = CurrentCell.Y + 5; x <= CurrentCell.X + 5 && y >= CurrentCell.Y - 5; x++, y--)
            {
                if (this[(int)x, (int)y].State == CurrentCell.State)
                    ++counter;
                else
                    counter = 0;

                if (counter == 5)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Vertical Check
        /// </summary>
        /// <returns></returns>
        private bool IsVertical()
        {
            long x = CurrentCell.X;
            int counter = 0;
            for (long i = CurrentCell.Y - 5; i <= CurrentCell.Y + 5; i++)
            {
                if (this[(int)x, (int)i].State == CurrentCell.State)
                    ++counter;
                else
                    counter = 0;

                if (counter == 5)
                    return true;
            }
            return false;
        }

        /// <summary>
        ///  Horizontal Check
        /// </summary>
        /// <returns></returns>
        private bool IsHorizontal()
        {
            long y = CurrentCell.Y;
            int counter = 0;
            for (long i = CurrentCell.X - 5; i <= CurrentCell.X + 5; i++)
            {
                if (this[(int)i,(int)y].State == CurrentCell.State)
                    ++counter;
                else
                    counter = 0;
                
                if (counter==5)
                    return true;
            }
            return false;
        }

        public bool IsEnd()
        {
            return _isEnd;
        }

        public IList<Cell> GetWinCells()
        {
            if (!IsEnd())
                throw new ArgumentException("The game has not been finished yet");
            if (IsDiagonalSecondary())
            {
                var cells = new List<Cell>();
                for (long x = CurrentCell.X - 5, y = CurrentCell.Y + 5; x <= CurrentCell.X + 5 && y >= CurrentCell.Y - 5; x++, y--)
                {
                    if (this[(int)x, (int)y].State==CurrentCell.State)
                    {
                        cells.Add(this[(int)x, (int)y]);
                    }
                }
                return cells;
            }
            if (IsDiagonalMain())
            {
                var cells = new List<Cell>();
                for (long x = CurrentCell.X - 5, y = CurrentCell.Y - 5; x <= CurrentCell.X + 5 && y <= CurrentCell.Y + 5; x++, y++)
                {
                    if (this[(int)x, (int)y].State == CurrentCell.State)
                    {
                        cells.Add(this[(int) x, (int) y]);
                    }
                }
                return cells;
            }
            if (IsVertical())
            {
                var cells = new List<Cell>();
                long x = CurrentCell.X;
                for (long i = CurrentCell.Y - 5; i <= CurrentCell.Y + 5; i++)
                {
                    if (this[(int)x, (int)i].State == CurrentCell.State)
                    {
                        cells.Add(this[(int) x, (int) i]);
                    }
                }
                return cells;
            }
            if (IsHorizontal())
            {
                var cells = new List<Cell>();
                long y = CurrentCell.Y;
                for (long i = CurrentCell.X - 5; i <= CurrentCell.X + 5; i++)
                {
                    if (this[(int)i, (int)y].State == CurrentCell.State)
                    {
                        cells.Add(this[(int) i, (int) y]);
                    }
                }
                return cells;
            }
            return null;
        }

        public IList<Cell> GetMarkedCells()
        {
            return _cells.Where(mCell => mCell.State != CellState.Empty).ToList();
        }

        public void Clear()
        {
            CurrentCell = null;
            _cells.Clear();
            _isEnd = false;
        }
    }
}
