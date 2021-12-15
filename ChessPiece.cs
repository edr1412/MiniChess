using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniChess
{
    abstract class ChessPiece
    {
        protected bool Color { get; set; }
        protected Point Location { get; set; }
        protected List<Point> possibleMoves = new List<Point>();
        protected Image Image { get; set; }

        public bool getColor()
        {
            return Color;
        }

        public Point getLocation()
        {
            return Location;
        }

        public void setLocation(Point newLocation)
        {
            Location = newLocation;
        }

        public Image getImage()
        {
            return Image;
        }

        public virtual List<Point> CalculateMoves(List<ChessPiece> chessPieces)
        {
            return null;
        }

        public virtual void setHasMoved(bool newSetting)
        {
            return;
        }

        protected ChessPiece findChessPiece(Point location, List<ChessPiece> chessPieces)
        {
            foreach (ChessPiece chessPiece in chessPieces)
            {
                if (chessPiece.getLocation() == location)
                    return chessPiece;
            }
            return null;
        }

        protected void canMoveHorizontally(List<ChessPiece> chessPieces)
        {

            Point temp = new Point(Location.X,Location.Y);
            ChessPiece tempChessPiece;
            temp.X++;

            while (temp.X < 8 && findChessPiece(temp, chessPieces) == null)
            {
                possibleMoves.Add(temp);
                temp.X++;
            }
            tempChessPiece = findChessPiece(temp, chessPieces);
            if (tempChessPiece != null && tempChessPiece.getColor() != this.getColor())
            {
                possibleMoves.Add(temp);
            }


            temp = Location;
            temp.Y++;

            while (temp.Y < 8 && findChessPiece(temp, chessPieces) == null)
            {
                possibleMoves.Add(temp);
                temp.Y++;
            }
            tempChessPiece = findChessPiece(temp, chessPieces);
            if (tempChessPiece != null && tempChessPiece.getColor() != this.getColor())
            {
                possibleMoves.Add(temp);
            }


            temp = Location;
            temp.X--;

            while (temp.X >= 0 && findChessPiece(temp, chessPieces) == null)
            {
                possibleMoves.Add(temp);
                temp.X--;
            }
            tempChessPiece = findChessPiece(temp, chessPieces);
            if (tempChessPiece != null && tempChessPiece.getColor() != this.getColor())
            {
                possibleMoves.Add(temp);
            }


            temp = Location;
            temp.Y--;

            while (temp.Y >= 0 && findChessPiece(temp, chessPieces) == null)
            {
                possibleMoves.Add(temp);
                temp.Y--;
            }
            tempChessPiece = findChessPiece(temp, chessPieces);
            if (tempChessPiece != null && tempChessPiece.getColor() != this.getColor())
            {
                possibleMoves.Add(temp);
            }
        }

        protected void canMoveDiagonally(List<ChessPiece> chessPieces)
        {

            Point temp = new Point(Location.X, Location.Y);
            ChessPiece tempChessPiece;


            temp.X++;
            temp.Y++;
            while (temp.X < 8 && temp.Y < 8 && findChessPiece(temp, chessPieces) == null)
            {
                possibleMoves.Add(temp);
                temp.X++;
                temp.Y++;
            }
            tempChessPiece = findChessPiece(temp, chessPieces);
            if (tempChessPiece != null && tempChessPiece.getColor() != this.getColor())
            {
                possibleMoves.Add(temp);
            }


            temp = Location;
            temp.X--;
            temp.Y++;
            
            while (temp.X >= 0 && temp.Y < 8 && findChessPiece(temp, chessPieces) == null)
            {
                possibleMoves.Add(temp);
                temp.X--;
                temp.Y++;
            }
            tempChessPiece = findChessPiece(temp, chessPieces);
            if (tempChessPiece != null && tempChessPiece.getColor() != this.getColor())
            {
                possibleMoves.Add(temp);
            }


            temp = Location;
            temp.X++;
            temp.Y--;

            while (temp.X < 8 && temp.Y >= 0 && findChessPiece(temp, chessPieces) == null)
            {
                possibleMoves.Add(temp);
                temp.X++;
                temp.Y--;
            }
            tempChessPiece = findChessPiece(temp, chessPieces);
            if (tempChessPiece != null && tempChessPiece.getColor() != this.getColor())
            {
                possibleMoves.Add(temp);
            }


            temp = Location;
            temp.X--;
            temp.Y--;

            while (temp.X >= 0 && temp.Y >= 0 && findChessPiece(temp, chessPieces) == null)
            {
                possibleMoves.Add(temp);
                temp.X--;
                temp.Y--;
            }
            tempChessPiece = findChessPiece(temp, chessPieces);
            if (tempChessPiece != null && tempChessPiece.getColor() != this.getColor())
            {
                possibleMoves.Add(temp);
            }
        }
    }
}
