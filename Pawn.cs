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

            //白子
            if (Color == true)
            {
                //初始可以走2步或者1步
                if (hasMoved == false)
                {
                    if (Location.X - 1 >= 0 && this.findChessPiece(new Point(Location.X - 1, Location.Y), chessPieces) == null)
                    {
                        possibleMoves.Add(new Point(Location.X - 1, Location.Y));
                        if (Location.X - 2 >= 0 && this.findChessPiece(new Point(Location.X - 2, Location.Y), chessPieces) == null)
                        {
                            possibleMoves.Add(new Point(Location.X - 2, Location.Y));
                        }
                    }
                }
                //白子移动过，可以走1步
                if (hasMoved == true)
                {
                    if (Location.X - 1 >= 0 && this.findChessPiece(new Point(Location.X - 1, Location.Y), chessPieces) == null)
                    {
                        possibleMoves.Add(new Point(Location.X - 1, Location.Y));
                    }
                }
                //白子，寻找有无可吃的子

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
            //黑子，同理
            else
            {
                if (hasMoved == false)
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

                if (hasMoved == true)
                {
                    if (Location.X + 1 < 8 && this.findChessPiece(new Point(Location.X + 1, Location.Y), chessPieces) == null)
                    {
                        possibleMoves.Add(new Point(Location.X + 1, Location.Y));
                    }
                }


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
