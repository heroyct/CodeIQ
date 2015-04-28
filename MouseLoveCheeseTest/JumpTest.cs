using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MouseLoveCheese.MouseJump;
using System.Collections.Generic;
using System.Linq;

namespace MouseLoveCheeseTest
{
    [TestClass]
    public class JumpTest : MouseJumpTestBase
    {
        [TestMethod]
        public void TestGetCanJumpCells()
        {
            Jumper jump1 = new Jumper(GetTestTable1());
            List<Cell> cells1 = jump1.GetCanJumpCells(new Cell(0, 0), new JumpRoute(GetTestTable1()));
            // 可能なcell (2,1)(1,2)
            Assert.AreEqual(2, cells1.Count);
            Assert.AreEqual(true, cells1.Any(a => a.X == 2 && a.Y == 1));
            Assert.AreEqual(true, cells1.Any(a => a.X == 1 && a.Y == 2));

            Jumper jump2 = new Jumper(GetTestTable1());
            JumpRoute route = new JumpRoute(GetTestTable1());
            route.AddCell(new Cell(0, 3));
            List<Cell> cells2 = jump2.GetCanJumpCells(new Cell(2, 2), route);
            // 可能なcell (1,0)(0,1) (3,0)(4,1) (0,3)(1,4) (4,3)(3,4)
            Assert.AreEqual(7, cells2.Count);
            Assert.AreEqual(true, cells2.Any(a => a.X == 1 && a.Y == 0));
            Assert.AreEqual(true, cells2.Any(a => a.X == 0 && a.Y == 1));
            Assert.AreEqual(true, cells2.Any(a => a.X == 3 && a.Y == 0));
            Assert.AreEqual(true, cells2.Any(a => a.X == 4 && a.Y == 1));
            //Assert.AreEqual(true, cells2.Any(a => a.X == 0 && a.Y == 3));
            Assert.AreEqual(true, cells2.Any(a => a.X == 1 && a.Y == 4));
            Assert.AreEqual(true, cells2.Any(a => a.X == 4 && a.Y == 3));
            Assert.AreEqual(true, cells2.Any(a => a.X == 3 && a.Y == 4));
        }
    }
}
