using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mygame
{
    public class Ball : GameObject
    {
        public int DirectionX { get; set; }
        public int DirectionY { get; set; }

        public Ball(int positionX, int positionY, int directionX, int directionY) : base(positionX, positionY)
        {
            DirectionX = directionX;
            DirectionY = directionY;
        }

    }
}
