namespace SnakeGame.Graphics
{
    struct SPoint
    {
        public int X;
        public int Y;
        public DirectionFlags directionFlag;

        public SPoint(int x, int y, DirectionFlags directionFlag = DirectionFlags.NULL)
        {
            this.X = x;
            this.Y = y;
            this.directionFlag = directionFlag;
        }
    }
}   
