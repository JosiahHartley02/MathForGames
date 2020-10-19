using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using MathLib;
using Raylib_cs;

namespace MathForGames
{
    class Game
    {
        private static bool _gameOver = false;
        public static Scene scene1;
        public static Player player1;
        public static GolfBall ball;

        public static ConsoleColor DefaultColor { get; set; } = ConsoleColor.White;
        //Static function used to set game over without an instance of game.
        public static void SetGameOver(bool value)
        {
            _gameOver = value;
        }

        //public static bool CheckKey(ConsoleKey key) //return whether specified key is pressed
        // {
        //    if (Console.KeyAvailable)  //if the user has pressed a key
        //    {
        //       if (Console.ReadKey(true).Key == key) //Checks to see if the key if the key pressed matched the argument given
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        public static ConsoleKey GetNextKey()
        {
            if (!Console.KeyAvailable) //if the user has not press a key
            {
                return 0;             //do nothing
            }
            return Console.ReadKey(true).Key;  //since the function would have aborted if not true- returns the key pressed
        }

        //Called when the game begins. Use this for initialization.
        void Start()
        {
            Raylib.InitWindow(1024, 760, "Math for games");
            Console.CursorVisible = false;
            scene1 = new Scene();
            Entity goal = new Entity(30, 30, '■', ConsoleColor.Green);
            player1 = new Player(0, 0, '►', ConsoleColor.Red);
            ball = new GolfBall(0, 0, '∙', ConsoleColor.Black);
            scene1.AddEntity(player1);
            scene1.AddEntity(goal);
            scene1.AddEntity(ball);
        }


        //Called every frame.
        public void Update()
        {
            scene1.Update();
        }

        //Used to display objects and other info on the screen.
        public void Draw()
        {
            Console.Clear();
            scene1.Draw();
        }


        //Called when the game ends.
        public void End()
        {

        }


        //Handles all of the main game logic including the main game loop.
        public void Run()
        {
            Start();

            while (!_gameOver)
            {
                Update();
                Draw();
                while (Console.KeyAvailable)
                    Console.ReadKey(true);
                Thread.Sleep(250);
            }

            End();
        }
        
    }
}
