using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MouseLoveCheese.MouseJump
{
    public class Jumper
    {
        public Table JumpTable { get; private set; }

        public Jumper(Table table)
        {
            this.JumpTable = table;
        }

        /// <summary>
        /// 八方桂馬飛びで可能な場所(cell)一覧を取得
        /// </summary>
        /// <param name="startCell"></param>
        /// <param name="route"></param>
        /// <returns></returns>
        public List<Cell> GetCanJumpCells(Cell startCell, JumpRoute route)
        {
            List<Cell> jumpCells = new List<Cell>();
            // TopLeft
            AddCellToCanJumpList(jumpCells, route, startCell, -1, -2);
            AddCellToCanJumpList(jumpCells, route, startCell, -2, -1);
            // TopRight
            AddCellToCanJumpList(jumpCells, route, startCell, 1, -2);
            AddCellToCanJumpList(jumpCells, route, startCell, 2, -1);
            // BottomLeft
            AddCellToCanJumpList(jumpCells, route, startCell, -1, 2);
            AddCellToCanJumpList(jumpCells, route, startCell, -2, 1);
            // BottomRight
            AddCellToCanJumpList(jumpCells, route, startCell, 1, 2);
            AddCellToCanJumpList(jumpCells, route, startCell, 2, 1);
            return jumpCells;
        }

        private void AddCellToCanJumpList(List<Cell> jumpCells, JumpRoute route, Cell startCell, int movementX, int movementY)
        {
            var cellToMove = GetMoveToCell(startCell, movementX, movementY);
            if (CanJump(route, cellToMove)
                )
            {
                jumpCells.Add(cellToMove);
            }
        }

        private static Cell GetMoveToCell(Cell startCell, int movementX, int movementY)
        {
            return new Cell(startCell.X + movementX, startCell.Y + movementY);
        }

        private bool CanJump(JumpRoute route, Cell cellToMove)
        {
            return JumpTable.Exists(cellToMove)
                   && JumpTable.CanJumpeToNext(route, cellToMove);
        }
    }
}
