using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniChess
{
    class King : ChessPiece, ICloneable
    {
        public King(Point location, bool color):base(location,color)
        {
            if (color == false)
            { Image = Properties.Resources.BlackKing; }
            if (color == true)
            { Image = Properties.Resources.WhiteKing; }

        }

        public new object Clone()
        {
            King clone = (King)base.Clone();
            return clone;
        }

        protected override List<Point> CalculateMoves(List<ChessPiece> chessPieces)
        {
            possibleMoves.Clear();
            Point temp = new Point(Location.X, Location.Y);
            ChessPiece tempChessPiece;

            temp = Location;
            temp.X++;
            if (temp.X < 8)
            {
                tempChessPiece = findChessPiece(temp, chessPieces);

                if (tempChessPiece == null)
                {
                    possibleMoves.Add(temp);
                }

                else if (tempChessPiece.getColor() != this.getColor())
                {
                    possibleMoves.Add(temp);
                }
            }

            temp = Location;
            temp.X--;
            if (temp.X >= 0)
            {
                tempChessPiece = findChessPiece(temp, chessPieces);
                if (tempChessPiece == null)
                {
                    possibleMoves.Add(temp);
                }
                else if (tempChessPiece.getColor() != this.getColor())
                {
                    possibleMoves.Add(temp);
                }
            }

            temp = Location;
            temp.Y++;
            

            if (temp.Y < 8)
            {
                tempChessPiece = findChessPiece(temp, chessPieces);

                if (tempChessPiece == null)
                {
                    possibleMoves.Add(temp);
                }
                else if (tempChessPiece.getColor() != this.getColor())
                {
                    possibleMoves.Add(temp);
                }
            }

            temp = Location;
            temp.Y--;
            
            if (temp.Y >= 0)
            {
                tempChessPiece = findChessPiece(temp, chessPieces);

                if (tempChessPiece == null)
                {
                    possibleMoves.Add(temp);
                }
                else if (tempChessPiece.getColor() != this.getColor())
                {
                    possibleMoves.Add(temp);
                }
            }

            temp = Location;
            temp.X++;
            temp.Y++;
            if (temp.X < 8 && temp.Y < 8)
            {

                tempChessPiece = findChessPiece(temp, chessPieces);
                if (tempChessPiece == null)
                {
                    possibleMoves.Add(temp);
                }
                else if (tempChessPiece.getColor() != this.getColor())
                {
                    possibleMoves.Add(temp);
                }
            }

            temp = Location;
            temp.X++;
            temp.Y--;

            if (temp.X < 8 && temp.Y >= 0)
            {

                tempChessPiece = findChessPiece(temp, chessPieces);
                if (tempChessPiece == null)
                {
                    possibleMoves.Add(temp);
                }
                else if (tempChessPiece.getColor() != this.getColor())
                {
                    possibleMoves.Add(temp);
                }
            }

            temp = Location;
            temp.X--;
            temp.Y++;
            if (temp.X >= 0 && temp.Y < 8)
            {

                tempChessPiece = findChessPiece(temp, chessPieces);
                if (tempChessPiece == null)
                {
                    possibleMoves.Add(temp);
                }
                else if (tempChessPiece.getColor() != this.getColor())
                {
                    possibleMoves.Add(temp);
                }
            }

            temp = Location;
            temp.X--;
            temp.Y--;
            if (temp.X >= 0 && temp.Y >= 0)
            {

                tempChessPiece = findChessPiece(temp, chessPieces);
                if (tempChessPiece == null)
                {
                    possibleMoves.Add(temp);
                }
                else if (tempChessPiece.getColor() != this.getColor())
                {
                    possibleMoves.Add(temp);
                }
            }


            return possibleMoves;
        }
        protected override List<Point> addExtraMoves(List<ChessPiece> chessPieces)
        {
            Point temp = new Point(Location.X, Location.Y);
            ChessPiece tempChessPiece;

            //检查是否满足王车易位
            if (!hasMoved)
            {
                int x = Color ? 7 : 0;
                //短易位
                tempChessPiece = findChessPiece(new Point(x, 0), chessPieces);
                if (tempChessPiece is Rook && !tempChessPiece.getHasMoved())
                {
                    Console.WriteLine("passed 1");
                    if (findChessPiece(new Point(x, 2), chessPieces) == null && findChessPiece(new Point(x, 1), chessPieces) == null)
                    {
                        Console.WriteLine("passed 2");
                        if (!isChecked(chessPieces, Location, Color) && !isChecked(chessPieces, new Point(x, 2), Color) && !isChecked(chessPieces, new Point(x, 1), Color))
                        {
                            Console.WriteLine("passed 3");
                            possibleMoves.Add(new Point(x, 1));
                        }
                    }
                }
                //长易位
                tempChessPiece = findChessPiece(new Point(x, 7), chessPieces);
                if (tempChessPiece is Rook && !tempChessPiece.getHasMoved())
                {
                    if (findChessPiece(new Point(x, 6), chessPieces) == null && findChessPiece(new Point(x, 5), chessPieces) == null && findChessPiece(new Point(x, 4), chessPieces) == null)
                    {
                        if (!isChecked(chessPieces, Location, Color) && !isChecked(chessPieces, new Point(x, 4), Color) && !isChecked(chessPieces, new Point(x, 3), Color))
                        {
                            possibleMoves.Add(new Point(x, 5));
                        }
                    }
                }
            }
            return possibleMoves;
        }
    }
}
