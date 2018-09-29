using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASTU.DotNet.TickTackToe
{
    /// <summary>
    /// Testing of Field.cs
    /// </summary>
    [TestClass]
    public class FieldTest {

        /// <summary>
        /// Testing of costructor which creates empty field
        /// </summary>
        [TestMethod]
        public void TestFieldConstructor() {
            var field = new Field();
            for (long x = Int32.MinValue; x < Int32.MaxValue; x += (Math.Abs(x) / 2 + 1)) {
                for (long y = Int32.MinValue; y < Int32.MaxValue; y += (Math.Abs(y) / 2 + 1)) {
                    Assert.AreEqual(CellState.Empty, field[(int)x, (int)y].State);
                    Assert.AreEqual(x, field[(int)x, (int)y].X);
                    Assert.AreEqual(y, field[(int)x, (int)y].Y);
                }
            }
        }

        /// <summary>
        /// Successfull tick turn
        /// </summary>
        [TestMethod]
        public void TestTurnTickSuccess() {
            var field = new Field();
            field.Turn(CellState.Tick, 0, 0);
            Assert.AreEqual(CellState.Tick, field[0, 0].State);
            Assert.AreEqual(CellState.Empty, field[0, 1].State);
            Assert.AreEqual(CellState.Empty, field[1, 0].State);
            Assert.AreEqual(CellState.Empty, field[1, 1].State);
            Assert.AreEqual(CellState.Empty, field[0, -1].State);
            Assert.AreEqual(CellState.Empty, field[-1, 0].State);
            Assert.AreEqual(CellState.Empty, field[-1, -1].State);
            Assert.AreEqual(CellState.Empty, field[-1, 1].State);
            Assert.AreEqual(CellState.Empty, field[1, -1].State);
            Assert.AreEqual(CellState.Empty, field[Int32.MaxValue, Int32.MaxValue].State);
        }


        /// <summary>
        /// Successfull tack turn
        /// </summary>
        [TestMethod]
        public void TestTurnTackSuccess() {
            var field = new Field();
            field.Turn(CellState.Tack, 0, 0);
            Assert.AreEqual(CellState.Tack, field[0, 0].State);
            Assert.AreEqual(CellState.Empty, field[0, 1].State);
            Assert.AreEqual(CellState.Empty, field[1, 0].State);
            Assert.AreEqual(CellState.Empty, field[1, 1].State);
            Assert.AreEqual(CellState.Empty, field[0, -1].State);
            Assert.AreEqual(CellState.Empty, field[-1, 0].State);
            Assert.AreEqual(CellState.Empty, field[-1, -1].State);
            Assert.AreEqual(CellState.Empty, field[-1, 1].State);
            Assert.AreEqual(CellState.Empty, field[1, -1].State);
            Assert.AreEqual(CellState.Empty, field[Int32.MaxValue, Int32.MaxValue].State);
        }

        /// <summary>
        /// Successfull turn to max coordinates of the field
        /// </summary>
        [TestMethod]
        public void TestTurnWithMaxXYSuccess() {
            var field = new Field();
            field.Turn(CellState.Tick, Int32.MaxValue, Int32.MaxValue);
            Assert.AreEqual(CellState.Tick, field[Int32.MaxValue, Int32.MaxValue].State);
        }

        /// <summary>
        /// Successfull turn to min coordinates of the field
        /// </summary>
        [TestMethod]
        public void TestTurnWithMinXYSuccess() {
            var field = new Field();
            field.Turn(CellState.Tick, Int32.MinValue, Int32.MinValue);
            Assert.AreEqual(CellState.Tick, field[Int32.MinValue, Int32.MinValue].State);
        }

        /// <summary>
        /// Attempt to go by empty state should throwing ApplicationException 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Impossible to turn using empty state", AllowDerivedTypes = true)]
        public void TestTurnEmptyCellFail() {
            var field = new Field();
            field.Turn(CellState.Tick, 0, 1);
            field.Turn(CellState.Empty, 0, 0);
        }

        /// <summary>
        /// Attempt to go to used cell should throwing ApplicationException 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Impossible to turn to used cell", AllowDerivedTypes = true)]
        public void TestTurnToOccupatedCellFail() {
            var field = new Field();
            field.Turn(CellState.Tick, 0, 0);
            field.Turn(CellState.Tack, 0, 0);
        }

        /// <summary>
        /// Attempt to go to used cell should throwing ApplicationException 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Impossible to turn to used cell", AllowDerivedTypes = true)]
        public void TestAnywayTurnToOccupatedCellFail() {
            var field = new Field();
            field.Turn(CellState.Tick, 0, 0);
            field.Turn(CellState.Tack, 0, 1);
            Assert.AreEqual(CellState.Tick, field[0, 0].State);
            Assert.AreEqual(CellState.Tack, field[0, 1].State);
            field.Turn(CellState.Tick, 0, 0);
        }

        /// <summary>
        /// Attempt to double turn should throwing ApplicationException 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Impossible to turn twice", AllowDerivedTypes = true)]
        public void TestTurnTwiceWithTickFail() {
            var field = new Field();
            field.Turn(CellState.Tick, 0, 0);
            field.Turn(CellState.Tick, 0, 1);
        }

        /// <summary>
        /// Attempt to double turn should throwing ApplicationException 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Impossible to turn twice", AllowDerivedTypes = true)]
        public void TestTurnTwiceWithTackFail() {
            var field = new Field();
            field.Turn(CellState.Tack, 0, 0);
            field.Turn(CellState.Tack, 0, 1);
        }

        /// <summary>
        /// Check if the game has been finished
        /// </summary>
        [TestMethod]
        [Timeout(200)]
        public void TestGameEnd1() {
            var field = new Field();

            PlayTurns(field, CellState.Tick, new[] {
                new[] {0, 0},
                new[] {0, 1},
                new[] {1, 0},
                new[] {1, 1},
                new[] {-1, 0},
                new[] {-1, 1},
                new[] {2, 0},
                new[] {2, 1},
                new[] {-2, 0}                
            });
            Assert.IsTrue(field.IsEnd());
        }

        /// <summary>
        /// Check if the game has been finished
        /// </summary>
        [TestMethod]
        [Timeout(200)]
        public void TestGameEnd2() {
            var field = new Field();

            PlayTurns(field, CellState.Tack, new[] {
                new[] {0, 0},
                new[] {1, 0},
                new[] {0, 1},
                new[] {1, 1},
                new[] {0, -1},
                new[] {-1, 1},
                new[] {0, 2},
                new[] {2, 1},
                new[] {0, -2}                
            });
            Assert.IsTrue(field.IsEnd());
        }

        /// <summary>
        /// Check if the game has been finished
        /// </summary>
        [TestMethod]
        [Timeout(200)]
        public void TestGameEnd3() {
            var field = new Field();

            PlayTurns(field, CellState.Tack, new[] {
                new[] {0, 0},
                new[] {-10, 10},
                new[] {1, 1},
                new[] {11, 1},
                new[] {-1, -1},
                new[] {-11, 1},
                new[] {2, 2},
                new[] {2, 1},
                new[] {-2, -2}                
            });
            Assert.IsTrue(field.IsEnd());
        }

        /// <summary>
        /// Check if the game has been finished
        /// </summary>
        [TestMethod]
        [Timeout(200)]
        public void TestGameEnd4() {
            var field = new Field();
            PlayTurns(field, CellState.Tick, new[] {
                new[] {0, 0},
                new[] {-1, 11},
                new[] {-1, 1},
                new[] {14, 1},
                new[] {1, -1},
                new[] {-41, 1},
                new[] {-2, 2},
                new[] {-3, 3},
                new[] {2, -2}                
            });
            Assert.IsTrue(field.IsEnd());
        }

        /// <summary>
        /// Getting correct state of cell
        /// </summary>
        [TestMethod]
        [Timeout(200)]
        public void TestGetMarkedCells() {
            var field = new Field();
            PlayTurns(field, CellState.Tick, new[] {
                new[] {0, 0},
                new[] {-1, 11},
                new[] {-1, 1},
                new[] {14, 1},
                new[] {1, -1},
                new[] {-41, 1},
                new[] {-2, 2},
                new[] {-3, 3},
                new[] {2, -2}                
            });

            IList<Cell> cells = field.GetMarkedCells();
            Assert.AreEqual(9, cells.Count);
            foreach (var cell in cells) {
                Assert.AreNotEqual(CellState.Empty, cell.State);
            }
        }

        private void PlayTurns(Field field, CellState whoBegins, int[][] positions) {
            var currentPlayer = whoBegins;
            foreach (var position in positions) {
                Assert.IsFalse(field.IsEnd());
                field.Turn(currentPlayer, position[0], position[1]);
                currentPlayer = (currentPlayer == CellState.Tick) ? CellState.Tack : CellState.Tick;
            }
        }
    }
}
