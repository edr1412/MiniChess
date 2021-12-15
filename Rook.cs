using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniChess
{
    class Rook : ChessPiece
    {
        public Rook(Point location, bool color)
        {
            this.Color = color;
            if (color == false)
            { Image = Properties.Resources.BlackRook; }
            if (color == true)
            { Image = Properties.Resources.WhiteRook; }

            this.Location = location;
        }

        public override List<Point> CalculateMoves(List<ChessPiece> chessPieces)
        {
            possibleMoves.Clear();
            canMoveHorizontally(chessPieces);
            return possibleMoves;
        }


        }
}
