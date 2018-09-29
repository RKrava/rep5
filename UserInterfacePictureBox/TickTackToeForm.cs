using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using ASTU.DotNet.TickTackToe;

namespace UserInterfacePictureBox
{
    public partial class TickTackToeForm : Form
    {
        /// <summary>
        /// Size of cell in PictureBox
        /// </summary>
        public int SizeOfCell { get; set; }

        /// <summary>
        /// X-axis offset
        /// </summary>
        public int Dx{ get; set; }

        /// <summary>
        /// Y-axis offset
        /// </summary>
        public int Dy { get; set; }

        /// <summary>
        /// Model of game field
        /// </summary>
        public Field GameField { get; set; }
        
        public TickTackToeForm()
        {
            GameField = new Field();
            InitializeComponent();
            InitializeMyComponents();
        }

        private void InitializeMyComponents()
        {
            GameField.Clear();
            Dx = Dy = 0;
            SizeOfCell = 30;
            GameField.Turn(CellState.Tick, pictureBoxField.ClientSize.Width / 2 / SizeOfCell, pictureBoxField.ClientSize.Height / 2 / SizeOfCell);
            SetScrollBarValues();
        }

        private void DrowSet(Graphics g)
        {
            for (int i = 0; i < pictureBoxField.Width; i += 30)
            {
                var pen = new Pen(Color.Black, 1);

                g.DrawLine(pen, 0, i, pictureBoxField.Width, i);
                g.DrawLine(pen, i, 0, i, pictureBoxField.Height);
            }
        }

        private void PictureBoxFieldPaint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            DrowSet(g);
            g.SmoothingMode = SmoothingMode.HighQuality;
            

            var penTick = new Pen(Color.Black, 3);
            var penTack = new Pen(Color.Black, 3);

            int right, left, top, bottom;
            right = top = left = bottom = 0;

            if (GameField.GetMarkedCells().Count != 0)
            {
                right = Convert.ToInt32(GameField.GetMarkedCells().Max(x => x.X));
                left = Convert.ToInt32(GameField.GetMarkedCells().Min(x => x.X));
                top = Convert.ToInt32(GameField.GetMarkedCells().Max(y => y.Y));
                bottom = Convert.ToInt32(GameField.GetMarkedCells().Min(y => y.Y));
            }

            for (int i = left; i <= right; i++)
            {
                for (int j = bottom; j <= top; j++)
                {
                    if (GameField[i, j].State == CellState.Empty) continue;
                    if (GameField[i, j].State == CellState.Tick)
                    {
                        g.DrawLine(penTick, i * SizeOfCell + 9 - (float)Dx, j * SizeOfCell + 5 - (float)Dy, i * SizeOfCell + 21 - (float)Dx, j * SizeOfCell + 25 - (float)Dy);// крестик
                        g.DrawLine(penTick, i * SizeOfCell + 21 - (float)Dx, j * SizeOfCell + 5 - (float)Dy, i * SizeOfCell + 9 - (float)Dx, j * SizeOfCell + 25 - (float)Dy);
                    }
                    if (GameField[i, j].State == CellState.Tack)
                    {
                        g.DrawEllipse(penTack, i * 30 + 9 - (float)Dx, j * 30 + 5 - (float)Dy, 10, 20);
                    }
                }
            }
            if (GameField.IsEnd())
            {
                PaintWins(g);
            }
        }

        private void PictureBoxFieldMouseClick(object sender, MouseEventArgs e)
        {
            if (!GameField.IsEnd())
            {
                int x = 0, y = 0;
                while (x < e.X)
                    x += SizeOfCell;
                while (y < e.Y)
                    y += SizeOfCell;

                var xfield = (x - 30 + Dx) / SizeOfCell;
                var yfield = (y - 30 + Dy) / SizeOfCell;

                GameField.Turn(CellState.Tack, xfield, yfield);

                pictureBoxField.Invalidate();
                pictureBoxField.Refresh();

                if (GameField.IsEnd())
                {
                    MessageBox.Show(String.Format("Congrats!\nYou won"), "Game Over");
                    return;
                }

                var ai = new AI(GameField);
                ai.MakeTurn();

                pictureBoxField.Invalidate();
                pictureBoxField.Refresh();
                if (GameField.IsEnd())
                    MessageBox.Show(String.Format("Game Over\nBot won"), "Game Over");


            }
        }

        private void PaintWins(Graphics g)
        {
            if (GameField.IsEnd())
            {
                var winCells = GameField.GetWinCells();

                var penTick = new Pen(Color.Red, 3);
                var penTack = new Pen(Color.Red, 3);

                int right, left, top, bottom;
                right = top = left = bottom = 0;

                if (GameField.GetMarkedCells().Count != 0)
                {
                    right = Convert.ToInt32(winCells.Max(x => x.X));
                    left = Convert.ToInt32(winCells.Min(x => x.X));
                    top = Convert.ToInt32(winCells.Max(y => y.Y));
                    bottom = Convert.ToInt32(winCells.Min(y => y.Y));
                }

                for (int i = left; i <= right; i++)
                {
                    for (int j = bottom; j <= top; j++)
                    {
                        if (GameField[i, j].State == CellState.Empty) continue;
                        if (GameField[i, j].State == CellState.Tick && winCells.Contains(GameField[i, j]))
                        {
                            g.DrawLine(penTick, i * SizeOfCell + 9 - (float)Dx, j * SizeOfCell + 5 - (float)Dy, i * SizeOfCell + 21 - (float)Dx, j * SizeOfCell + 25 - (float)Dy);
                            g.DrawLine(penTick, i * SizeOfCell + 21 - (float)Dx, j * SizeOfCell + 5 - (float)Dy, i * SizeOfCell + 9 - (float)Dx, j * SizeOfCell + 25 - (float)Dy);
                        }
                        if (GameField[i, j].State == CellState.Tack && winCells.Contains(GameField[i, j]))
                        {
                            g.DrawEllipse(penTack, i * 30 + 9 - (float)Dx, j * 30 + 5 - (float)Dy, 10, 20);
                        }
                    }
                }
            }
        }

        public void SetScrollBarValues()
        {
            hScrollBar1.Minimum = 0;
            hScrollBar1.SmallChange = pictureBoxField.Width / 20;
            hScrollBar1.LargeChange = pictureBoxField.Width / 10;
            hScrollBar1.Maximum = Width - vScrollBar1.Width;  
            hScrollBar1.Value = (hScrollBar1.Maximum + hScrollBar1.Minimum)/2;
            

            vScrollBar1.Minimum = 0;
            vScrollBar1.SmallChange = pictureBoxField.Height / 20;
            vScrollBar1.LargeChange = pictureBoxField.Height / 10;
            vScrollBar1.Maximum = pictureBoxField.ClientSize.Height - hScrollBar1.Height;
            vScrollBar1.Value = (vScrollBar1.Maximum + vScrollBar1.Minimum)/2;
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue < e.OldValue)
            {
                Dx -= SizeOfCell;
                pictureBoxField.Invalidate();
                pictureBoxField.Refresh();
            }

            if (e.NewValue > e.OldValue)
            {
                Dx += SizeOfCell;
                pictureBoxField.Invalidate();
                pictureBoxField.Refresh(); 
            }

            if (e.NewValue == hScrollBar1.Minimum)
            {
                hScrollBar1.Value = hScrollBar1.Maximum / 2;
            }

            if (e.NewValue == hScrollBar1.Maximum)
            {
                hScrollBar1.Value = hScrollBar1.Maximum / 2;
            }
            hScrollBar1.Invalidate();
            hScrollBar1.Refresh();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue < e.OldValue)
            {
                Dy -= SizeOfCell;
                pictureBoxField.Invalidate();
                pictureBoxField.Refresh();
            }
            if (e.NewValue > e.OldValue)
            {
                Dy += SizeOfCell;
                pictureBoxField.Invalidate();
                pictureBoxField.Refresh();
            }
            if (e.NewValue == vScrollBar1.Minimum)
            {
                vScrollBar1.Value = vScrollBar1.Maximum / 2;
            }
            if (e.NewValue == vScrollBar1.Maximum)
            {
                vScrollBar1.Value = vScrollBar1.Maximum / 2;
            }
            vScrollBar1.Invalidate();
            vScrollBar1.Refresh();
        }

        private void TickTackToeForm_Resize(object sender, EventArgs e)
        {
            SetScrollBarValues();
        }

        private void NewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializeMyComponents();
            pictureBoxField.Invalidate();
            pictureBoxField.Refresh();
        }
    }
}
