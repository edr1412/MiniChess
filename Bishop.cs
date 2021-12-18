using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniChess
{
    class Bishop : ChessPiece,ICloneable
    {
        public Bishop(Point location, bool color) : base(location, color)
        {
            if (color == false)
            { Image = Properties.Resources.BlackBishop; }
            if (color == true)
            { Image = Properties.Resources.WhiteBishop; }

        }

        public new object Clone()
        {
            Bishop clone = (Bishop)base.Clone();
            return clone;
        }
        protected override List<Point> CalculateMoves(List<ChessPiece> chessPieces)
        {
            possibleMoves.Clear();
            canMoveDiagonally(chessPieces);
            return possibleMoves;
        }

    }
}
