using System;
using System.Collections.Generic;
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
        protected Matrix3 _translate;
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
            set
            {
                _transform.m11 = value.X;
                _transform.m21 = value.Y;
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
        public Matrix3 Translate
        {
            get { return new Matrix3(0,0,_translate.m13,0,0, _translate.m23, 0,0,0); }
            set { _translate = new Matrix3(_translate.m11, _translate.m12, value.m13, _translate.m21, _translate.m22, value.m23, _translate.m31, _translate.m32, _translate.m33); }
        }
        public Matrix3 Rotate
        {
            get { return new Matrix3(_rotation.m11 - _scale.m11, _rotation.m12,0, _rotation.m21, _rotation.m22 - _scale.m22,0,0,0,0); }
            set { _rotation =new Matrix3(value.m11, value.m12,_rotation.m13, value.m21, value.m22,_rotation.m23, _rotation.m31, _rotation.m32, _rotation.m33); }
        }
        public Matrix3 Scale
        { 
            get { return new Matrix3(_scale.m11,0,0,0, _scale.m22,0,0,0,0); }
            set { _scale = new Matrix3(value.m11,_scale.m12, _scale.m13, _scale.m21, value.m22, _scale.m23, _scale.m31, _scale.m32, _scale.m33); }
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
            _color = color;
            Forward = new Vector2(1, 0);
        }


        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="rayColor">The color of the symbol that will appear when drawn to raylib</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn to the console</param>
        public Actor(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : this(x,y,icon,color)
        {
            _transform = new Matrix3();
            _rayColor = rayColor;
        }

        /// <summary>
        /// Updates the actors forward vector to be
        /// the last direction it moved in
        /// </summary>
        protected void UpdateFacing()
        {
            if (_velocity.Magnitude <= 0)
                return;

            Forward = Velocity.Normalized;
        }

        public virtual void Start()
        {
            Started = true;
        }

        
        public virtual void Update(float deltaTime)
        {
            //Before the actor is moved, update the direction it's facing
            UpdateFacing();

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
                (int)((Position.X + Forward.X) * 32),
                (int)((Position.Y + Forward.Y) * 32),
                Color.WHITE
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
