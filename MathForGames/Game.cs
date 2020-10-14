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
        private Scene _scene;

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
            if(!Console.KeyAvailable) //if the user has not press a key
            {
                return 0;             //do nothing
            }
            return Console.ReadKey(true).Key;  //since the function would have aborted if not true- returns the key pressed
        }
        public

        //Called when the game begins. Use this for initialization.
        void Start()
        {
            Console.CursorVisible = false;
            _scene = new Scene();
            BouncyBall ball1 = new BouncyBall(0, 10, 'o', ConsoleColor.DarkBlue);
            BouncyBall ball2 = new BouncyBall(10, 0, 'o', ConsoleColor.DarkBlue);
            BouncyBall ball3 = new BouncyBall(10, 10, 'o', ConsoleColor.DarkBlue);
            BouncyBall ball4 = new BouncyBall(0, 0, 'o', ConsoleColor.DarkBlue);
            Entity entity = new Entity(0,0,'■',ConsoleColor.Green);
            entity.Veloctity.X = 1;
            Player player = new Player(0, 1, '@', ConsoleColor.Red);
            _scene.AddEntity(entity);
            _scene.AddEntity(player);
            _scene.AddEntity(ball1);
            _scene.AddEntity(ball2);
            _scene.AddEntity(ball3);
            _scene.AddEntity(ball4);
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

            while(!_gameOver)
            {
                Update();
                Draw();
                while (Console.KeyAvailable)
                    Console.ReadKey(true);
                Thread.Sleep(200);
            }

            End();
        }
    }
}
