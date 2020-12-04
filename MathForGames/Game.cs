using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Game
    {
        //Basic Game Engine Assets
        private static bool _gameOver = false;
        public static void SetGameOver(bool value) { _gameOver = value; }
        private static bool _debugVisual = false;
        public static bool DebugVisual { get { return _debugVisual; } set { _debugVisual = value; } }
        public static bool GetKeyDown(int key) { return Raylib.IsKeyDown((KeyboardKey)key); }
        public static bool GetKeyPressed(int key) { return Raylib.IsKeyPressed((KeyboardKey)key); }

        //Scene Tools
        private static Scene[] _scenes;
        private static int _currentSceneIndex;
        public static int CurrentSceneIndex { get { return _currentSceneIndex; } }
        public static Scene GetScene(int index)
        {
            if (index < 0 || index >= _scenes.Length)
                return new Scene();

            return _scenes[index];
        }        
        public static Scene GetCurrentScene()
        {
            return _scenes[_currentSceneIndex];
        }
        public static int AddScene(Scene scene)
        {
            //If the scene is null then return before running any other logic
            if (scene == null)
                return -1;

            //Create a new temporary array that one size larger than the original
            Scene[] tempArray = new Scene[_scenes.Length + 1];

            //Copy values from old array into new array
            for (int i = 0; i < _scenes.Length; i++)
            {
                tempArray[i] = _scenes[i];
            }

            //Store the current index
            int index = _scenes.Length;

            //Sets the scene at the new index to be the scene passed in
            tempArray[index] = scene;

            //Set the old array to the tmeporary array
            _scenes = tempArray;

            return index;
        }
        public static bool RemoveScene(Scene scene)
        {
            //If the scene is null then return before running any other logic
            if (scene == null)
                return false;

            bool sceneRemoved = false;

            //Create a new temporary array that is one less than our original array
            Scene[] tempArray = new Scene[_scenes.Length - 1];

            //Copy all scenes except the scene we don't want into the new array
            int j = 0;
            for (int i = 0; i < _scenes.Length; i++)
            {
                if (tempArray[i] != scene)
                {
                    tempArray[j] = _scenes[i];
                    j++;
                }
                else
                {
                    sceneRemoved = true;
                }
            }

            //If the scene was successfully removed set the old array to be the new array
            if (sceneRemoved)
                _scenes = tempArray;

            return sceneRemoved;
        }

        public static void SetCurrentScene(int index)
        {
            //If the index is not within the range of the the array return
            if (index < 0 || index >= _scenes.Length)
                return;

            //Call end for the previous scene before changing to the new one
            if (_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].End();

            //Update the current scene index
            _currentSceneIndex = index;
        }

        public Game()
        {
            _scenes = new Scene[0];
        }

        //Called when the game begins. Use this for initialization.
        public void Start()
        {
            //Creates a new window for raylib
            Raylib.InitWindow(1024, 760, "Math For Games");
            Raylib.SetTargetFPS(60);

            //Set up console window
            Console.CursorVisible = false;
            Console.Title = "Math For Games";

            //Create a new scene for our actors to exist in
            Scene scene1 = new Scene();
            Scene scene2 = new Scene();
             
            Player player = new Player(2, 10, Color.RED, ' ', ConsoleColor.Red);                        
            PlayersTurret gun = new PlayersTurret(0, 0, Color.RED, ' ', ConsoleColor.Red);
            Projectile playersBullet = new Projectile(new Matrix3(), "sprites/Bullets/bulletRed.png");

            Enemy enemy = new Enemy(0, 0, Color.GREEN, ' ', ConsoleColor.Green);
            EnemysTurret Egun = new EnemysTurret(0, 0, Color.BLUE, ' ', ConsoleColor.Blue);
            Projectile enemysBullet = new Projectile(new Matrix3(), "sprites/Bullets/bulletblue.png");

            Egun.AddTarget(player);
            Egun.AddAmmo(enemysBullet);
            player.Speed = 5;
            playersBullet.SetScale(1, 1);
            enemysBullet.SetScale(1, 1);
            gun.SetScale(1, 2);
            Egun.SetScale(1, 2);

            player.AddChild(gun);
            player.AddTarget(enemy);
            gun.AddAmmo(playersBullet);

            enemy.LocalPosition = new Vector2(5, 5);
            enemy.AddChild(Egun);
            enemy.AddTarget(player);

            scene1.AddActor(player);
            scene1.AddActor(enemy);
            scene1.AddActor(gun);
            scene1.AddActor(playersBullet);
            scene1.AddActor(Egun);
            scene1.AddActor(enemysBullet);
            

            //Sets the starting scene index and adds the scenes to the scenes array
            int startingSceneIndex = 0;
            startingSceneIndex = AddScene(scene1);
            AddScene(scene2);

            //Sets the current scene to be the starting scene index
            SetCurrentScene(startingSceneIndex);
        }



        /// <summary>
        /// Called every frame
        /// </summary>
        /// <param name="deltaTime">The time between each frame</param>
        public void Update(float deltaTime)
        {
            if (!_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].Start();

            _scenes[_currentSceneIndex].Update(deltaTime);
            bool debugMode = GetKeyDown((int)KeyboardKey.KEY_F1);
            Game.DebugVisual = debugMode;
        }

        //Used to display objects and other info on the screen.
        public void Draw()
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.BLACK);
            Console.Clear();
            _scenes[_currentSceneIndex].Draw();

            Raylib.EndDrawing();
        }


        //Called when the game ends.
        public void End()
        {
            if (_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].End();
        }


        //Handles all of the main game logic including the main game loop.
        public void Run()
        {
            //Call start for all objects in game
            Start();


            //Loops the game until either the game is set to be over or the window closes
            while (!_gameOver && !Raylib.WindowShouldClose())
            {
                //Stores the current time between frames
                float deltaTime = Raylib.GetFrameTime();
                //Call update for all objects in game
                Update(deltaTime); 
                //Call draw for all objects in game
                Draw();
                //Clear the input stream for the console window
                while (Console.KeyAvailable)
                    Console.ReadKey(true);
            }

            End();
        }
    }
}
