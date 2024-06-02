using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace mygame
{
    public class Paddle : GameObject
    {
        public int Height { get; }
        public int Width { get; }

        public Paddle(int positionX, int positionY, int height, int width) : base(positionX, positionY)
        {
            Height = height;
            Width = width;
        }

        public void MoveUp()
        {
            if (PositionY > 0)
                PositionY--;
        }

        public void MoveDown(int windowHeight)
        {
            if (PositionY < windowHeight - Height)
                PositionY++;
        }
    }
}
