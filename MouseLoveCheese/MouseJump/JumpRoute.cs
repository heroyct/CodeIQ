using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseLoveCheese.MouseJump
{
    /// <summary>
    /// 飛ぶルート
    /// </summary>
    public class JumpRoute
    {
        private readonly List<Cell> routeCells = new List<Cell>();

        public Table Table { get; private set; }

        public JumpRoute(Table table)
        {
            this.Table = table;
        }

        public JumpRoute(Table table, Cell cell)
        {
            this.Table = table;
            this.AddCell(cell);
        }

        public IEnumerable<Cell> GetCellsEnumerator
        {
            get { return routeCells; }
        }

        public int CellsCount
        {
            get { return routeCells.Count; }
        }

        public int Steps
        {
            get { return routeCells.Count - 1; }
        }

        /// <summary>
        /// すべてのチーズが食べられるルートかどうか
        /// </summary>
        public bool IsEatAllCheese
        {
            get { return Table.CanEatAllCheese(this); }
        }

        public Cell LastCell { get; private set; }
        /// <summary>
        /// 次の場所を探す必要あるかどうかを示す
        /// </summary>
        public bool Complete { get; set; }

        public void AddCellList(IEnumerable<Cell> cells)
        {
            foreach (Cell cell in cells)
            {
                AddCell(cell);
            }
        }

        public bool AddCell(Cell cell)
        {
            if (this.Complete) return false;
            if (!CanAddCell(cell)) return false;

            this.routeCells.Add(cell);
            LastCell = cell;
            return true;
        }

        public bool ContainCell(Cell cell)
        {
            return routeCells.Any(a => a.Compare(cell));
        }

        private bool CanAddCell(Cell cell)
        {
            return Table.CanJumpeToNext(this, cell);
        }

        public override string ToString()
        {
            StringBuilder msg = new StringBuilder();
            foreach (var cell in routeCells)
            {
                msg.Append(string.Format("{0},{1} ", cell.X, cell.Y));
            }
            return msg.ToString().Trim();
        }
    }
}
