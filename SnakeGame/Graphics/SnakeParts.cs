using System.Collections.Generic;

namespace SnakeGame.Graphics
{
    struct SnakeParts
    {
        public List<SPoint> bodyPoints;
        public SPoint centerPoint;

        public SnakeParts(List<SPoint> bodyPoints, SPoint center)
        {
            this.bodyPoints = bodyPoints;
            this.centerPoint = center;
        }
    }
}
