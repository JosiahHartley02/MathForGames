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
        protected Vector2 _position;
        protected Vector2 _velocity;*/
        protected Matrix3 _transform = new Matrix3();
        private Vector2 _facing;
        protected ConsoleColor _color;
        protected Color _rayColor;
        public bool Started { get; private set; }

        public Vector2 Forward
        {
            get { return _facing; }
            set { _facing = value; }
        }


        public Vector2 Position
        {
            get
            {
                return new Vector2(_transform.m13, _transform.m23);
            }
            set
            {
                _transform.m13 += value.X;
                _transform.m23 += value.Y;
            }
        }

        public Vector2 xVelocity
        {
            get
            {
                return new Vector2(_transform.m11,_transform.m21);
            }
            set
            {
                _transform.m11 += value.X;
                _transform.m21 += value.Y;
            }
        }
        public Vector2 yVelocity
        {
            get
            {
                return new Vector2(_transform.m12, _transform.m22);
            }
            set
            {                
                _transform.m12 += value.X;
                _transform.m22 += value.Y;
            }

        }
        public Vector2 Direction
        {
            get
            {
                return new Vector2(xVelocity.Magnitude, yVelocity.Magnitude);
            }
            set
            {
                Direction = value;
                return;
            }
        }




        public Actor(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
        {
            _rayColor = Color.WHITE;
            _icon = icon;
            Position = new Vector2(x, y);
            xVelocity = new Vector2(0,0);
            yVelocity = new Vector2(0,0);
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
            if (Direction.Magnitude <= 0)
                return;

            _facing = Direction.Normalized;
        }

        public virtual void Start()
        {
            Started = true;
        }

        
        public virtual void Update(float deltaTime)
        {
            UpdateFacing();
            Position += Direction * deltaTime;
            Position.X = Math.Clamp(Position.X, 0, Console.WindowWidth-1);
            Position.Y = Math.Clamp(Position.Y, 0, Console.WindowHeight-1);

        }

        public virtual void Draw()
        {
            Raylib.DrawText(_icon.ToString(), (int)(Position.X * 32), (int)(Position.Y * 32), 32, _rayColor);
            Raylib.DrawLine(
                (int)(Position.X * 32),
                (int)(Position.Y * 32),
                (int)((Position.X + Forward.X) * 32),
                (int)((Position.Y + Forward.Y) * 32),
                Color.WHITE
            );

            Console.ForegroundColor = _color;
            Console.SetCursorPosition((int)Position.X, (int)Position.Y);
            Console.Write(_icon);
            Console.ForegroundColor = Game.DefaultColor;
            

        }

        public virtual void End()
        {
            Started = false;
        }

    }
}
