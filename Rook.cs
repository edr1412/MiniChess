using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniChess
{
    class Rook : ChessPiece,ICloneable
    {
        public Rook(Point location, bool color) : base(location, color)
        {
            if (color == false)
            { Image = Properties.Resources.BlackRook; }
            if (color == true)
            { Image = Properties.Resources.WhiteRook; }

        }

        public new object Clone()
        {
            Rook clone = (Rook)base.Clone();
            return clone;
        }


        protected override List<Point> CalculateMoves(List<ChessPiece> chessPieces)
        {
            possibleMoves.Clear();
            canMoveHorizontally(chessPieces);
            return possibleMoves;
        }


        }
}
