﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniChess
{
    class King : ChessPiece
    {
        public King(Point location, bool color):base(location,color)
        {
            if (color == false)
            { Image = Properties.Resources.BlackKing; }
            if (color == true)
            { Image = Properties.Resources.WhiteKing; }

        }


        public override List<Point> CalculateMoves(List<ChessPiece> chessPieces)
        {
            possibleMoves.Clear();
            Point temp = new Point(Location.X, Location.Y);
            ChessPiece tempChessPiece;

            
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
    }
}
