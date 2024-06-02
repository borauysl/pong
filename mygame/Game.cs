using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace mygame
{
    public class Game
    {
        public int WindowHeight { get; }
        public int WindowWidth { get; }
        public int PlayerLeftScore { get; set; }
        public int PlayerRightScore { get; set; }
        public int GameSpeed { get; }

        private Paddle _paddleLeft;
        private Paddle _paddleRight;
        private Ball _ball;

        public Game(int windowHeight, int windowWidth, int gameSpeed)
        {
            WindowHeight = windowHeight;
            WindowWidth = windowWidth;
            GameSpeed = gameSpeed;

            InitializeGame();
        }

        public void InitializeGame()
        {
            _paddleLeft = new Paddle(0, WindowHeight / 2 - 2, 4, 1);
            _paddleRight = new Paddle(WindowWidth - 1, WindowHeight / 2 - 2, 4, 1);
            _ball = new Ball(WindowWidth / 2, WindowHeight / 2, 1, 1);
            PlayerLeftScore = 0;
            PlayerRightScore = 0;
        }

        public void Update()
        {
            _ball.PositionX += _ball.DirectionX;
            _ball.PositionY += _ball.DirectionY;

            if (_ball.PositionY == 0 || _ball.PositionY == WindowHeight - 1)
            {
                _ball.DirectionY = -_ball.DirectionY;
            }

            if (_ball.PositionX == 0)
            {
                PlayerRightScore++;
                ResetBall();
            }
            else if (_ball.PositionX == WindowWidth - 1)
            {
                PlayerLeftScore++;
                ResetBall();
            }

            if ((_ball.PositionX == 1 && _ball.PositionY >= _paddleLeft.PositionY && _ball.PositionY < _paddleLeft.PositionY + _paddleLeft.Height) ||
                (_ball.PositionX == WindowWidth - 2 && _ball.PositionY >= _paddleRight.PositionY && _ball.PositionY < _paddleRight.PositionY + _paddleRight.Height))
            {
                _ball.DirectionX = -_ball.DirectionX;
            }
        }

        public async Task AIControlAsync()
        {
            await Task.Run(() =>
            {
                if (_ball.PositionX >= WindowWidth / 2 && _ball.DirectionX == 1)
                {
                    if (_ball.PositionY < _paddleRight.PositionY + _paddleRight.Height / 2 && _paddleRight.PositionY > 0)
                    {
                        _paddleRight.MoveUp();
                    }
                    else if (_ball.PositionY > _paddleRight.PositionY + _paddleRight.Height / 2 && _paddleRight.PositionY < WindowHeight - _paddleRight.Height)
                    {
                        _paddleRight.MoveDown(WindowHeight);
                    }
                }
            });
        }

        public void ProcessUserInput()
        {
            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.W)
                {
                    MoveLeftPaddleUp();
                }
                if (key.Key == ConsoleKey.S)
                {
                    MoveLeftPaddleDown();
                }
            }
        }

        public void ResetBall()
        {
            _ball.PositionX = WindowWidth / 2;
            _ball.PositionY = WindowHeight / 2;
            _ball.DirectionX = -_ball.DirectionX;
        }

        public void MoveLeftPaddleUp()
        {
            _paddleLeft.MoveUp();
        }

        public void MoveLeftPaddleDown()
        {
            _paddleLeft.MoveDown(WindowHeight);
        }

        public void Draw()
        {
            Console.Clear();

            for (int i = 0; i <= WindowHeight; i++)
            {
                for (int j = 0; j <= WindowWidth; j++)
                {
                    if ((j == 0 && i >= _paddleLeft.PositionY && i < _paddleLeft.PositionY + _paddleLeft.Height) ||
                        (j == WindowWidth - 1 && i >= _paddleRight.PositionY && i < _paddleRight.PositionY + _paddleRight.Height))
                    {
                        Console.Write("|");
                    }
                    else if (i == _ball.PositionY && j == _ball.PositionX)
                    {
                        Console.Write("O");
                    }
                    else
                    {
                        Console.Write(" ");
                    }

                    if (j == WindowWidth)
                    {
                        Console.WriteLine();
                    }
                }
            }

            Console.WriteLine($"Player Left: {PlayerLeftScore}\t\tPlayer Right: {PlayerRightScore}");
        }

    }
}
    
