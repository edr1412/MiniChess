using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniChess
{
    class Pawn : ChessPiece,ICloneable
    {


        public Pawn(Point location, bool color) : base(location, color)
        {
            if (color == false)
            { Image = Properties.Resources.BlackPawn; }
            if (color == true)
            { Image = Properties.Resources.WhitePawn; }

        }

        public new object Clone()
        {
            Pawn clone = (Pawn)base.Clone();
            return clone;
        }

        protected override List<Point> CalculateMoves(List<ChessPiece> chessPieces)
        {
            possibleMoves.Clear();

            Console.WriteLine(hasMoved + " " + Color);

            //黑子
            if (Color == false)
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
                //移动过，可以走1步
                if (hasMoved == true)
                {
                    if (Location.X - 1 >= 0 && this.findChessPiece(new Point(Location.X - 1, Location.Y), chessPieces) == null)
                    {
                        possibleMoves.Add(new Point(Location.X - 1, Location.Y));
                    }
                }
                //寻找有无可吃的子

                ChessPiece possibleTarget = this.findChessPiece(new Point(Location.X - 1, Location.Y + 1), chessPieces);
                if (Location.X - 1 >= 0 && Location.Y + 1 < 8 && possibleTarget != null && possibleTarget.getColor() != Color)
                {
                    possibleMoves.Add(new Point(Location.X - 1, Location.Y + 1));
                }
                possibleTarget = this.findChessPiece(new Point(Location.X - 1, Location.Y - 1), chessPieces);
                if (Location.X - 1 >= 0 && Location.Y - 1 >= 0 && possibleTarget != null && possibleTarget.getColor() != Color)
                {
                    possibleMoves.Add(new Point(Location.X - 1, Location.Y - 1));
                }


            }
            //白子，同理
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
                if (Location.X + 1 < 8 && Location.Y + 1 < 8 && possibleTarget != null && possibleTarget.getColor() != Color)
                {
                    possibleMoves.Add(new Point(Location.X + 1, Location.Y + 1));
                }
                possibleTarget = this.findChessPiece(new Point(Location.X + 1, Location.Y - 1), chessPieces);
                if (Location.X + 1 < 8 && Location.Y - 1 >= 0 && possibleTarget != null && possibleTarget.getColor() != Color)
                {
                    possibleMoves.Add(new Point(Location.X + 1, Location.Y - 1));
                }

            }

            return possibleMoves;
        }

        protected override List<Point> addExtraMoves(List<ChessPiece> chessPieces)
        {
            //吃过路兵

            ChessPiece tempChessPiece = null;

            if(Color && Location.X == 4)
            {
                if(Location.Y+1<8)
                {
                    tempChessPiece = findChessPiece(new Point(4, Location.Y + 1), chessPieces);
                    if(tempChessPiece != null && tempChessPiece.getColor() != Color && tempChessPiece is Pawn && tempChessPiece.getLocationLast().X == 6)
                    {
                        possibleMoves.Add(new Point(5, Location.Y + 1));
                    }
                }
                if (Location.Y - 1 >= 0)
                {
                    tempChessPiece = findChessPiece(new Point(4, Location.Y - 1), chessPieces);
                    if (tempChessPiece != null && tempChessPiece.getColor() != Color && tempChessPiece is Pawn && tempChessPiece.getLocationLast().X == 6)
                    {
                        possibleMoves.Add(new Point(5, Location.Y - 1));
                    }
                }
            }
            else if (!Color && Location.X == 3)
            {
                if (Location.Y + 1 < 8)
                {
                    tempChessPiece = findChessPiece(new Point(3, Location.Y + 1), chessPieces);
                    if (tempChessPiece != null && tempChessPiece.getColor() != Color && tempChessPiece is Pawn && tempChessPiece.getLocationLast().X == 1)
                    {
                        possibleMoves.Add(new Point(2, Location.Y + 1));
                    }
                }
                if (Location.Y - 1 >= 0)
                {
                    tempChessPiece = findChessPiece(new Point(3, Location.Y - 1), chessPieces);
                    if (tempChessPiece != null && tempChessPiece.getColor() != Color && tempChessPiece is Pawn && tempChessPiece.getLocationLast().X == 1)
                    {
                        possibleMoves.Add(new Point(2, Location.Y - 1));
                    }
                }
            }


            return possibleMoves;
        }

    }
}
