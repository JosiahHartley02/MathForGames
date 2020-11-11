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
        private static int _currentSceneIndex;
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
            _camera.position = new System.Numerics.Vector3(0.0f, 10.0f, 10.0f);
            _camera.target = new System.Numerics.Vector3(0.0f, 0.0f, 0.0f);
            _camera.up = new System.Numerics.Vector3(0.0f, 1.0f, 0.0f);
            _camera.fovy = 45.0f;
            _camera.type = CameraType.CAMERA_PERSPECTIVE;
            Scene SolarSystem = new Scene();
            Actor sun = new Actor(0, 0, 0, Color.YELLOW);
            Actor mercury = new Actor(1, 0, 0, Color.DARKGRAY);
            Actor venus = new Actor(2, 0, 0,Color.GRAY);
            Actor earth = new Actor(3, 0, 0, Color.GREEN);
            Actor moon = new Actor(1,0,0, Color.GRAY);
            SolarSystem.AddActor(sun);
            SolarSystem.AddActor(mercury);
            SolarSystem.AddActor(venus);
            SolarSystem.AddActor(earth);
            SolarSystem.AddActor(moon);
            sun.AddChild(mercury);
            sun.AddChild(venus);
            sun.AddChild(earth);
            earth.AddChild(moon);
            sun.SetScale(1.5f, 1.5f, 1.5f);
            mercury.SetScale(0.3f, 0.3f, 0.3f);
            venus.SetScale(0.4f, 0.4f, 0.4f);
            earth.SetScale(0.5f, 0.5f, 0.5f);
            moon.SetScale(0.2f, 0.2f, 0.2f);
            moon.SetTranslation(new Vector3(2, 0, 0));
            int startingSceneIndex = 0;
            startingSceneIndex = AddScene(SolarSystem);
            SetCurrentScene(startingSceneIndex);
        }

        public void Update(float deltaTime)
        {
            if (!_scenes[_currentSceneIndex].Started)
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
