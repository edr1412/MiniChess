using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniChess
{
    abstract class ChessPiece:ICloneable
    {
        //白色为True
        protected bool Color { get; set; }
        //Point Location 用于储存棋子的逻辑位置 如(3,4)
        protected Point Location { get; set; }
        protected Point LocationLast { get; set; }
        protected List<Point> possibleMoves = new List<Point>();
        protected Image Image { get; set; }
        protected bool hasMoved;
        public ChessPiece(Point location, bool color)
        {
            this.Color = color;
            this.Location = location;
            this.LocationLast = new Point(-1,-1);
            this.hasMoved = false;
        }

        public object Clone()
        {
            ChessPiece cp = (ChessPiece)this.MemberwiseClone();
            cp.Color = this.Color;
            cp.Location = this.Location;
            cp.possibleMoves = this.possibleMoves;
            cp.Image = this.Image;
            cp.hasMoved = this.hasMoved;
            cp.LocationLast = this.LocationLast;
            return cp;

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

        public Point getLocationLast()
        {
            return LocationLast;
        }

        public void setLocationLast(Point newLocation)
        {
            LocationLast = newLocation;
        }

        public Image getImage()
        {
            return Image;
        }

        public bool getHasMoved()
        {
            return hasMoved ;
        }

        public void setHasMoved(bool newSetting)
        {
            hasMoved = newSetting;
        }

        //得到可落子的格子集。需要传入目前棋子集。
        protected virtual List<Point> CalculateMoves(List<ChessPiece> chessPieces)
        {
            return null;
        }
        protected virtual List<Point> addExtraMoves(List<ChessPiece> chessPieces)
        {
            return null;
        }
        public List<Point> CalculateMovesWithKingConsidered(List<ChessPiece> chessPieces) {
            CalculateMoves(chessPieces);
            addExtraMoves(chessPieces);
            removeImposssibleMoves(chessPieces);
            return possibleMoves;
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

        public bool isChecked(List<ChessPiece> chessPieces, Point kingPoint, bool isWhite)
        {
            foreach (ChessPiece chessPiece in chessPieces)
            {
                if (chessPiece.Color != isWhite && chessPiece.CalculateMoves(chessPieces).Contains(kingPoint))
                {
                    return true;
                }
            }
            return false;
        }

        protected void removeImposssibleMoves(List<ChessPiece> chessPieces)
        {
            ChessPiece anotherMe = null;

            //List<ChessPiece> chessPiecesTemp = new List<ChessPiece>(chessPieces);  //坑
            List<ChessPiece> chessPiecesTemp = new List<ChessPiece>();
            foreach (ChessPiece chessPiece in chessPieces)
            {
                ChessPiece temp = (ChessPiece)chessPiece.Clone();
                if (chessPiece == this)
                {
                    anotherMe = temp;
                }

                chessPiecesTemp.Add(temp);
            }
            
            Point kingPoint = Location;

            List<Point> possibleMovesTemp = new List<Point>();
            possibleMoves.ForEach(i => possibleMovesTemp.Add(i));
            List<ChessPiece> chessPiecesTemporaryRemoved = new List<ChessPiece>();
            foreach (Point p in possibleMoves)
            {
                //Console.WriteLine("===========me\n{0},{1}", p.X, p.Y);
                anotherMe.setLocation(p);
                foreach (ChessPiece chessPiece in chessPiecesTemp)
                {
                    if (chessPiece.GetType().Name == "King" && chessPiece.getColor() == this.Color)
                    {
                        kingPoint = chessPiece.getLocation();
                    }
                    if(chessPiece.getLocation() == p && chessPiece.getColor()!=this.Color)
                    {
                        chessPiecesTemporaryRemoved.Add(chessPiece);

                    }
                }
                foreach(ChessPiece chessPiece in chessPiecesTemporaryRemoved)
                {
                    chessPiecesTemp.Remove(chessPiece);
                }

                if (isChecked(chessPiecesTemp, kingPoint, this.Color))
                {
                    possibleMovesTemp.Remove(p);
                }
                foreach (ChessPiece chessPiece in chessPiecesTemporaryRemoved)
                {
                    chessPiecesTemp.Add(chessPiece);
                }
                chessPiecesTemporaryRemoved.Clear();
            }
            possibleMoves = possibleMovesTemp;

            //Console.WriteLine("===========me\n");
            //foreach (ChessPiece chessPiece in chessPieces)
            //{
            //    Console.WriteLine("{0} {1}:{2},{3}", chessPiece.getColor() ? "white" : "black", chessPiece.GetType().Name, chessPiece.getLocation().X, chessPiece.getLocation().Y);
            //}
        }
    }
}
