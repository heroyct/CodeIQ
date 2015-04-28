using Microsoft.VisualStudio.TestTools.UnitTesting;
using MouseLoveCheese.MouseJump;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseLoveCheeseTest
{
    public class MouseJumpTestBase
    {
        public TestContext TestContext { get; set; }

        protected JumpRoute GetJumpRoute1()
        {
            JumpRoute route = new JumpRoute(GetTestTable1());
            foreach (Cell cell in route.Table.CheeseCells)
            {
                route.AddCell(cell);
            }
            return route;
        }

        protected Table GetTestTable1()
        {
            Table table = new Table(5, 5);
            table.AddCheesse(new Cell(2, 0));
            table.AddCheesse(new Cell(2, 1));
            table.AddCheesse(new Cell(4, 1));
            table.AddCheesse(new Cell(0, 2));
            table.AddCheesse(new Cell(1, 2));
            table.AddCheesse(new Cell(3, 3));
            table.AddCheesse(new Cell(0, 4));
            table.AddCheesse(new Cell(1, 4));
            return table;
        }

        protected Table GetTestTable2()
        {
            Table table = new Table(2, 2);
            table.AddCheesse(new Cell(0, 0));
            table.AddCheesse(new Cell(1, 1));
            return table;
        }
        protected Table GetTestTable3()
        {
            Table table = new Table(2, 3);
            table.AddCheesse(new Cell(0, 0));
            table.AddCheesse(new Cell(1, 2));
            return table;
        }

        protected Table GetTestTable4()
        {
            Table table = new Table(3, 4);
            table.AddCheesse(new Cell(2, 0));
            table.AddCheesse(new Cell(1, 1));
            table.AddCheesse(new Cell(2, 2));
            table.AddCheesse(new Cell(1, 3));
            return table;
        }
    }
}
