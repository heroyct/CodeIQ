using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MouseLoveCheese.MouseJump;
using System.Linq;

namespace MouseLoveCheeseTest
{
    [TestClass]
    public class TableTest : MouseJumpTestBase
    {
        [TestMethod]
        public void TestAddCheesse()
        {
            Table tt = new Table(3, 3);
            Assert.AreEqual(false, tt.AddCheesse(new Cell(-1, 0)));
            Assert.AreEqual(0, tt.CheeseCells.Count());
            Assert.AreEqual(true, tt.AddCheesse(new Cell(1, 0)));
            Assert.AreEqual(true, tt.AddCheesse(new Cell(2, 0)));
            Assert.AreEqual(2, tt.CheeseCells.Count());
            Assert.AreEqual(false, tt.AddCheesse(new Cell(1, 0)));
            Assert.AreEqual(2, tt.CheeseCells.Count());
        }

        [TestMethod]
        public void TesCanEatAllCheese()
        {
            Table t1 = GetTestTable1();
            Assert.AreEqual(true, t1.CanEatAllCheese(GetJumpRoute1()));

            Table t2 = new Table(2, 2);
            Assert.AreEqual(false, t2.CanEatAllCheese(new JumpRoute(t2)));

            Table t3 = GetTestTable3();
            JumpRoute route = new JumpRoute(t3);
            route.AddCell(new Cell(0, 0));
            Assert.AreEqual(false, t3.CanEatAllCheese(route));
            route.AddCell(new Cell(1, 2));
            Assert.AreEqual(true, t3.CanEatAllCheese(route));
        }

        [TestMethod]
        public void TestIsValidCell()
        {
            Assert.AreEqual(true, GetTestTable1().Exists(new Cell(0, 0)));
            Assert.AreEqual(true, GetTestTable1().Exists(new Cell(4, 0)));
            Assert.AreEqual(false, GetTestTable1().Exists(new Cell(-1, 0)));
            Assert.AreEqual(false, GetTestTable1().Exists(new Cell(5, 0)));

            Assert.AreEqual(true, GetTestTable1().Exists(new Cell(0, 0)));
            Assert.AreEqual(true, GetTestTable1().Exists(new Cell(0, 4)));
            Assert.AreEqual(false, GetTestTable1().Exists(new Cell(0, -1)));
            Assert.AreEqual(false, GetTestTable1().Exists(new Cell(0, 5)));

            Assert.AreEqual(false, GetTestTable1().Exists(new Cell(-1, -1)));
        }

        [TestMethod]
        public void TestCanJumpeToNext()
        {
            JumpRoute route1 = new JumpRoute(GetTestTable1());
            route1.AddCell(new Cell(0, 0));
            Assert.AreEqual(true, GetTestTable1().CanJumpeToNext(route1, new Cell(2, 1)));

            JumpRoute route2 = new JumpRoute(GetTestTable1());
            foreach (var tt in GetTestTable1().CheeseCells)
            {
                route2.AddCell(tt);
            }
            Assert.AreEqual(false, GetTestTable1().CanJumpeToNext(route2, new Cell(2, 1)));

            JumpRoute route3 = new JumpRoute(GetTestTable1());
            route3.AddCell(new Cell(0, 0));
            route3.AddCell(new Cell(2, 1));
            Assert.AreEqual(false, GetTestTable1().CanJumpeToNext(route3, new Cell(2, 1)));

            JumpRoute route4 = new JumpRoute(GetTestTable1());
            route4.AddCell(new Cell(0, 0));
            route4.AddCell(new Cell(2, 1));
            Assert.AreEqual(true, GetTestTable1().CanJumpeToNext(route4, new Cell(3, 3)));

            // (0,0) (1,2) (2,0) (3,2) (1,1) (3,0) (2,2) (1,0) (0,2) (2,1) (4,0)
            JumpRoute route5 = new JumpRoute(GetTestTable1());
            route5.AddCell(new Cell(0, 0));
            route5.AddCell(new Cell(2, 1));
            route5.AddCell(new Cell(3, 2));
            route5.AddCell(new Cell(4, 0));
            Assert.AreEqual(false, GetTestTable1().CanJumpeToNext(route5, new Cell(2, 1)));

            JumpRoute route6 = new JumpRoute(GetTestTable2());
            route6.Complete = true;
            Assert.AreEqual(false, GetTestTable2().CanJumpeToNext(route6, new Cell(2, 1)));
        }
    }  
}
