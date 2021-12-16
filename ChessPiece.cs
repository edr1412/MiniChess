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
        //白色为True
        protected bool Color { get; set; }
        //Point Location 用于储存棋子的逻辑位置 如(3,4)
        protected Point Location { get; set; }
        protected List<Point> possibleMoves = new List<Point>();
        protected Image Image { get; set; }
        public ChessPiece(Point location, bool color)
        {
            this.Color = color;
            this.Location = location;
        }
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

        //得到可落子的格子集。需要传入目前棋子集。
        public virtual List<Point> CalculateMoves(List<ChessPiece> chessPieces)
        {
            return null;
        }

        public virtual void setHasMoved(bool newSetting)
        {
            return;
        }

        //工具，得到指定位置的棋子
        protected ChessPiece findChessPiece(Point location, List<ChessPiece> chessPieces)
        {
            foreach (ChessPiece chessPiece in chessPieces)
            {
                if (chessPiece.getLocation() == location)
                    return chessPiece;
            }
            return null;
        }

        //调用此函数即可向possibleMoves中加入十字经过的格子
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

        //调用此函数即可向possibleMoves中加入X字经过的格子
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
