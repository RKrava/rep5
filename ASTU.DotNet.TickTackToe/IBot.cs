namespace ASTU.DotNet.TickTackToe
{
    public abstract class IBot
    {
        /// <summary>
        /// Название бота (например "Я бот Васи")
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Игровое поле
        /// </summary>
        public Field GameField { get; set; }

        /// <summary>
        /// Фигура, которой играет AI
        /// </summary>
        public CellState State;

        /// <summary>
        /// Конструктор IBoot
        /// </summary>
        /// <param name="gameField">Игровое поле</param>
        /// <param name="cellState">Фигура, которой играет бот (крестик или нолик)</param>
        /// <param name="name">Название бота (например "Я бот Васи")</param>
        protected IBot(Field gameField, CellState cellState, string name)
        {
            GameField = gameField;
            State = cellState;
            Name = name;
        }

        /// <summary>
        /// Сделать ход
        /// </summary>
        public abstract void MakeTurn();
    }
}
