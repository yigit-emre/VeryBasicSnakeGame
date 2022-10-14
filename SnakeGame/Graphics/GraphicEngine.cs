using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SnakeGame.Graphics
{
    class GraphicEngine
    {
        // **** Private Variables ******

        private PictureBox Screen;
        private List<SnakeParts> parts = new List<SnakeParts>();
        private List<SPoint> directions = new List<SPoint>();
        private List<SnakeParts> Coin = new List<SnakeParts>();
        private bool aditionalBody = true;


        // ****** Protected Variables *******

        protected int additonalScore = 0;
        protected readonly int PartSize;
        protected SnakeParts Head { get { return parts[0]; } }

        // ******* Proctected Methods ********

        protected GraphicEngine(PictureBox Screen,int size = 17)
        {
            this.Screen = Screen;            
            if (10 < size && size < 18 && !(size % 2 == 0))
            {
                this.PartSize = size;
            }
            else
            {
                throw new System.Exception("Incorrect Size");
            }
        }

        protected async void StartGraphics(SPoint startPoint)
        {
            DrawHead(startPoint);
            await RuntimeAsync();
        }

        protected void AddDirection(SPoint point) 
        {
            directions.Add(point);
        }

        protected void ClearLists() 
        {
            parts.Clear();
            directions.Clear();
            Coin.Clear();
        }

        protected async void AddCoin(int score, bool addBody = true) 
        {
            await DrawCoinAsync(score, addBody);
        }
        
        //  ****** Private Methods ******

        private void DrawPart(SPoint centerPoint) 
        {
            List<SPoint> points = new List<SPoint>();
            int distance = (PartSize - 1) / 2;
            for (int y = -distance; y < (distance + 1); y++)
            {
                if (y == -distance || y == distance)
                {
                    for (int x = -distance; x < (distance + 1); x++)
                    {
                        points.Add(new SPoint(centerPoint.X + x, centerPoint.Y + y));
                    }
                }
                else
                {
                    points.Add(new SPoint(centerPoint.X - distance, centerPoint.Y + y));
                    points.Add(new SPoint(centerPoint.X + distance, centerPoint.Y + y));
                }
            }
            SnakeParts part = new SnakeParts(points, centerPoint);
            parts.Add(part);
        }

        private void DrawHead(SPoint point)
        {
            if (parts.Count == 0)
            {
                DrawPart(point); //This direction is starting direciton.
            }
        }

        private void AddBody()
        {
            if (parts.Count == 0)
            {
                throw new System.Exception("Head is not created yet!");
            }
            if (parts[parts.Count - 1].centerPoint.directionFlag == DirectionFlags.Up)
            {
                DrawPart(new Graphics.SPoint(parts[parts.Count - 1].centerPoint.X, parts[parts.Count - 1].centerPoint.Y + PartSize + 1, DirectionFlags.Up));
            }
            if (parts[parts.Count - 1].centerPoint.directionFlag == DirectionFlags.Down)
            {
                DrawPart(new Graphics.SPoint(parts[parts.Count - 1].centerPoint.X, parts[parts.Count - 1].centerPoint.Y - PartSize - 1, DirectionFlags.Down));
            }
            if (parts[parts.Count - 1].centerPoint.directionFlag == DirectionFlags.Right)
            {
                DrawPart(new Graphics.SPoint(parts[parts.Count - 1].centerPoint.X - PartSize - 1, parts[parts.Count - 1].centerPoint.Y, DirectionFlags.Right));
            }
            if (parts[parts.Count - 1].centerPoint.directionFlag == DirectionFlags.Left)
            {
                DrawPart(new Graphics.SPoint(parts[parts.Count - 1].centerPoint.X + PartSize + 1, parts[parts.Count - 1].centerPoint.Y, DirectionFlags.Left));
            }
        }

        private void Move()
        {
            List<SPoint> centerPoints = new List<SPoint>();
            int deletePointNum = -1;
            int counter = 0;
            bool isChanhed = true;
            foreach (SnakeParts snakePart in parts)
            {
                foreach (SPoint direction in directions)
                {
                    counter++;
                    if (direction.directionFlag == DirectionFlags.Down)
                    {
                        if (snakePart.centerPoint.X == direction.X && snakePart.centerPoint.Y == direction.Y)
                        {
                            if (direction.X == parts[parts.Count - 1].centerPoint.X && direction.Y == parts[parts.Count - 1].centerPoint.Y)
                            {
                                deletePointNum = counter;
                            }
                            centerPoints.Add(new SPoint(snakePart.centerPoint.X, snakePart.centerPoint.Y + 1, direction.directionFlag));
                            isChanhed = false;
                        }
                    }
                    if (direction.directionFlag == DirectionFlags.Up)
                    {
                        if (snakePart.centerPoint.X == direction.X && snakePart.centerPoint.Y == direction.Y)
                        {
                            if (direction.X == parts[parts.Count - 1].centerPoint.X && direction.Y == parts[parts.Count - 1].centerPoint.Y)
                            {
                                deletePointNum = counter;
                            }
                            centerPoints.Add(new SPoint(snakePart.centerPoint.X, snakePart.centerPoint.Y - 1, direction.directionFlag));
                            isChanhed = false;
                        }
                    }
                    if (direction.directionFlag == DirectionFlags.Right)
                    {
                        if (snakePart.centerPoint.X == direction.X && snakePart.centerPoint.Y == direction.Y)
                        {
                            if (direction.X == parts[parts.Count - 1].centerPoint.X && direction.Y == parts[parts.Count - 1].centerPoint.Y)
                            {
                                deletePointNum = counter;
                            }
                            centerPoints.Add(new SPoint(snakePart.centerPoint.X + 1, snakePart.centerPoint.Y, direction.directionFlag));
                            isChanhed = false;
                        }
                    }
                    if (direction.directionFlag == DirectionFlags.Left)
                    {
                        if (snakePart.centerPoint.X == direction.X && snakePart.centerPoint.Y == direction.Y)
                        {
                            if (direction.X == parts[parts.Count - 1].centerPoint.X && direction.Y == parts[parts.Count - 1].centerPoint.Y)
                            {
                                deletePointNum = counter;
                            }
                            centerPoints.Add(new SPoint(snakePart.centerPoint.X - 1, snakePart.centerPoint.Y, direction.directionFlag));
                            isChanhed = false;
                        }
                    }
                }
                if (isChanhed || directions.Count == 0)
                {
                    if (snakePart.centerPoint.directionFlag == DirectionFlags.Down)
                    {
                        centerPoints.Add(new SPoint(snakePart.centerPoint.X, snakePart.centerPoint.Y + 1, snakePart.centerPoint.directionFlag));
                    }
                    if (snakePart.centerPoint.directionFlag == DirectionFlags.Up)
                    {
                        centerPoints.Add(new SPoint(snakePart.centerPoint.X, snakePart.centerPoint.Y - 1, snakePart.centerPoint.directionFlag));
                    }
                    if (snakePart.centerPoint.directionFlag == DirectionFlags.Right)
                    {
                        centerPoints.Add(new SPoint(snakePart.centerPoint.X + 1, snakePart.centerPoint.Y, snakePart.centerPoint.directionFlag));
                    }
                    if (snakePart.centerPoint.directionFlag == DirectionFlags.Left)
                    {
                        centerPoints.Add(new SPoint(snakePart.centerPoint.X - 1, snakePart.centerPoint.Y, snakePart.centerPoint.directionFlag));
                    }
                }
                counter = 0;
                isChanhed = true;
            }
            if (deletePointNum != -1)
            {
                directions.RemoveAt(deletePointNum - 1);
            }
            parts.Clear();
            foreach (SPoint point in centerPoints)
            {
                DrawPart(point);
            }
        }

        private void RenderScreen() 
        {
            Bitmap bitmap = new Bitmap(Screen.Width, Screen.Height);
            Move();
            // ***** Draw Snake *******
            foreach (SnakeParts part in parts)
            {
                bitmap.SetPixel(part.centerPoint.X, part.centerPoint.Y, Color.Red);
                foreach (SPoint bodyPoint in part.bodyPoints)
                {
                    bitmap.SetPixel(bodyPoint.X, bodyPoint.Y, Color.Black);
                }
            }

            // ******* DrawCoin *******
            if (Coin.Count != 0)
            {
                foreach (SnakeParts partsCoin in Coin)
                {
                    bitmap.SetPixel(partsCoin.centerPoint.X, partsCoin.centerPoint.Y, Color.DarkGreen);
                    foreach (SPoint bodyCoin in partsCoin.bodyPoints)
                    {
                        bitmap.SetPixel(bodyCoin.X, bodyCoin.Y, Color.DarkGreen);
                    }
                }
            }
            Screen.Image = bitmap;
            Screen.Show();
        }

        private Task DrawCoinAsync(int score, bool addBody = true) 
        {
            return Task.Factory.StartNew(() => 
            {
                if (Coin.Count != 0)
                {
                    Coin.Clear();
                }

                List<SPoint> coinBody = new List<SPoint>();
                bool loop = true;
                bool loop1 = false;
                Random rnd = new Random();
                int distance = (PartSize - 1) / 2;
                int x = rnd.Next(PartSize + 1, Screen.Width - PartSize - 1);
                int y = rnd.Next(PartSize + 1, Screen.Height - PartSize - 1);

                while (loop)
                {
                    foreach (SnakeParts part in parts)
                    {
                        if ((part.centerPoint.X - distance - 1 < x && x < part.centerPoint.X + distance + 1) && (part.centerPoint.Y - distance - 1 < y && y < part.centerPoint.Y + distance + 1))
                        {
                            loop1 = true;
                        }
                    }
                    if (loop1)
                    {
                        x = rnd.Next(PartSize + 1, Screen.Width - PartSize - 1);
                        y = rnd.Next(PartSize + 1, Screen.Height - PartSize - 1);
                        loop1 = false;
                    }
                    else
                    {
                        loop = false;
                        this.aditionalBody = addBody;
                        this.additonalScore = score;
                        for (int ix = -2; ix < 3; ix++)
                        {
                            for (int iy = -2; iy < 3; iy++)
                            {
                                coinBody.Add(new SPoint(x + ix, y + iy, DirectionFlags.NULL));
                            }
                        }
                        Coin.Add(new SnakeParts(coinBody, new SPoint(x, y, DirectionFlags.NULL)));
                    }
                }
            });
        }

        private Task RuntimeAsync()
        {
            return Task.Factory.StartNew(() => 
            {
                // ****** Draw Snake Fully ********
                int distance = (PartSize - 1) / 2;
                while (Game.GameEngine.LoopTasks)
                {
                    if (Game.GameEngine.addBody == true)
                    {
                        AddBody();
                        Game.GameEngine.addBody = false;
                    }
                    RenderScreen();
                    // *** Checking Snake Crashing to Body ****
                    foreach (SnakeParts partsSnake in parts)
                    {
                        foreach (SPoint points in partsSnake.bodyPoints)
                        {
                            if (!(Head.centerPoint.X == partsSnake.centerPoint.X && Head.centerPoint.Y == partsSnake.centerPoint.Y) && directions.Count != 0)
                            {
                                if (!(((Head.centerPoint.X - PartSize - 1) < directions[directions.Count - 1].X && directions[directions.Count - 1].X < (Head.centerPoint.X + PartSize + 1)) && ((Head.centerPoint.Y - PartSize - 1) < directions[directions.Count - 1].Y && directions[directions.Count - 1].Y < (Head.centerPoint.Y + PartSize + 1))))
                                {
                                    if (((Head.centerPoint.X - distance) < points.X && points.X < (Head.centerPoint.X + distance)) && ((Head.centerPoint.Y - distance) < points.Y && points.Y < (Head.centerPoint.Y + distance)))
                                    {
                                        Game.GameEngine.LoopTasks = false;
                                    }
                                }
                            }
                        }
                    }
                    // ***** Checking has snake taken coin ******
                    if (Coin.Count != 0)
                    {
                        if ((Head.centerPoint.X - distance < Coin[0].centerPoint.X && Coin[0].centerPoint.X < Head.centerPoint.X + distance) && (Head.centerPoint.Y - distance < Coin[0].centerPoint.Y && Coin[0].centerPoint.Y < Head.centerPoint.Y + distance))
                        {
                            if (this.aditionalBody == true)
                            {
                                Game.GameEngine.addBody = true;
                            }
                            Game.GameEngine.Score += this.additonalScore;
                            Coin.Clear();
                            this.additonalScore = 0;
                        }
                    }
                    // ***** Checking has snake crahed the wall ******
                    if (!(distance +  1 < Head.centerPoint.X && Head.centerPoint.X <Screen.Width - distance - 1) || !(distance + 1 < Head.centerPoint.Y && Head.centerPoint.Y < Screen.Height - distance - 1))
                    {
                        Game.GameEngine.LoopTasks = false;
                    }
                    Thread.Sleep(10);
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}