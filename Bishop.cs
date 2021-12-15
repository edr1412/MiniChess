using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniChess
{
    class Bishop : ChessPiece
    {
        public Bishop(Point location, bool color)
        {
            this.Color = color;
            if (color == false)
            { Image = Properties.Resources.BlackBishop; }
            if (color == true)
            { Image = Properties.Resources.WhiteBishop; }

            this.Location = location;
        }

        public override List<Point> CalculateMoves(List<ChessPiece> chessPieces)
        {
            possibleMoves.Clear();
            canMoveDiagonally(chessPieces);
            return possibleMoves;
        }

    }
}
