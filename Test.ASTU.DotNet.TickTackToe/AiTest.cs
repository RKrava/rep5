using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASTU.DotNet.TickTackToe
{
    [TestClass]
    public class AITest
    {
        [TestMethod]
        public void LastStepUptoDown()
        {
            var field = new Field();
            PlayTurns(field, CellState.Tick, new[] {
                new[] {-5, 0},
                new[] {-1, 11},
                new[] {-4, 0},
                new[] {14, 1},
                new[] {-3, 0},
                new[] {-41, 1},
                new[] {-2, 0},
                new[] {0, 0} 
            });
            var ai = new AI(field);
            ai.MakeTurn();
            Assert.AreEqual(field.IsEnd(),true);
        }

        [TestMethod]
        public void LastStepLefttoRight()
        {
            var field = new Field();
            PlayTurns(field, CellState.Tick, new[] {
                new[] {0, -5},
                new[] {-1, 11},
                new[] {0, -4},
                new[] {14, 1},
                new[] {0, -3},
                new[] {-41, 1},
                new[] {0, -2},
                new[] {-6, 0}
            });
            var ai = new AI(field);
            ai.MakeTurn();
            Assert.AreEqual(field.IsEnd(), true);
        }

        [TestMethod]
        public void ProtectionBehaviorThreeStep()
        {
            var field = new Field();
            PlayTurns(field, CellState.Tack, new[] {
                new[] {0, 0},
                new[] {-1, 11},
                new[] {0, 1},
                new[] {14, 1},
                new[] {0, -1},
            });
            var ai = new AI(field);
            ai.MakeTurn();
            field.Turn(CellState.Tack, 11,11);
            ai.MakeTurn();

            Assert.AreEqual(field[0,2].State==CellState.Tick, true);
            Assert.AreEqual(field[0,-2].State==CellState.Tick, true);
        }         

        private void PlayTurns(Field field, CellState whoBegins, int[][] positions)
        {
            var currentPlayer = whoBegins;
            foreach (var position in positions)
            {
                Assert.IsFalse(field.IsEnd());
                field.Turn(currentPlayer, position[0], position[1]);
                currentPlayer = (currentPlayer == CellState.Tick) ? CellState.Tack : CellState.Tick;
            }
        }
    }
}
