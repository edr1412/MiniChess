using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniChess
{
    class Pawn : ChessPiece
    {

        private bool hasMoved;

        public Pawn(Point location, bool color) : base(location, color)
        {
            if (color == false)
            { Image = Properties.Resources.BlackPawn; }
            if (color == true)
            { Image = Properties.Resources.WhitePawn; }

            hasMoved = false;
        }

        public override List<Point> CalculateMoves(List<ChessPiece> chessPieces)
        {
            possibleMoves.Clear();
           
            
            Console.WriteLine(hasMoved + " " + Color);

            if (hasMoved == false && Color == true)
            {
                if (Location.X - 1 >= 0 && this.findChessPiece(new Point(Location.X - 1, Location.Y), chessPieces) == null)
                {
                    possibleMoves.Add(new Point(Location.X- 1, Location.Y));
                    if (Location.X - 2 >= 0 && this.findChessPiece(new Point(Location.X - 2, Location.Y), chessPieces) == null)
                    {
                        possibleMoves.Add(new Point(Location.X - 2, Location.Y));
                    }
                }
             }
            if (hasMoved == true && Color == true)
            {
                if (Location.X - 1 >= 0 && this.findChessPiece(new Point(Location.X - 1, Location.Y), chessPieces) == null)
                {
                    possibleMoves.Add(new Point(Location.X - 1, Location.Y));
                }
            }
            //check if can take enemy
            if (Color == true)
            {
                ChessPiece possibleTarget = this.findChessPiece(new Point(Location.X - 1, Location.Y + 1), chessPieces);
                if (Location.X - 1 >= 0 && Location.Y + 1 < 8 && possibleTarget != null && possibleTarget.getColor() == false)
                {
                    possibleMoves.Add(new Point(Location.X - 1, Location.Y + 1));
                }
                possibleTarget = this.findChessPiece(new Point(Location.X - 1, Location.Y - 1), chessPieces);
                if (Location.X - 1 >= 0 && Location.Y - 1 >= 0 && possibleTarget != null && possibleTarget.getColor() == false)
                {
                    possibleMoves.Add(new Point(Location.X - 1, Location.Y - 1));
                }
            }



            if (hasMoved == false && Color == false)
            {
                if (Location.X + 1 < 8 && this.findChessPiece(new Point(Location.X + 1, Location.Y), chessPieces) == null)
                {
                    possibleMoves.Add(new Point(Location.X + 1, Location.Y));
                    if (Location.X + 2 < 8 && this.findChessPiece(new Point(Location.X + 2, Location.Y), chessPieces) == null)
                    {
                        possibleMoves.Add(new Point(Location.X + 2, Location.Y));
                    }
                }
            }

            if (hasMoved == true && Color == false)
            {
                if (Location.X + 1 < 8 && this.findChessPiece(new Point(Location.X + 1, Location.Y), chessPieces) == null)
                {
                    possibleMoves.Add(new Point(Location.X + 1, Location.Y));
                }
            }
            //check if can take enemy
            if (Color == false)
            {
                ChessPiece possibleTarget = this.findChessPiece(new Point(Location.X + 1, Location.Y + 1), chessPieces);
                if (Location.X + 1 < 8 && Location.Y + 1 < 8 && possibleTarget != null && possibleTarget.getColor() == true)
                {
                    possibleMoves.Add(new Point(Location.X + 1, Location.Y + 1));
                }
                possibleTarget = this.findChessPiece(new Point(Location.X + 1, Location.Y - 1), chessPieces);
                if (Location.X + 1 < 8 && Location.Y - 1 >= 0 && possibleTarget != null && possibleTarget.getColor() == true)
                {
                    possibleMoves.Add(new Point(Location.X + 1, Location.Y - 1));
                }
            }

            return possibleMoves;
        }

        public override void setHasMoved(bool newSetting)
        {
            hasMoved = newSetting;
        }


    }
}
