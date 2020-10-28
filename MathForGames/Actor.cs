using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Actor
    {
        protected char _icon = ' ';/*
        protected Vector3 _position;
        protected Vector3 _velocity;*/
        private Vector2 _facing;
        protected Matrix3 _transform;
        protected ConsoleColor _color;
        protected Color _rayColor;
        public bool Started { get; private set; }
       public Vector2 Forward
        {
            get { return _facing; }
            set { _facing = value; }
        }

        public Matrix3 Transform
        {
          get { return _transform; }
        }

       /* public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }

        public Vector2 Velocity
        {
            get
            {
                return _velocity;
            }
            set
            {
                _velocity = value;
            }
        }*/


        public Actor(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
        {
            _rayColor = Color.WHITE;
            _icon = icon;
            Transform.Position = new Vector3(x, y, 0);
            Transform.Rotate = new Vector3();
            _color = color;
            Forward = new Vector2(1, 0);
        }

        public Actor(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : this(x,y,icon,color)
        {
            _rayColor = rayColor;
        }

        private void UpdateFacing()
        {
            if (_velocity.Magnitude <= 0)
                return;

            _facing = Velocity.Normalized;
        }

        public virtual void Start()
        {
            Started = true;
        }

        
        public virtual void Update(float deltaTime)
        {
            UpdateFacing();
            Transform.Position += _velocity * deltaTime;
            Transform.Position.X = Math.Clamp(Transform.Position.X, 0, Console.WindowWidth-1);
            Transform.Position.Y = Math.Clamp(Transform.Position.Y, 0, Console.WindowHeight-1);

        }

        public virtual void Draw()
        {
            Raylib.DrawText(_icon.ToString(), (int)(Transform.Position.X * 32), (int)(Transform.Position.Y * 32), 32, _rayColor);
            Raylib.DrawLine(
                (int)(Transform.Position.X * 32),
                (int)(Transform.Position.Y * 32),
                (int)((Transform.Position.X + Forward.X) * 32),
                (int)((Transform.Position.Y + Forward.Y) * 32),
                Color.WHITE
            );

            Console.ForegroundColor = _color;
            Console.SetCursorPosition((int)Transform.Position.X, (int)Transform.Position.Y);
            Console.Write(_icon);
            Console.ForegroundColor = Game.DefaultColor;
            

        }

        public virtual void End()
        {
            Started = false;
        }

    }
}
