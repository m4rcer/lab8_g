using AlgoritmLi;
using lab8_g.classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab8_g
{
    public partial class Form1 : Form
    {
        const int ceilsInRow = 15;
        Graphics g;
        Bitmap bgImage;
        int pbW;
        System.Drawing.Point heroPos;
        Point snakePos;
        Timer timer;
        bool isLose = false;

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DrawBackgroung();
        }

        private void DrawBackgroung()
        {
            pbW = pictureBox.Size.Width;

            var bgImg = Image.FromFile("img/bg.png");
            var bmBg = new Bitmap(bgImg, pbW, pbW);
            var g = Graphics.FromImage(bmBg);

            var bushImg = Image.FromFile("img/bush.png");
            var bushBm = new Bitmap(bushImg, 40, 40);

            var treeImg = Image.FromFile("img/tree.png");
            var treeBm = new Bitmap(treeImg, 40, 40);

            var houseImg = Image.FromFile("img/house.png");
            var houseBm = new Bitmap(houseImg, 40, 40);

            for (int i = 0; i < ceilsInRow; i++)
            {
                int step = pbW / ceilsInRow;
                var startP = new Point(step * i, 0);
                var endP = new Point(step * i, pbW);
                var pen = new Pen(Color.DarkGreen, 1);
                g.DrawLine(pen, startP, endP);
                pen.Dispose();
            }

            for (int i = 0; i < ceilsInRow; i++)
            {
                int step = pbW / ceilsInRow;
                var startP = new Point(0, step * i);
                var endP = new Point(pbW, step * i);
                var pen = new Pen(Color.DarkGreen, 1);
                g.DrawLine(pen, startP, endP);
                pen.Dispose();
            }

            for (int i = 0; i < ceilsInRow; i++)
            {
                for (int j = 0; j < ceilsInRow; j++)
                {
                    if (Storage.field[i, j] == 2) {
                        var step = pbW / ceilsInRow;
                        var p = new Point(step * (j), step * (i));
                        g.DrawImage(bushBm, p);
                    }
                    if (Storage.field[i, j] == 1)
                    {
                        var step = pbW / ceilsInRow;
                        var p = new Point(step * (j), step * (i));
                        g.DrawImage(treeBm, p);
                    }
                    if (Storage.field[i, j] == 6)
                    {
                        var step = pbW / ceilsInRow;
                        var p = new Point(step * (j), step * (i));
                        g.DrawImage(houseBm, p);
                    }
                }
            }

            
            bgImage = bmBg;

            g.Dispose();
            treeBm.Dispose();
            treeImg.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            g = pictureBox.CreateGraphics();
            g.DrawImage(bgImage, new Point(0, 0));

            button1.Enabled = false;

            FillEntities();
            DrawEntities();
            
            timer = new Timer();
            timer.Interval = 300;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Enabled = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (isLose)
                return;
            Point[] path = findShortestWayToPlayer(snakePos.X, snakePos.Y, heroPos.X, heroPos.Y);
            if(path.Length != 0)
                MoveSnake(path[0].X, path[0].Y);
        }

        private Point[] findShortestWayToPlayer(int I, int J, int K, int L)
        {
            int[,] map = new int[ceilsInRow, ceilsInRow];

            for (int i = 0; i < ceilsInRow; i++)
            {
                for (int j = 0; j < ceilsInRow; j++)
                {
                    if (Storage.field[i,j] == 1 || Storage.field[i, j] == 2)
                        map[i,j] = 1;
                    else
                        map[i,j] = 0;
                }
            }

            var searcher = new PathSearchLee(PathSearchLee.SearchMethod.Path4);
            var start = new PathSearchLee.Point(I, J);
            var end = new PathSearchLee.Point(K, L);
            AlgoritmLi.PathSearchLee.Point[] path = searcher.Search(map, start, end);

            Point[] newPath = new Point[path.Length];

            for (int i = 0; i < path.Length; i++)
            {
                newPath[i] = new Point(path[i].X, path[i].Y);
            }

            return newPath;
        }

        private void PickUpCoin(int I, int J)
        {
            var item = Storage.fieldEntities[I, J];
            if(item == null)
                return;
            Coin coin = ((Coin)Storage.fieldEntities[I, J]);
            coin.PickUp();
            scoreLabel.Text = Storage.score.ToString();
            g.DrawImage(coin.cloneBm, coin.J * 40, coin.I * 40);
            //DrawEntities();
        }

        private void MoveHero(int I, int J)
        {
            if (I < 0 || J < 0 || I > 14 || J > 14)
                return;
            Hero hero = (Hero)Storage.fieldEntities[heroPos.X, heroPos.Y];
            if (Storage.field[I, J] == 1 || Storage.field[I, J] == 2)
                return;

            if(Storage.field[I, J] == 3)
            {
                PickUpCoin(I, J);
            }

            if(Storage.field[I, J] == 5)
            {
                timer.Stop();
                timer.Enabled = false;
                isLose = true;
                MessageBox.Show("You lose!");
            }

            if (Storage.field[I, J] == 6)
            {
                timer.Enabled = false;
                isLose = true;
                MessageBox.Show("You win!");
            }


            g.DrawImage(hero.cloneBm, hero.J * 40, hero.I * 40);
            g.DrawImage(hero.sprite, J * 40, I * 40);

            hero.Move(I, J);

            heroPos = new Point(I, J);
            DrawEntities();
        }

        private void MoveSnake(int I, int J)
        {
            if (isLose)
                return;

            Snake snake = (Snake)Storage.fieldEntities[snakePos.X, snakePos.Y];
            if (Storage.field[I, J] == 1 || Storage.field[I, J] == 2)
                return;

            if (Storage.field[I, J] == 4)
            {
                timer.Stop();
                timer.Enabled = false;
                isLose = true;
                MessageBox.Show("You lose!");
            }

            g.DrawImage(snake.cloneBm, snake.J * 40, snake.I * 40);
            g.DrawImage(snake.sprite, J * 40, I * 40);

            snake.Move(I, J);

            snakePos = new Point(I, J);
            DrawEntities();
        }

        private void FillEntities()
        {
             for (int i = 0; i < ceilsInRow; i++)
                {
                    for (int j = 0; j < ceilsInRow; j++)
                    {
                        if (Storage.field[i, j] == 3)
                        {
                            var coin = new Coin(i, j, bgImage);
                            Storage.fieldEntities[i, j] = coin;
                        }
                        if (Storage.field[i, j] == 4)
                        {
                            var hero = new Hero(i, j, bgImage);
                            Storage.fieldEntities[i, j] = hero;
                            heroPos = new Point(i, j);
                        }
                        if (Storage.field[i, j] == 5)
                        {
                            var snake = new Snake(i, j, bgImage);
                            Storage.fieldEntities[i, j] = snake;
                            snakePos = new Point(i, j);
                        }
                    }
                }
        }

        private void DrawEntities()
        {
            if (isLose)
                return;
            for (int i = 0; i < ceilsInRow; i++)
                {
                    for (int j = 0; j < ceilsInRow; j++)
                    {
                        if (Storage.field[i, j] == 3)
                        {
                            Coin coin = (Coin)Storage.fieldEntities[i, j];
                            var step = pbW / ceilsInRow;
                            var p = new Point(step * (j), step * (i));
                            g.DrawImage(coin.sprite, p);
                        }
                        if (Storage.field[i, j] == 4)
                        {
                            Hero hero = (Hero)Storage.fieldEntities[i, j];
                            var step = pbW / ceilsInRow;
                            var p = new Point(step * (j), step * (i));
                            g.DrawImage(hero.sprite, p);
                        }
                        if (Storage.field[i, j] == 5)
                        {
                            Snake snake = (Snake)Storage.fieldEntities[i, j];
                            var step = pbW / ceilsInRow;
                            var p = new Point(step * (j), step * (i));
                            g.DrawImage(snake.sprite, p);
                        }
                    }
                }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;

            if (e.KeyCode == Keys.W)
            {
                MoveHero(heroPos.X - 1, heroPos.Y);
            }
            if (e.KeyCode == Keys.S)
            {
                MoveHero(heroPos.X + 1, heroPos.Y);
            }
            if (e.KeyCode == Keys.A)
            {
                MoveHero(heroPos.X, heroPos.Y - 1);
            }
            if (e.KeyCode == Keys.D)
            {
                MoveHero(heroPos.X, heroPos.Y + 1);
            }
        }
    }
}
