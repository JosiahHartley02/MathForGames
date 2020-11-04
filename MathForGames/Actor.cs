using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{

    /// <summary>
    /// This is the base class for all objects that will 
    /// be moved or interacted with in the game
    /// </summary>
    class Actor
    {
        protected char _icon = ' ';
        protected Vector2 _velocity;
        protected Matrix3 _transform;
        protected Matrix3 _translation;
        protected Matrix3 _rotation;
        protected Matrix3 _scale;
        protected ConsoleColor _color;
        protected Color _rayColor;
        public bool Started { get; private set; }

        public Vector2 Forward
        {
            get 
            {
                return new Vector2(_transform.m11, _transform.m21);
            }
        }


        public Vector2 Position
        {
            get
            {
                return new Vector2(_transform.m13, _transform.m23);
            }
            set
            {
                _transform.m13 = value.X;
                _transform.m23 = value.Y;
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
        }
        protected Vector2 Scale
        {
            get { return new Vector2(_scale.m11, _scale.m22); }
            set { _scale.m11 = value.X; _scale.m22 = value.Y; }
        }
        protected float Rotation
        {
            get { return _rotation.m11; }
            set { 
                _rotation.m11 = (float)Math.Cos(value); _rotation.m12 = -(float)Math.Sin(value);
                _rotation.m21 = (float)Math.Sin(value); _rotation.m22 = (float)Math.Cos(value); 
                }
        }
        protected Vector2 Translate
        {
            get { return new Vector2(_translation.m13, _translation.m23); }
            set { _translation.m13 = value.X; _translation.m23 = value.Y; }
        }


        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn</param>
        public Actor(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
        {
            _rayColor = Color.WHITE;
            _icon = icon;
            _transform = new Matrix3();
            Position = new Vector2(x, y);
            _velocity = new Vector2();
            _color = color;/*
            Forward = new Vector2(1, 0);*/
        }


        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="rayColor">The color of the symbol that will appear when drawn to raylib</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn to the console</param>
        public Actor(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : this(x,y,icon,color)
        {
            _rayColor = rayColor;
            _transform = new Matrix3(0, 0, x * 32, 0, 0, y * 32, 0, 0, 1);
            _translation = new Matrix3();
            _rotation = new Matrix3(0, 0, 0, 0, 0, 0, 0, 0, 1);
            _scale = new Matrix3();
        }

        /// <summary>
        /// Updates the actors forward vector to be
        /// the last direction it moved in
        /// </summary>
        private void UpdateFacing()
        {
            if (_velocity.Magnitude <= 0)
                return;
/*
            Forward = Velocity.Normalized;*/
        }

        public virtual void Start()
        {
            Started = true;
        }

        
        public virtual void Update(float deltaTime)
        {
            //Before the actor is moved, update the direction it's facing
           /* UpdateFacing();*/

            //Increase position by the current velocity
            Position += _velocity * deltaTime;
        }

        public virtual void Draw()
        {
            //Draws the actor and a line indicating it facing to the raylib window.
            //Scaled to match console movement
            Raylib.DrawText(_icon.ToString(), (int)(Position.X * 32), (int)(Position.Y * 32), 32, _rayColor);
            Raylib.DrawLine(
                (int)(Position.X * 32),
                (int)(Position.Y * 32),
                (int)((Position.X + _rotation.m11) * 32),
                (int)((Position.Y + _rotation.m21) * 32),
                Color.BLUE
            );
            Raylib.DrawLine(
                (int)(Position.X * 32),
                (int)(Position.Y * 32),
                (int)((Position.X - _rotation.m12) * 32),
                (int)((Position.Y - _rotation.m22) * 32),
                Color.RED
                );

            //Changes the color of the console text to be this actors color
            Console.ForegroundColor = _color;

            //Only draws the actor on the console if it is within the bounds of the window
            if(Position.X >= 0 && Position.X < Console.WindowWidth 
                && Position.Y >= 0  && Position.Y < Console.WindowHeight)
            {
                Console.SetCursorPosition((int)Position.X, (int)Position.Y);
                Console.Write(_icon);
            }
            
            //Reset console text color to be default color
            Console.ForegroundColor = Game.DefaultColor;
        }

        public virtual void End()
        {
            Started = false;
        }

    }
}
