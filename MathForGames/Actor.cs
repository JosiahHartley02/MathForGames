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
        private Vector2 _velocity = new Vector2();
        private Vector2 acceleration = new Vector2();
        private float _maxSpeed = 5;
        protected Matrix3 _globalTransform = new Matrix3();
        protected Matrix3 _localTransform = new Matrix3();
        protected Matrix3 _translation = new Matrix3();
        protected Matrix3 _rotation = new Matrix3();
        protected Matrix3 _scale = new Matrix3();
        protected float _currentRadianRotation;
        protected float _rotationspeed = 0;
        protected ConsoleColor _color;
        protected Color _rayColor;
        protected Actor _parent;
        protected Actor[] _children = new Actor[0];
        protected bool isChild = false;
        protected Actor _Tank;
        protected Projectile[] _projectiles = new Projectile[0];
        protected bool isBullet = false;
        protected bool isVisible = true;
        protected bool _isColliding = false;
        protected float _collisionRadius = 1;
        protected Sprite _sprite;
        public bool Started { get; private set; }

        public float CollisionRadius
        { get { return _collisionRadius; } set { _collisionRadius = value; } }
        public bool isColliding
        {
            get { return _isColliding; }
            set { _isColliding = value; }
        }
        public Vector2 Forward
        {
            get 
            {
                return new Vector2(_localTransform.m11, _localTransform.m21);
            }
            set
            {
                Vector2 lookPosition = LocalPosition + value.Normalized;
                LookAt(lookPosition);
            }
        }

        public Vector2 WorldPosition
        {
            get { return new Vector2(_globalTransform.m13, _globalTransform.m23); }
        }
        public Matrix3 GlobalTransform
        {
            get { return _globalTransform; }
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
        { get{return _velocity;} set{_velocity = value;} }

        protected Vector2 Acceleration { get => acceleration; set => acceleration = value; }
        public float MaxSpeed { get => _maxSpeed; set => _maxSpeed = value; }

        public void Rotate(float angle)
        {
            _currentRadianRotation += angle;
            SetRotation(_currentRadianRotation);
        }
        public void LookAt(Vector2 position)
        {
            Vector2 direction = (position - LocalPosition).Normalized; // finds the vector facing towards point we want to look at
            float dotProd = Vector2.DotProduct(Forward, direction);    //gets  Forward•Direction 
            if (Math.Abs(dotProd) > 1)                             //sometimes dotprod math is bad due to double
                return;
            float angle = (float)Math.Acos(dotProd);               //gets the angle of the dotproduct
            Vector2 perp = new Vector2(direction.Y, -direction.X);  //creats a vector perpendicular to direction we want to look
            float perpDot = Vector2.DotProduct(perp, Forward);      // gets Perp•Forward
            if (perpDot != 0)                                       //if 0 then already facing
                angle *= -perpDot / Math.Abs(perpDot);              // multiply angle by the negative inverse of perpdot
            Rotate(angle);                                          //Rotates by the angle needed to look at the target
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
            this.isChild = true;
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
            this.isChild = false;
            return childRemoved;
        }
        public void AddAmmo(Projectile bullet)
        {
            Projectile[] tempArray = new Projectile[_projectiles.Length + 1];
            for (int i = 0; i < _projectiles.Length; i++)
            {
                tempArray[i] = _projectiles[i];
            }
            tempArray[_projectiles.Length] = bullet;
            _projectiles = tempArray;
            bullet._Tank = this;
            this.isBullet = true;
        }
        public bool RemoveAmmo(Projectile bullet)
        {
            if (bullet == null)
                return false; ;
            bool bulletRemoved = false;
            Projectile[] tempArray = new Projectile[_projectiles.Length - 1];
            int j = 0;
            for (int i = 0; i < _projectiles.Length; i++)
            {
                if (bullet != _projectiles[i])
                {
                    tempArray[j] = _projectiles[i];
                    j++;
                }
                else
                {
                    bulletRemoved = true;
                }
            }
            _projectiles = tempArray;
            bullet._parent = null;
            this.isBullet = false;
            return bulletRemoved;
        }
        public void SetTranslation(Vector2 position)
        {
            _translation = Matrix3.CreateTranslation(position);
        }
        public void SetRotation(float radians)
        {
            _rotation = Matrix3.CreateRotation(radians);
            _currentRadianRotation = radians;
        }
        public void SetRotationSpeed(float speed)
        {
            _rotationspeed = speed;
        }
        public void SetScale(float x, float y)
        {
            _scale = Matrix3.CreateScale(x,y);
        }
        private void UpdateTransform()
        {
            SetRotation(_rotationspeed + _currentRadianRotation);
            _localTransform = _translation *_rotation * _scale;
        }

        //to make this easier and more balanced, player and other only get one bullet bullet is "destroyed" when collision is true
        protected void LaunchProjectile(Projectile bullet)
        {
            if (bullet.isVisible == false)
            {
                bullet._rotation = bullet._Tank._rotation * bullet._Tank._parent._rotation;
                bullet.SetTranslation(bullet._Tank.WorldPosition);
                bullet.Velocity = new Vector2(1, 0);
                bullet.isVisible = true;
            }

        }

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
        public Actor()
        {
        }
        public Actor(Matrix3 globalTransform, string path)
        {

        }

        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="rayColor">The color of the symbol that will appear when drawn to raylib</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn to the console</param>
        public Actor(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : this(x,y,icon,color)
        {
            _rayColor = Color.WHITE;
            _icon = icon;
            _localTransform = new Matrix3();
            LocalPosition = new Vector2(x, y);
            _velocity = new Vector2();
            _color = color;
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
            Acceleration += new Vector2(-Velocity.X, -Velocity.Y) * deltaTime;

            Velocity += Acceleration;

            if (Velocity.Magnitude > MaxSpeed)
                Velocity = Velocity.Normalized * MaxSpeed;

            LocalPosition += _velocity * deltaTime;

            if (_parent != null)
                _globalTransform = _parent._globalTransform * _localTransform;
            else
                _globalTransform = Game.GetCurrentScene().World * _localTransform;
        }

        public virtual void Draw()
        {
            //Draws the actor and a line indicating it facing to the raylib window.
            //Scaled to match console movement
            Raylib.DrawText(_icon.ToString(), (int)(WorldPosition.X * 32), (int)(WorldPosition.Y * 32), 32, _rayColor);
            Raylib.DrawLine(
                (int)(WorldPosition.X * 32),
                (int)(WorldPosition.Y * 32),
                (int)((WorldPosition.X + _globalTransform.m11) * 32),
                (int)((WorldPosition.Y + _globalTransform.m21) * 32),
                Color.BLUE
            );
            Raylib.DrawLine(
                (int)(WorldPosition.X * 32),
                (int)(WorldPosition.Y * 32),
                (int)((WorldPosition.X - _globalTransform.m12) * 32),
                (int)((WorldPosition.Y - _globalTransform.m22) * 32),
                Color.RED
                );

            //Changes the color of the console text to be this actors color
            Console.ForegroundColor = _color;

            //Only draws the actor on the console if it is within the bounds of the window
            if(WorldPosition.X >= 0 && WorldPosition.X < Console.WindowWidth 
                && WorldPosition.Y >= 0  && WorldPosition.Y < Console.WindowHeight)
            {
                Console.SetCursorPosition((int)WorldPosition.X, (int)WorldPosition.Y);
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
