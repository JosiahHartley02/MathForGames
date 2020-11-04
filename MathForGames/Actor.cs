using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    //child enemy to player, so enemy mimics player

    /// <summary>
    /// This is the base class for all objects that will 
    /// be moved or interacted with in the game
    /// </summary>
    class Actor
    {
        protected char _icon = ' ';
        protected Vector2 _velocity;
        protected Matrix3 _globalTransform;
        protected Matrix3 _localTransform;
        protected Matrix3 _translation = new Matrix3();
        protected Matrix3 _rotation = new Matrix3();
        protected Matrix3 _scale = new Matrix3();
        protected float _currentRadianRotation;
        protected ConsoleColor _color;
        protected Color _rayColor;
        protected Actor _parent;
        protected Actor[] _children = new Actor[0];
        public bool Started { get; private set; }

        public Vector2 Forward
        {
            get 
            {
                return new Vector2(_localTransform.m11, _localTransform.m21);
            }
        }

        public Vector2 WorldPosition
        {
            get { return new Vector2(_globalTransform.m13, _globalTransform.m23); }
        }
        public Vector2 LocalPosition
        {
            get
            {
                return new Vector2(_localTransform.m13, _localTransform.m23);
            }
            set
            {
                _translation.m13 = value.X;
                _translation.m23 = value.Y;
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
        public void AddChild(Actor child)
        {
            Actor[] tempArray = new Actor[_children.Length + 1];
            for(int i = 0; i < _children.Length; i++)
            {
                tempArray[i] = _children[i];
            }
            tempArray[_children.Length] = child;
            _children = tempArray;
            child._parent = this;
        }
        public bool RemoveChild(Actor child)
        {
            if (child == null)
                return false; ;
            bool childRemoved = false;
            Actor[] tempArray = new Actor[_children.Length - 1];
            int j = 0;
            for(int i = 0;i < _children.Length; i++)
            {
                if(child != _children[i])
                {
                    tempArray[j] = _children[i];
                    j++;
                }
                else
                {
                    childRemoved = true;
                }
            }
            _children = tempArray;
            child._parent = null;
            return childRemoved;
        }
        public void SetTranslation(Vector2 position)
        {
            _translation.m13 = position.X;
            _translation.m23 = position.Y;
        }
        public void SetRotation(float radians)
        {
            _rotation.m11 = (float)Math.Cos(radians); _rotation.m12 = (float)Math.Sin(radians);
            _rotation.m21 = -(float)Math.Sin(radians); _rotation.m22 = (float)Math.Cos(radians);
            _currentRadianRotation = radians;
        }
        public void SetScale(float x, float y)
        {
            _scale.m11 = x;
            _scale.m22 = y;
        }
        private void UpdateTransform()
        {

            _localTransform = _translation *_rotation * _scale;
        }
       /* protected Vector2 Scale
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
        }*/


        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn</param>
        public Actor(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
        {
            _rayColor = Color.WHITE;
            _icon = icon;
            _localTransform = new Matrix3();
            LocalPosition = new Vector2(x, y);
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
            _localTransform = new Matrix3(0, 0, x * 32, 0, 0, y * 32, 0, 0, 1);
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
            UpdateTransform();

            //Before the actor is moved, update the direction it's facing
            /* UpdateFacing();*/

            //Increase position by the current velocity
            LocalPosition += _velocity * deltaTime;
        }

        public virtual void Draw()
        {
            //Draws the actor and a line indicating it facing to the raylib window.
            //Scaled to match console movement
            Raylib.DrawText(_icon.ToString(), (int)(LocalPosition.X * 32), (int)(LocalPosition.Y * 32), 32, _rayColor);
            Raylib.DrawLine(
                (int)(LocalPosition.X * 32),
                (int)(LocalPosition.Y * 32),
                (int)((LocalPosition.X + _rotation.m11) * 32),
                (int)((LocalPosition.Y + _rotation.m21) * 32),
                Color.BLUE
            );
            Raylib.DrawLine(
                (int)(LocalPosition.X * 32),
                (int)(LocalPosition.Y * 32),
                (int)((LocalPosition.X - _rotation.m12) * 32),
                (int)((LocalPosition.Y - _rotation.m22) * 32),
                Color.RED
                );

            //Changes the color of the console text to be this actors color
            Console.ForegroundColor = _color;

            //Only draws the actor on the console if it is within the bounds of the window
            if(LocalPosition.X >= 0 && LocalPosition.X < Console.WindowWidth 
                && LocalPosition.Y >= 0  && LocalPosition.Y < Console.WindowHeight)
            {
                Console.SetCursorPosition((int)LocalPosition.X, (int)LocalPosition.Y);
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
