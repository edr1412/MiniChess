using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniChess
{
    class Queen : ChessPiece,ICloneable
    {
        public Queen(Point location, bool color) : base(location, color)
        {
            if (color == false)
            { Image = Properties.Resources.BlackQueen; }
            if (color == true)
            { Image = Properties.Resources.WhiteQueen; }

        }

        public new object Clone()
        {
            Queen clone = (Queen)base.Clone();
            return clone;
        }
        protected override List<Point> CalculateMoves(List<ChessPiece> chessPieces)
        {
            possibleMoves.Clear();
            canMoveHorizontally(chessPieces);
            canMoveDiagonally(chessPieces);
            return possibleMoves;
        }
    }
}
