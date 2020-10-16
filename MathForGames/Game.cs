using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using MathLib;

namespace MathForGames
{
    class Game
    {
        private static bool _gameOver = false;
        private static Scene _scene;
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
        public static void PlaceGolfBall(Player player)
        {
            ball._visible = true;
            float x = player.Position.X;
            float y = player.Position.Y;
            if (player.direction == 1)
            {
                if (_scene.CheckPositionAvailable(x += 1, y))
                { MoveBall(player, 1, 0); }  
            }
            if (player.direction == 2)
            {
                if (_scene.CheckPositionAvailable(x, y += 1))
                { MoveBall(player,0,1);}                
            }
            if (player.direction == 3)
            {
                if (_scene.CheckPositionAvailable(x -= 1, y))
                { MoveBall(player,-1,0);}
            }
            if (player.direction == 4)
            {
                if (_scene.CheckPositionAvailable(x, y - 1))
                { MoveBall(player,0,-1);}
            }
        }
        static void MoveBall(Player player, float xValIncrease, float yValIncrease)
        {
            ball.Position.X = player.Position.X + xValIncrease;
            ball.Position.Y = player.Position.Y + yValIncrease;
        }
        //Called when the game begins. Use this for initialization.
        void Start()
        {
            Console.CursorVisible = false;
            _scene = new Scene();
            Entity goal = new Entity(30, 30, '■', ConsoleColor.Green);
            player1 = new Player(0, 0, '►', ConsoleColor.Red);
            ball = new GolfBall(0, 0, '∙', ConsoleColor.Black);
            _scene.AddEntity(player1);
            _scene.AddEntity(goal);
            _scene.AddEntity(ball);
        }


        //Called every frame.
        public void Update()
        {
            _scene.Update();
        }

        //Used to display objects and other info on the screen.
        public void Draw()
        {
            Console.Clear();
            _scene.Draw();
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
