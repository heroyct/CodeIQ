using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseLoveCheese.MouseJump
{
    public class Table
    {
        private readonly List<Cell> cheeseCells = new List<Cell>();

        public int Cols { get; private set; }

        public int Rows { get; private set; }

        public IEnumerable<Cell> CheeseCells
        {
            get { return cheeseCells; }
        }

        public Table(int cols, int rows)
        {
            this.Cols = cols;
            this.Rows = rows;
        }

        public bool AddCheesse(Cell cell)
        {
            if (!this.Exists(cell)) return false;
            if (this.ContainCheesseCell(cell)) return false;

            this.cheeseCells.Add(cell);
            return true;
        }

        public bool ContainCheesseCell(Cell cheesseCell)
        {
            return this.cheeseCells.Any(a => a.Compare(cheesseCell));
        }

        public bool Exists(Cell cell)
        {
            bool isValidX = cell.X >= 0 && cell.X < Cols;
            bool isValidY = cell.Y >= 0 && cell.Y < Rows;
            return isValidX && isValidY;
        }

        public bool CanEatAllCheese(JumpRoute jump)
        {
            if (cheeseCells.Count == 0) return false;
            bool all = true;
            foreach (Cell cell in cheeseCells)
            {
                if (!jump.GetCellsEnumerator.Any(a => a.Compare(cell)))
                {
                    all = false;
                    break;
                }
            }

            return all;
        }

        public bool CanJumpeToNext(JumpRoute jump, Cell nextCell)
        {
            if (jump.Complete) { return false; }
            // チーズ食べ済みか
            bool eatAllCheese = CanEatAllCheese(jump);
            if (eatAllCheese) { return false; }
            // 既に飛んだ場所
            if (jump.ContainCell(nextCell)) return false;
            return true;
        }
    }
}
