using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASTU.DotNet.TickTackToe {

    /// <summary>
    /// Testing of Cell.cs
    /// </summary>
    [TestClass]
    public class CellTest {

        /// <summary>
        /// Testing of default constructor
        /// </summary>
        [TestMethod]
        public void TestCellConstructor() {
            var cell = new Cell(7, 10);
            Assert.AreEqual(CellState.Empty, cell.State);
            Assert.AreEqual(7, cell.X);
            Assert.AreEqual(10, cell.Y);
        }

        /// <summary>
        /// Testing of init constructor 
        /// </summary>
        [TestMethod]
        public void TestCellInitConstructor() {
            var cell = new Cell(7, 10, CellState.Tick);
            Assert.AreEqual(CellState.Tick, cell.State);
            Assert.AreEqual(7, cell.X);
            Assert.AreEqual(10, cell.Y);

            cell = new Cell(7, 10, CellState.Tack);
            Assert.AreEqual(CellState.Tack, cell.State);
            Assert.AreEqual(7, cell.X);
            Assert.AreEqual(10, cell.Y);
        }

        /// <summary>
        /// Testing of three parameters' constructor 
        /// </summary>
        [TestMethod]
        public void TestCellStateProperty() {
            var cell = new Cell(7, 10, CellState.Tick);
            Assert.AreEqual(CellState.Tick, cell.State);
            
            cell.State = CellState.Tack;
            Assert.AreEqual(CellState.Tack, cell.State);

            cell.State = CellState.Tick;
            Assert.AreEqual(CellState.Tick, cell.State);

            cell.State = CellState.Empty;
            Assert.AreEqual(CellState.Empty, cell.State);
        }
    }
}
