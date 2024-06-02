using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace mygame
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            const int windowHeight = 20;
            const int windowWidth = 40;
            const int gameSpeed = 50;

            Console.CursorVisible = false;
            Console.WindowHeight = windowHeight + 2;
            Console.WindowWidth = windowWidth + 2;
            Console.BufferHeight = windowHeight + 2;
            Console.BufferWidth = windowWidth + 2;

            Game game = new Game(windowHeight, windowWidth, gameSpeed);

            while (true)
            {
                game.ProcessUserInput();
                await game.AIControlAsync();
                game.Update();
                game.Draw();
                System.Threading.Thread.Sleep(game.GameSpeed);
            }

            Console.ReadLine();

        }
    }
 }

