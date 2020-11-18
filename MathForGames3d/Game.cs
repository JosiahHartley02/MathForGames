using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames3D
{
    class Game
    {
        //create scene, actor, matrix4, and vector 4
        private static bool _gameOver;
        private static Scene[] _scenes = new Scene[0];
        private Camera3D _camera = new Camera3D();
        private static int _currentSceneIndex;/*
        Actor CameraOrigin;
        Actor Camera;*/
        public static bool GameOver
        {
            get { return _gameOver; } set { _gameOver = value; }
        }
        public static bool GetKeyDown(int key)
        {
            return Raylib.IsKeyDown((KeyboardKey)key);
        }
        public static bool GetKeyPressed(int key)
        {
            return Raylib.IsKeyPressed((KeyboardKey)key);
        }
        public static int CurrentSceneIndex
        {
            get
            {
                return _currentSceneIndex;
            }
        }

        private void Start()
        {
            Raylib.InitWindow(1024, 760, "Math For Games");
            Raylib.SetTargetFPS(60);
            _camera.position = new System.Numerics.Vector3(0, 10.0f, -10);
            _camera.target = new System.Numerics.Vector3(0, 0, 0);
            _camera.up = new System.Numerics.Vector3(0.0f, 1.0f, 0.0f);
            _camera.fovy = 45.0f;
            _camera.type = CameraType.CAMERA_PERSPECTIVE;

            Scene SolarSystem = new Scene();
            Scene TankFight = new Scene();

            /*Actor sun = new Actor(0, 0, 0, Color.YELLOW);
            Actor mercury = new Actor(0, 0, 0, Color.DARKGRAY);
            Actor venus = new Actor(0, 0, 0,Color.GRAY);
            Actor earth = new Actor(0, 0, 0, Color.GREEN);
            Actor moon = new Actor(0,0,0, Color.GRAY);*/
            PlayerCosmetic Player = new PlayerCosmetic(0, 0, 0, Color.RED);
            Player _player = new Player(0, 0, 0, Color.BLUE);
            /*
            CameraOrigin = new Actor(0, 0, 0, Color.WHITE);
            Camera = new Actor(_camera.position.X, _camera.position.Y, _camera.position.Z, Color.GOLD);*//*

            SolarSystem.AddActor(sun);
            SolarSystem.AddActor(mercury);
            SolarSystem.AddActor(venus);
            SolarSystem.AddActor(earth);
            SolarSystem.AddActor(moon);
            */
            TankFight.AddActor(Player);
            TankFight.AddActor(_player);
            _player.AddChild(Player);/*
            TankFight.AddActor(CameraOrigin);
            TankFight.AddActor(Camera);
            CameraOrigin.AddChild(Camera);*/
            /*
            sun.AddChild(mercury);
            sun.AddChild(venus);
            sun.AddChild(earth);
            earth.AddChild(moon);

            sun.SetScale(1f);
            mercury.SetScale(0.3f);
            venus.SetScale(0.4f);
            earth.SetScale(0.5f);
            moon.SetScale(0.2f);
*/
            Player.SetScale(1);/*
            CameraOrigin.SetScale(1f);
            Camera.SetScale(1);*/

            /*sun.SetTranslation(0, 0, 0);
            mercury.SetTranslation(1.5f, 0, 1.5f);
            venus.SetTranslation(2.5f, 0, 2.5f);
            earth.SetTranslation(3.5f, 0, 3.5f);
            moon.SetTranslation(1, 0, 1);
*/
            Player.SetTranslation(0, 0, 0);/*
            CameraOrigin.SetTranslation(0, 0, 0);*/

            int startingSceneIndex = 0;
            AddScene(SolarSystem);
            startingSceneIndex = AddScene(TankFight);
            SetCurrentScene(startingSceneIndex);
        }

        public void Update(float deltaTime)
        {
            /*_camera.position = new System.Numerics.Vector3(Camera.WorldPosition.X, Camera.WorldPosition.Y, Camera.WorldPosition.Z);
            Camera.LocalPosition = new Vector3(
                Camera.LocalPosition.X + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_W)) - Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_S)),
                Camera.LocalPosition.Y + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_Q)) - Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_E)),
                Camera.LocalPosition.Z + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_A)) - Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_D))
                );
            int rotation = Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_COMMA)) - Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_PERIOD));
            CameraOrigin.Rotate(rotation * .05f);*/
            _scenes[_currentSceneIndex].Start();
            _scenes[_currentSceneIndex].Update(deltaTime);
        }

        private void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.BeginMode3D(_camera);

            Raylib.ClearBackground(Color.RAYWHITE);
            Raylib.DrawGrid(20, 1.0f);
            _scenes[_currentSceneIndex].Draw();

            Raylib.EndMode3D();
            Raylib.EndDrawing();
        }

        public void End()
        {
            if (_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].End();
        }

        public void Run()
        {
            Start();
            while (!_gameOver && !Raylib.WindowShouldClose())
            {
                float deltaTime = Raylib.GetFrameTime();
                Update(deltaTime);
                Draw();
            }
            End();
        }
        public static Scene GetCurrentScene()
        {
            return _scenes[_currentSceneIndex];
        }
        public static Scene GetScene(int index)
        {
            if (index < 0 || index >= _scenes.Length)
                return new Scene();

            return _scenes[index];
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
    }
}
