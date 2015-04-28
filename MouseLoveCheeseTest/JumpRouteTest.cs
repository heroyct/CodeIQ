using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MouseLoveCheese.MouseJump;

namespace MouseLoveCheeseTest
{
    [TestClass]
    public class JumpRouteTest : MouseJumpTestBase
    {
        [TestMethod]
        public void TestAddCell()
        {
            JumpRoute route1 = new JumpRoute(GetTestTable1());
            route1.Complete = true;
            Assert.AreEqual(false, route1.AddCell(new Cell(2, 1)));
            Assert.AreEqual(0, route1.CellsCount);

            JumpRoute route2 = new JumpRoute(GetTestTable1());
            Assert.AreEqual(true, route2.AddCell(new Cell(0, 0)));
            Assert.AreEqual(true, route2.AddCell(new Cell(2, 1)));
            Assert.AreEqual(true, route2.AddCell(new Cell(0, 2)));
            Assert.AreEqual(false, route2.AddCell(new Cell(2, 1)));
            Assert.AreEqual(3, route2.CellsCount);

            JumpRoute route3 = new JumpRoute(GetTestTable1());
            route3.AddCell(new Cell(0, 0));
            route3.AddCell(new Cell(2, 1));
            Assert.AreEqual(true, route3.AddCell(new Cell(0, 2)));
            Assert.AreEqual(3, route3.CellsCount);
        }
    }
}
