using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseLoveCheese.MouseJump
{
    public class Cell
    {
        #region constructor
        public Cell(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        #endregion

        public int X { get; private set; }
        public int Y { get; private set; }

        /// <summary>
        /// 同じセルかどうかを比較します
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public bool Compare(Cell cell)
        {
            return this.X == cell.X && this.Y == cell.Y;
        }
    }
}
