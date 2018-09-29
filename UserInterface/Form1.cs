using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ASTU.DotNet.TickTackToe;

namespace UserInterface
{
    /// <summary>
    /// Пользователь играет Ноликами  (Tack)
    /// </summary>
    public partial class TickTackToeForm : Form
    {
        public TickTackToeForm()
        {
            InitializeComponent();
            GameField = new Field();
            XPadding = YPadding = 0;
        }

        /// <summary>
        /// Отступ, от истинного значения который появляется в результате добавления новых столбцев
        /// </summary>
        public int XPadding { get; set; }

        /// <summary>
        /// Отступ, от истинного значения который появляется в результате добавления новых строк
        /// </summary>
        public int YPadding { get; set; }

        /// <summary>
        /// Модель игрового поля
        /// </summary>
        public Field GameField { get; set; }

        /// <summary>
        /// Пользователь идет
        /// </summary>
        /// <param name="x">значение по оси х</param>
        /// <param name="y">значение по оси у</param>
        public void Turn(int x, int y)
        {
            fieldGridView[x, y].Value = Properties.Resources.Tack;
            GameField.Turn(CellState.Tack, x - XPadding, y - YPadding);

            if (GameField.IsEnd())
            {
                MessageBox.Show(String.Format("Конец, мой друг, победил Пользователь"),"Конец игры");
                return;
            }

            var ai = new AI(GameField);
            ai.MakeTurn();
            fieldGridView[(int)GameField.CurrentCell.X + XPadding, (int)GameField.CurrentCell.Y + YPadding].Value = Properties.Resources.Tick;
            
            if (GameField.IsEnd())
                MessageBox.Show(String.Format("Конец, мой друг, победил Бот"), "Конец игры");
        }
        
        private void TickTackToeFormLoad(object sender, EventArgs e)
        {
            fieldGridView.Rows.Add(20);
            GameField.Turn(CellState.Tick, fieldGridView.ColumnCount / 2, fieldGridView.RowCount / 2);
            fieldGridView[fieldGridView.ColumnCount / 2, fieldGridView.RowCount / 2].Value = Properties.Resources.Tick;
        }

        private void FieldGridViewCellClick(object sender, DataGridViewCellEventArgs e)
        {
            Turn(fieldGridView.CurrentCell.ColumnIndex,fieldGridView.CurrentCell.RowIndex);

        }

        private void RightButtonClick(object sender, EventArgs e)
        {
            fieldGridView.Columns.Add(new DataGridViewImageColumn { Width = 30, MinimumWidth = 30, Image = new Bitmap(30,30)});
        }

        private void LeftButtonClick(object sender, EventArgs e)
        {
            fieldGridView.Columns.Insert(0, new DataGridViewImageColumn() { Width = 30, MinimumWidth = 30, Image = new Bitmap(30, 30) });
            XPadding = XPadding + 1;
        }

        private void UpButtonClick(object sender, EventArgs e)
        {
            fieldGridView.Rows.Insert(0,1);
            YPadding = YPadding + 1;
        }

        private void DownButtonClick(object sender, EventArgs e)
        {
            fieldGridView.Rows.Add(1);
        }

    }
}
