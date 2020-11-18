using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames3D
{
    class Scene
    {
        private Actor[] _actors;
        private Matrix4 _transform = new Matrix4();

        public bool Started { get; private set; }

        public Scene()
        { _actors = new Actor[0]; }

        public Matrix4 World { get { return _transform; } }

        public void AddActor(Actor actor)
        {
            Actor[] newArray = new Actor[_actors.Length + 1];
            for (int i = 0; i < _actors.Length; i ++)
            { newArray[i] = _actors[i]; }
            newArray[_actors.Length] = actor;
            _actors = newArray;
        }
        public bool RemoveActor(Actor actor)
        {
            if (actor == null) { return false; }
            bool actorRemoved = false;
            Actor[] newArray = new Actor[_actors.Length - 1];
            int j = 0;
            for (int i = 0; i <_actors.Length; i ++)
            {
                if (actor != _actors[i]) { newArray[j] = _actors[i]; }
                else 
                { 
                    actorRemoved = true;
                    if (actor.Started) { actor.End(); }
                }
            }
            _actors = newArray;
            return actorRemoved;
        }
        public bool RemoveActor(int index)
        {
            if (index < 0 || index >= _actors.Length) { return false; }
            bool actorRemoved = false;
            Actor[] newArray = new Actor[_actors.Length - 1];
            int j = 0;
            for (int i = 0; i < _actors.Length; i++)
            {
                if (i != index)
                {
                    newArray[j] = _actors[i];
                    j++;
                }
                else
                {
                    actorRemoved = true;
                    if (_actors[i].Started)
                        _actors[i].End();
                }
            }
            _actors = newArray;
            return actorRemoved;
        }
        private void CheckCollision()
        {

        }
        public virtual void Start()
        {
            Started = true;
        }

        public virtual void Update(float deltaTime)
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                if (!_actors[i].Started)
                    _actors[i].Start();

                _actors[i].Update(deltaTime);
            }
            CheckCollision();
        }

        public virtual void Draw()
        {
            Raylib.DrawPlane(new System.Numerics.Vector3(5, 0, 5), new System.Numerics.Vector2(10, 10), Color.YELLOW);
            Raylib.DrawPlane(new System.Numerics.Vector3(-5, 0, 5), new System.Numerics.Vector2(10, 10), Color.BLUE);
            Raylib.DrawPlane(new System.Numerics.Vector3(5, 0, -5), new System.Numerics.Vector2(10, 10), Color.GOLD);
            Raylib.DrawPlane(new System.Numerics.Vector3(-5, 0, -5), new System.Numerics.Vector2(10, 10), Color.DARKBLUE);
            for (int i = 0; i < _actors.Length; i++)
            {
                _actors[i].Draw();
            }
        }

        public virtual void End()
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                if (_actors[i].Started)
                    _actors[i].End();
            }

            Started = false;
        }
    }
}
