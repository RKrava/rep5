using System;
using System.Linq;

namespace ASTU.DotNet.TickTackToe
{
    public class AI : IBot
    {

        /// <summary>
        /// Оценочный коэффициент (used at EvaluateFunction)
        /// </summary>
        public ulong EvalIndex { get; set; }

        protected float AttackFactor { get; set; }

        public AI(Field gameField, CellState cellState = CellState.Tick) : base(gameField,cellState,"Зверский бот")
        {
            AttackFactor = 0.5f;
            EvalIndex = 3;
        }

        /// <summary>
        /// Сделать ход
        /// </summary>
        public override void MakeTurn()
        {
            float evalMax = -1;
            int xBest = 0, yBest = 0;
            int right, left, top, bottom;
            right = top = left = bottom = 0;

            if (GameField.GetMarkedCells().Count!=0)
            {
                right = Convert.ToInt32(GameField.GetMarkedCells().Max(x => x.X))+1;
                left = Convert.ToInt32(GameField.GetMarkedCells().Min(x => x.X))-1;
                top = Convert.ToInt32(GameField.GetMarkedCells().Max(y => y.Y))+1;
                bottom = Convert.ToInt32(GameField.GetMarkedCells().Min(y => y.Y))-1;
            }
      

            for (int i = left; i <= right; i++)
            {
                for (int j = bottom; j <= top; j++)
                {
                    if (GameField[i,j].State==CellState.Empty)
                    {
                        var grade = EvaluateFunction(State, i, j) + EvaluateFunction(State == CellState.Tick ? CellState.Tack : CellState.Tick, i, j) * AttackFactor;
                        if (grade > evalMax)
                        {
                            evalMax = grade;
                            xBest = i;
                            yBest = j;
                        }
                    }
                }
            }
            if(evalMax==-1)
                throw new Exception("Ограничивающий прямоугольник поля не верный");
            GameField.Turn(State,xBest,yBest);
        }

        private ulong EvaluateFunction(CellState state, int x, int y)
        {
            int series_length = 0;
            ulong sum = 0;

            #region Сверху вниз
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if ((GameField[x - 4 + i + j, y].State != state) && (GameField[x - 4 + i + j, y].State != CellState.Empty))
                        {
                            //Конец ряда
                            series_length = 0;
                            break;

                        }
                        if (GameField[x - 4 + i + j, y].State != CellState.Empty) series_length++; //Ряд увеличивается
                    }
                    if (series_length == 4) 
                        series_length = 100; //Выигрышная ситуация, ставим большое значение

                    ulong powSt;

                    if (series_length == 100)
                    {
                        if (state == State)
                            powSt = 10000;//Большое значение при своем выигрыше
                        else
                            powSt = 1000; //Большое значение при выигрыше соперника, но меньшее, чем при своем
                    }
                    else
                        powSt = Convert.ToUInt64(Math.Pow(EvalIndex, series_length)); //Возводим оценочный коэффициент в степень длины серии
                
                    sum += powSt;
                    series_length = 0;
                }
            #endregion

            #region Слева направо

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if ((GameField[x, y - 4 + i + j].State != state) && (GameField[x, y - 4 + i + j].State != CellState.Empty))
                    {
                        //Конец ряда
                        series_length = 0;
                        break;
                    }
                    if (GameField[x, y - 4 + i + j].State != CellState.Empty) series_length++; //Ряд увеличивается
                }
                if (series_length == 4)
                    series_length = 100; //Выигрышная ситуация, ставим большое значение

                ulong powSt;

                if (series_length == 100)
                {
                    if (state == State)
                        powSt = 10000;//Большое значение при своем выигрыше
                    else
                        powSt = 1000; //Большое значение при выигрыше соперника, но меньшее, чем при своем
                }
                else
                    powSt = Convert.ToUInt64(Math.Pow(EvalIndex, series_length)); //Возводим оценочный коэффициент в степень длины серии
                sum += powSt;
                series_length = 0;
            }
            
            #endregion

            #region Диагональ с левого верхнего

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if ((GameField[x - 4 + i + j, y - 4 + i + j].State != state) && (GameField[x - 4 + i + j, y - 4 + i + j].State != CellState.Empty))
                    {
                        //Конец ряда
                        series_length = 0;
                        break;
                    }
                    if (GameField[x - 4 + i + j, y - 4 + i + j].State != CellState.Empty) series_length++; //Ряд увеличивается
                }
                if (series_length == 4)
                    series_length = 100; //Выигрышная ситуация, ставим большое значение

                ulong powSt;

                if (series_length == 100)
                {
                    if (state == State)
                        powSt = 10000;//Большое значение при своем выигрыше
                    else
                        powSt = 1000; //Большое значение при выигрыше соперника, но меньшее, чем при своем
                }
                else
                    powSt = Convert.ToUInt64(Math.Pow(EvalIndex, series_length)); //Возводим оценочный коэффициент в степень длины серии
                sum += powSt;
                series_length = 0;
            }

            #endregion

            #region Диагональ с левого нижнего

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if ((GameField[x + 4 - i - j, y - 4 + i + j].State != state) && (GameField[x + 4 - i - j, y - 4 + i + j].State != CellState.Empty))
                    {
                        //Конец ряда
                        series_length = 0;
                        break;
                    }
                    if (GameField[x + 4 - i - j, y - 4 + i + j].State != CellState.Empty) series_length++; //Ряд увеличивается
                }
                if (series_length == 4)
                    series_length = 100; //Выигрышная ситуация, ставим большое значение

                ulong powSt;

                if (series_length == 100)
                {
                    if (state == State)
                        powSt = 10000;//Большое значение при своем выигрыше
                    else
                        powSt = 1000; //Большое значение при выигрыше соперника, но меньшее, чем при своем
                }
                else
                    powSt = Convert.ToUInt64(Math.Pow(EvalIndex, series_length)); //Возводим оценочный коэффициент в степень длины серии
                sum += powSt;
                series_length = 0;
            }

            #endregion

            return sum;
        }
    }
}
