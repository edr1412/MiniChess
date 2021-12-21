using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniChess
{
    public partial class ChessBoard : Form
    {
    
        private const int panelSize = 100;//每格边长
        private const int gridSize = 8;//8x8个格
        private Panel[,] chessPanels = new Panel[gridSize, gridSize];//棋格集
        private List<ChessPiece> chessPieces = new List<ChessPiece>();//棋子集
        List<Point> potentialMoves = new List<Point>();//可移动区
        private bool whiteTurn = true;//是否白棋回合
        private int turnCounter = 0;
        ChessPiece selectedPiece;//选中棋子
        Point locationLast = new Point(-1, -1);
        Point locationNow = new Point(-1, -1);
        private int rotation = 0;//0:white left,1:white bottom,2:white right;3:white up
        private Panel modifyingPanel = null;
        private bool allowModify = false;
        private int spaceHitCombo = 0;

        private List<Point> possibleMoves;

        public ChessBoard()
        {
            InitializeComponent();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                spaceHitCombo = (spaceHitCombo + 1) % 5;
                if(spaceHitCombo == 0)
                    allowModify = !allowModify;
            }
        }

        //打开窗口时执行。创建每个格子的panel，并存放于chessPanels
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int row = 0; row < gridSize; row++)
            {
                for (int column = 0; column < gridSize; column++)
                {
                    var newPanel = new Panel
                    {
                        Size = new Size(panelSize, panelSize),
                        Location = new Point(panelSize * column, panelSize * row),
                        BackgroundImageLayout = ImageLayout.Stretch
                    };
                    //点击事件订阅panelClick
                    newPanel.MouseClick += panelClick;
                    //为中间区域订阅boardRotate
                    if (column <= 4 && column >= 3 && row <= 4 && row >= 3)
                    {
                        newPanel.MouseWheel += boardRotate;
                    }

                    this.Controls.Add(newPanel);

                    chessPanels[column, row] = newPanel;
                }
            }

            //foreach(Control c in this.Controls)
            //{
            //    if (c is Panel)
            //    {
            //        c.Click += panelClick;
            //    }
            //}

            setUpBoard();

            redrawBoard();

        }

        //棋盘开局
        void setUpBoard()
        {
            //清空棋子集
            chessPieces.Clear();


            //添加白子

            chessPieces.Add(new Rook(new Point(0, 0), true));
            chessPieces.Add(new Knight(new Point(0, 1), true));
            chessPieces.Add(new Bishop(new Point(0, 2), true));
            chessPieces.Add(new Queen(new Point(0, 3), true));
            chessPieces.Add(new King(new Point(0, 4), true));
            chessPieces.Add(new Bishop(new Point(0, 5), true));
            chessPieces.Add(new Knight(new Point(0, 6), true));
            chessPieces.Add(new Rook(new Point(0, 7), true));
            for (int x = 0; x < 8; x++)
            {
                chessPieces.Add(new Pawn(new Point(1, x), true));
            }

            //添加黒子

            chessPieces.Add(new Rook(new Point(7, 0), false));
            chessPieces.Add(new Knight(new Point(7, 1), false));
            chessPieces.Add(new Bishop(new Point(7, 2), false));
            chessPieces.Add(new Queen(new Point(7, 3), false));
            chessPieces.Add(new King(new Point(7, 4), false));
            chessPieces.Add(new Bishop(new Point(7, 5), false));
            chessPieces.Add(new Knight(new Point(7, 6), false));
            chessPieces.Add(new Rook(new Point(7, 7), false));

            for (int x = 0; x < 8; x++)
            {
                chessPieces.Add(new Pawn(new Point(6, x), false));
            }


        }

        // 刷新界面
        void redrawBoard(bool forceClear=false)
        {
            //绘制棋盘
            for (int row = 0; row < gridSize; row++)
            {
                for (int column = 0; column < gridSize; column++)
                {
                    //清除棋子图像
                    if(forceClear)
                        findPanelByLocation(new Point(column, row)).BackgroundImage = null;
                    //奇数行：白黑白黑白黑白黑
                    if (row % 2 != 0)
                        findPanelByLocation(new Point(column,row)).BackColor = column % 2 != 0 ? Color.LightGray : Color.White;
                    //偶数行：黑白黑白黑白黑白
                    else
                        findPanelByLocation(new Point(column, row)).BackColor = column % 2 != 0 ? Color.White : Color.LightGray;
                }
            }
            //绘制棋子
            foreach (ChessPiece chessPiece in chessPieces)
            {
                findPanelByLocation(chessPiece.getLocation()).BackgroundImage = chessPiece.getImage();
            }
            //绘制移动轨迹
            if(locationLast.X != -1)
                findPanelByLocation(locationLast).BackColor = Color.BlueViolet;
            if (locationNow.X != -1)
                findPanelByLocation(locationNow).BackColor = Color.Aquamarine;
        }




        //工具，得到指定位置的棋子
        ChessPiece findChessPiece(Point location)
        {
            foreach (ChessPiece chessPiece in chessPieces)
            {
                if (chessPiece.getLocation() == location)
                    return chessPiece;
            }
            return null;
        }
        //得到一个位置的panel，考虑rotation
        Panel findPanelByLocation(Point location)
        {
            if (rotation == 0)
            {
            }
            else if (rotation == 1)
            {
                location = new Point(location.Y, 7 - location.X);
            }
            else if (rotation == 2)
            {
                location = new Point(7 - location.X, 7 - location.Y);
            }
            else if (rotation == 3)
            {
                location = new Point(7 - location.Y, location.X);
            }
            return chessPanels[location.X, location.Y];

        }
        //得到一格panel的位置，考虑rotation
        Point findLocationByPanel(Panel panel)
        {
            Point location = new Point();
            for (int row = 0; row < gridSize; row++)
            {
                for (int column = 0; column < gridSize; column++)
                {
                    if (chessPanels[row, column] == panel)
                    {
                        location.X = row;
                        location.Y = column;
                        break;
                    }
                }
            }
            if (rotation == 0)
            {
            }
            else if (rotation == 3)
            {
                location = new Point(location.Y, 7 - location.X);
            }
            else if (rotation == 2)
            {
                location = new Point(7 - location.X, 7 - location.Y);
            }
            else if (rotation == 1)
            {
                location = new Point(7 - location.Y, location.X);
            }
            return location;
        }

        void endGameIfNecessary()
        {
            //清除本回合选手的棋子的locationLast，通过locationLast可以判断一个子是否为上回合移动的子
            foreach (ChessPiece chessPiece in chessPieces)
            {
                if (chessPiece.getColor() == whiteTurn) {
                    chessPiece.setLocationLast(new Point(-1, -1));
                }
            }
            //检查有没有可动棋子，没有则游戏结束进入结果判断
            foreach (ChessPiece chessPiece in chessPieces)
            {
                if (chessPiece.getColor() == whiteTurn)
                {
                    if (chessPiece.CalculateMovesWithKingConsidered(chessPieces).Any())
                    {
                        return;
                    }
                }
            }
            //王可能有多个
            List<ChessPiece> kings = new List<ChessPiece>();
            foreach (ChessPiece chessPiece in chessPieces)
            {
                if (chessPiece.getColor() == whiteTurn && chessPiece is King)
                {
                    kings.Add(chessPiece);
                }
            }
            //只要一个王被将就算输
            foreach(ChessPiece king in kings)
            {
                if (king.isChecked(chessPieces, king.getLocation(), king.getColor()))
                {
                    if (king.getColor() == true)
                        this.BlackWins.Visible = true;
                    else
                        this.WhiteWins.Visible = true;
                    return;
                }
            }
            //没有王被将，就平局
            this.Draw.Visible = true;
        }

        //移动selectedPiece至指定位置。每个turn一次。
        void movePiece(Point panelLocation)
        {
            //标记升变
            bool hasPromoted = false;
            //清除棋子原处的图像
            findPanelByLocation(selectedPiece.getLocation()).BackgroundImage = null;

            selectedPiece.setLocationLast(selectedPiece.getLocation());

            //检查是否吃过路兵
            if (selectedPiece is Pawn)
            {
                if(panelLocation.Y != selectedPiece.getLocation().Y && findChessPiece(panelLocation) == null)
                {
                    findPanelByLocation(new Point(selectedPiece.getLocation().X, panelLocation.Y)).BackgroundImage = null;
                    chessPieces.Remove(findChessPiece(new Point(selectedPiece.getLocation().X,panelLocation.Y)));
                }
            }

            //检查是否吃子
            if (findChessPiece(panelLocation) != null)
            {
                chessPieces.Remove(findChessPiece(panelLocation));
            }
            //检查是否升变 
            if(selectedPiece is Pawn)
            {
                if (!selectedPiece.getColor() && panelLocation.X == 0)
                {
                    Point oldPlace = selectedPiece.getLocation();
                    chessPieces.Remove(selectedPiece);
                    selectedPiece = new Queen(panelLocation, false);
                    selectedPiece.setLocationLast(oldPlace);
                    chessPieces.Add(selectedPiece);
                    findPanelByLocation(panelLocation).MouseEnter += panelEnter;
                    findPanelByLocation(panelLocation).MouseLeave += panelLeave;
                    findPanelByLocation(panelLocation).MouseWheel += panelScroll;
                    hasPromoted = true;
                    //Console.WriteLine("++++++++++++");

                }
                else if (selectedPiece.getColor() && panelLocation.X == 7)
                {
                    Point oldPlace = selectedPiece.getLocation();
                    chessPieces.Remove(selectedPiece);
                    selectedPiece = new Queen(panelLocation, true);
                    selectedPiece.setLocationLast(oldPlace);
                    chessPieces.Add(selectedPiece);
                    findPanelByLocation(panelLocation).MouseEnter += panelEnter;
                    findPanelByLocation(panelLocation).MouseLeave += panelLeave;
                    findPanelByLocation(panelLocation).MouseWheel += panelScroll;
                    hasPromoted = true;
                    //Console.WriteLine("++++++++++++");
                }

            }
            //检查是否王车易位
            else if (selectedPiece is King && !selectedPiece.getHasMoved())
            {
                if (selectedPiece.getLocation().X == (selectedPiece.getColor() ? 0 : 7) && selectedPiece.getLocation().Y == 4)
                {
                    //短易位
                    if (panelLocation.Y == 6)
                    {
                        Point rookLocation = new Point(selectedPiece.getColor() ? 0 : 7, 7);
                        ChessPiece rookCastled = findChessPiece(rookLocation);
                        rookCastled.setLocationLast(rookLocation);
                        findPanelByLocation(rookLocation).BackgroundImage = null;
                        findPanelByLocation(new Point(rookLocation.X, 5)).BackgroundImage = rookCastled.getImage();
                        rookCastled.setLocation(new Point(rookLocation.X, 5));
                        rookCastled.setHasMoved(true);

                    }
                    //长易位
                    else if (panelLocation.Y == 2)
                    {
                        Point rookLocation = new Point(selectedPiece.getColor() ? 0 : 7, 0);
                        ChessPiece rookCastled = findChessPiece(rookLocation);
                        rookCastled.setLocationLast(rookLocation);
                        findPanelByLocation(rookLocation).BackgroundImage = null;
                        findPanelByLocation(new Point(rookLocation.X, 3)).BackgroundImage = rookCastled.getImage();
                        rookCastled.setLocation(new Point(rookLocation.X, 3));
                        rookCastled.setHasMoved(true);

                    }
                }
            }

            //在指定格更换为所选棋子的图像
            findPanelByLocation(panelLocation).BackgroundImage = selectedPiece.getImage();
            //更新棋子位置
            selectedPiece.setLocation(panelLocation);
            //更新寄存的值
            locationLast = selectedPiece.getLocationLast();
            locationNow = selectedPiece.getLocation();

            //设置已移动过
            if(!hasPromoted)
                selectedPiece.setHasMoved(true);
            

            //下一回合
            whiteTurn = !whiteTurn;
            //绘制轨迹需要调用一下
            redrawBoard();

            endGameIfNecessary();

            //Console.WriteLine("===========me\n");
            //foreach (ChessPiece chessPiece in chessPieces)
            //{
            //    Console.WriteLine("{0} {1}:{2},{3}", chessPiece.getColor()?"white":"black",chessPiece.GetType().Name, chessPiece.getLocation().X, chessPiece.getLocation().Y);
            //}


        }
        //进入，高亮
        void panelEnter(object sender, EventArgs e)
        {
            //Console.WriteLine("------------");
            Panel panel = sender as Panel;
            //panel.Focus();
            Point panelLocation = findLocationByPanel(panel);
            findPanelByLocation(panelLocation).BackColor = Color.Goldenrod;
        }
        //离开，取消高亮
        void panelLeave(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;
            Point panelLocation = findLocationByPanel(panel);
            findPanelByLocation(panelLocation).BackColor = Color.Aquamarine;
        }
        //滚轮触发的方法，选任意子
        void panelSelectFromAllPieces(object sender, MouseEventArgs e)
        {
            Panel panel = sender as Panel;
            panel.BackColor = Color.DarkGoldenrod;
            Point panelLocation = findLocationByPanel(panel);
            ChessPiece tempChess = findChessPiece(panelLocation);
            ChessPiece[] chessSelectable = new ChessPiece[] { new Pawn(panelLocation,whiteTurn) , new Rook(panelLocation, whiteTurn),
                new Knight(panelLocation,whiteTurn) ,new Bishop(panelLocation,whiteTurn) ,new Queen(panelLocation,whiteTurn) ,
                new King(panelLocation,whiteTurn), new Pawn(panelLocation,!whiteTurn) , new Rook(panelLocation, !whiteTurn),
                new Knight(panelLocation,!whiteTurn) ,new Bishop(panelLocation,!whiteTurn) ,new Queen(panelLocation,!whiteTurn),
                new King(panelLocation,!whiteTurn) };
            chessPieces.Remove(tempChess);
            Type tempChessType = tempChess.GetType();
            bool tempChessColor = tempChess.getColor();
            foreach (ChessPiece cp in chessSelectable)
            {
                if (tempChessType == cp.GetType() && tempChessColor == cp.getColor())
                {
                    //Console.WriteLine("+++++++");
                    if (e.Delta < 0)
                        tempChess = chessSelectable[(Array.IndexOf(chessSelectable, cp) + 1) % 12];
                    else
                        tempChess = chessSelectable[(Array.IndexOf(chessSelectable, cp) + 11) % 12];
                    //tempChess.setHasMoved(true);
                    if(tempChess.GetType() == typeof(Pawn))
                        tempChess.setHasMoved(true);
                    break;
                }
            }

            chessPieces.Add(tempChess);

            panel.BackgroundImage = tempChess.getImage();
        }
        //滚轮触发的方法，选子
        void panelScroll(object sender, MouseEventArgs e)
        {
            Panel panel = sender as Panel;
            panel.BackColor = Color.Goldenrod;
            Point panelLocation = findLocationByPanel(panel);
            ChessPiece tempChess = findChessPiece(panelLocation);
            Point oldPlace = tempChess.getLocationLast();
            chessPieces.Remove(tempChess);
            if (tempChess is Queen)
            {
                if (e.Delta < 0)
                    tempChess = new Knight(panelLocation, !whiteTurn);
                else
                    tempChess = new Bishop(panelLocation, !whiteTurn);
                tempChess.setLocationLast(oldPlace);
            }
            else if(tempChess is Knight)
            {
                if (e.Delta < 0)
                    tempChess = new Rook(panelLocation, !whiteTurn);
                else
                    tempChess = new Queen(panelLocation, !whiteTurn);
                tempChess.setLocationLast(oldPlace);
            }
            else if (tempChess is Rook)
            {
                if (e.Delta < 0)
                    tempChess = new Bishop(panelLocation, !whiteTurn);
                else
                    tempChess = new Knight(panelLocation, !whiteTurn);
                tempChess.setLocationLast(oldPlace);
            }
            else
            {
                if (e.Delta < 0)
                    tempChess = new Queen(panelLocation, !whiteTurn);
                else
                    tempChess = new Rook(panelLocation, !whiteTurn);
                tempChess.setLocationLast(oldPlace);
            }
            chessPieces.Add(tempChess);
            panel.BackgroundImage = tempChess.getImage();

        }
        //滚轮触发的方法，旋转棋盘
        void boardRotate(object sender, MouseEventArgs e)
        {
            //关于升变的订阅需要处理一下
            //如果既动又没动，那就是刚刚升变了
            //如果刚刚升变了，先清除有关升变的订阅
            if (locationNow.X >= 0 && !findChessPiece(locationNow).getHasMoved())
            {
                //Console.WriteLine("==================={0},{1}", locationNow.X, locationNow.Y);
                Panel panelToClean = findPanelByLocation(locationNow);
                panelToClean.MouseEnter -= panelEnter;
                panelToClean.MouseWheel -= panelScroll;
                panelToClean.MouseLeave -= panelLeave;
            }
            //清除棋子任选
            if (modifyingPanel != null)
            {
                modifyingPanel.MouseWheel -= panelSelectFromAllPieces;
                Point modifyingPanelLocation = findLocationByPanel(modifyingPanel);
                if (modifyingPanelLocation.X <= 4 && modifyingPanelLocation.X >= 3 && modifyingPanelLocation.Y <= 4 && modifyingPanelLocation.Y >= 3)
                {
                    modifyingPanel.MouseWheel += boardRotate;
                }
                modifyingPanel = null;
            }
            //更改rotation参数
            if (e.Delta < 0)
                rotation = (rotation + 1) % 4;
            else
                rotation = (rotation + 3) % 4;
            //刷新棋盘
            redrawBoard(true);
            //如果刚刚升变了，再添加有关升变的订阅到正确panel
            if (locationNow.X >= 0 && !findChessPiece(locationNow).getHasMoved())
            {
                Panel panelToClean = findPanelByLocation(locationNow);
                panelToClean.MouseEnter += panelEnter;
                panelToClean.MouseWheel += panelScroll;
                panelToClean.MouseLeave += panelLeave;
            }
        }



        //点击触发的方法，移动到所选可移动格，或者显示所选棋子的可移动区
        void panelClick(object sender, MouseEventArgs e)
        {
            //清除空格连击的计数
            spaceHitCombo = 0;
            //清除有关升变的订阅。如果既动又没动，那就是刚刚升变了。
            if (locationNow.X>=0 && !findChessPiece(locationNow).getHasMoved())
            {
                //Console.WriteLine("-=-=-=-=-=-=-=-=-=-{0},{1}", locationNow.X,locationNow.Y);
                Panel panelToClean = findPanelByLocation(locationNow);
                panelToClean.MouseEnter -= panelEnter;
                panelToClean.MouseWheel -= panelScroll;
                panelToClean.MouseLeave -= panelLeave;
            }
            //清除棋子任选
            if(modifyingPanel != null)
            {
                modifyingPanel.MouseWheel -= panelSelectFromAllPieces;
                Point modifyingPanelLocation = findLocationByPanel(modifyingPanel);
                if (modifyingPanelLocation.X <= 4 && modifyingPanelLocation.X >= 3 && modifyingPanelLocation.Y <= 4 && modifyingPanelLocation.Y >= 3)
                {
                    modifyingPanel.MouseWheel += boardRotate;
                }
                modifyingPanel = null;
            }
            

            //清除先前的高亮
            redrawBoard();

            //非左键点击：仅刷新potentialMoves和selectedPiece（只要中键没起效）
            if (e.Button != MouseButtons.Left && !(allowModify && e.Button == MouseButtons.Middle))
            {
                selectedPiece = null;
                if (potentialMoves != null)
                    potentialMoves.Clear();
                return;
            }

            //获取所点的panel信息
            Panel panel = sender as Panel;
            Point panelLocation = findLocationByPanel(panel);

            //中键点击：自由清除棋子或者新建棋子
            if (allowModify && e.Button == MouseButtons.Middle)
            {
                this.BlackWins.Visible = false;
                this.WhiteWins.Visible = false;
                this.Draw.Visible = false;
                if (locationNow == panelLocation)
                {
                    locationNow = new Point(-1, -1);
                    locationLast = new Point(-1, -1);
                }
                selectedPiece = findChessPiece(panelLocation);
                if (selectedPiece != null)
                {
                    chessPieces.Remove(selectedPiece);
                    panel.BackgroundImage = null;
                }
                else
                {
                    //新建棋子。也可以试试换成随机产生一个。
                    ChessPiece addedPiece = new Pawn(panelLocation, whiteTurn);
                    chessPieces.Add(addedPiece);
                    panel.BackgroundImage = addedPiece.getImage();
                    panel.MouseWheel += panelSelectFromAllPieces;
                    if(panelLocation.X <= 4 && panelLocation.X >= 3 && panelLocation.Y <= 4 && panelLocation.Y >= 3)
                    {
                        panel.MouseWheel -= boardRotate;
                    }
                    modifyingPanel = panel;

                }
                selectedPiece = null;
                if (potentialMoves != null)
                    potentialMoves.Clear();
                return;
            }
            
            


            //如果所选panel已是高亮格，移动已选棋子至此处
            if (potentialMoves != null && potentialMoves.Contains(panelLocation))
            {
                movePiece(panelLocation);
                selectedPiece = null;
                potentialMoves.Clear();
                return;
            }
            selectedPiece = findChessPiece(panelLocation);

            //若点击的是空白格或对方棋子，则仅刷新potentialMoves和selectedPiece
            if (selectedPiece == null || selectedPiece.getColor() != this.whiteTurn)
            {
                selectedPiece = null;
                if (potentialMoves != null)
                    potentialMoves.Clear();
                return;
            }

            //计算可移动区域并高亮
            potentialMoves = selectedPiece.CalculateMovesWithKingConsidered(chessPieces);
            if (potentialMoves != null)
            {
                foreach (Point point in potentialMoves)
                {
                    //Console.WriteLine(point);
                    if (point.Y % 2 != 0)
                        findPanelByLocation(point).BackColor = point.X % 2 != 0 ? Color.FromArgb(160, 144, 238, 144) : Color.FromArgb(134, 144, 238, 144);
                    else
                        findPanelByLocation(point).BackColor = point.X % 2 != 0 ? Color.FromArgb(134, 144, 238, 144) : Color.FromArgb(160, 144, 238, 144);
                }
            }
            if (panelLocation.Y % 2 != 0)
                findPanelByLocation(panelLocation).BackColor = panelLocation.X % 2 != 0 ? Color.FromArgb(224, 16, 139, 139) : Color.FromArgb(192, 16, 139, 139);
            else
                findPanelByLocation(panelLocation).BackColor = panelLocation.X % 2 != 0 ? Color.FromArgb(192, 16, 139, 139) : Color.FromArgb(224, 16, 139, 139);
        
        }

        

    }
}
