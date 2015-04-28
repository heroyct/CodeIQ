using MouseLoveCheese.MouseJump;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseLoveCheese
{
    class Program
    {
        static void Main(string[] args)
        {
            ShortestJumpRouteFinder generater1 = new ShortestJumpRouteFinder(GetJumpTable());
            var routes = generater1.FindShortestJumpRoute(new Cell(0, 0));
            foreach(var route in routes){
                Console.WriteLine(route.ToString());
            }  
        }

        private static Table GetJumpTable()
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
    }
}
