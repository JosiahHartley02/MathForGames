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
        protected float _collisionRadius = 1;

        protected ConsoleColor _color;
        protected Color _rayColor;
        protected Actor _parent;
        public Actor Parent { get { return _parent; } }
        protected Actor[] _children = new Actor[0];
        public Actor[] Children { get { return _children; } }
        protected bool isChild = false;
        public bool IsChild { get { return isChild; } }
        protected bool _canCollide = false;
        public bool CanCollide { get { return _canCollide; } set { _canCollide = value; } }
        protected Actor _Tank;
        public Actor Tank { get { return _Tank; } }
        protected Projectile[] _projectiles = new Projectile[0];
        protected bool _isBullet = false;
        protected bool _isVisible = true;
        protected bool _isColliding = false;
        protected bool _collidable = false;
        public bool Collidable { get { return _collidable; } set { _collidable = value; } }
        protected Sprite _sprite;
        public bool Started { get; private set; }

        public float CollisionRadius
        { get { return _collisionRadius; } set { _collisionRadius = value; } }
        public bool IsBullet
        { get { return _isBullet; } }
        public bool IsVisible { get { return _isVisible; } set { _isVisible = value; } }
        public bool isColliding
        {
            get { return _isColliding; }
            set { _isColliding = value; }
        }

        private Actor _target;
        public Actor Target { get { return _target; } }
        public void AddTarget(Actor target)
        {
            if (_target == null)
                _target = target;
        }
        public void RemoveTarget()
        {
            _target = null;
        }

        public Vector2 Forward
        {
            get 
            {
                return new Vector2(_globalTransform.m11, _globalTransform.m21);
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
            //Find the direction that the actor should look in
            Vector2 direction = (position - WorldPosition).Normalized;

            //Use the dotproduct to find the angle the actor needs to rotate
            float dotProd = Vector2.DotProduct(Forward, direction);
            if (Math.Abs(dotProd) > 1)
                return;
            float angle = (float)Math.Acos(dotProd);

            //Find a perpindicular vector to the direction
            Vector2 perp = new Vector2(direction.Y, -direction.X);

            //Find the dot product of the perpindicular vector and the current forward
            float perpDot = Vector2.DotProduct(perp, Forward);

            //If the result isn't 0, use it to change the sign of the angle to be either positive or negative
            if (perpDot != 0)
                angle *= -perpDot / Math.Abs(perpDot);

            Rotate(angle);
        }
        public void LookAt(Actor Target)
        {
            
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
            this._isBullet = true;
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
            this._isBullet = false;
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
            if (bullet.IsVisible == false)
            {
                bullet._isVisible = true;
                bullet._rotation = bullet._Tank._rotation * bullet._Tank._parent._rotation;
                bullet.SetTranslation(new Vector2(bullet._Tank._globalTransform.m13 + (bullet._Tank._globalTransform.m11 * 0.65f), bullet._Tank._globalTransform.m23 + (bullet._Tank._globalTransform.m21 * 0.65f)));
                bullet.Velocity = new Vector2(bullet._Tank._globalTransform.m11, bullet._Tank._globalTransform.m21);
            }

        }

        public Actor(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
        {
            _rayColor = Color.WHITE;
            _icon = icon;
            _localTransform = new Matrix3();
            LocalPosition = new Vector2(x, y);
            _velocity = new Vector2();
            _color = color;
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

            //Modifys Acceleration To Constantly Be Subtracting Velocity To Replicate Friction
            Acceleration += new Vector2(-Velocity.X, -Velocity.Y) * deltaTime;
            //Adds Acceleration To Velocity After The Friction Has Been Applied
            Velocity += Acceleration;
            //Tests For Velocity Value, If > MaxSpeed Then = MaxSpeed
            if (Velocity.Magnitude > MaxSpeed)
                Velocity = Velocity.Normalized * MaxSpeed;

            LocalPosition += _velocity * deltaTime;

            if (_parent != null)
                _globalTransform = _parent._globalTransform * _localTransform;
            else
                _globalTransform = Game.GetCurrentScene().World * _localTransform;
            _isColliding = false;
            Game.GetCurrentScene().TestForCollision(this);

            //Creates Boucing Off Walls Effect By Testing Global Position
            if (WorldPosition.X < 0.75f) { Velocity.X = Math.Abs(Velocity.X); }
            if (WorldPosition.X > 31.25f) { Velocity.X = -Math.Abs(Velocity.X); }
            if (WorldPosition.Y < 0.7f) { Velocity.Y = Math.Abs(Velocity.Y); }
            if (WorldPosition.Y > 23.25f) { Velocity.Y = -Math.Abs(Velocity.Y); }
        }

        public virtual void Draw()
        {
            

            if (Game.DebugVisual == false)
            { return; }
            //Draws the actor and a line indicating it facing to the raylib window.
            //Scaled to match console movement
            if (isColliding == true)
            {
                Raylib.DrawCircle((int)(WorldPosition.X * 32), (int)(WorldPosition.Y * 32), CollisionRadius, Color.RED);
                Raylib.DrawCircle((int)(WorldPosition.X * 32), (int)(WorldPosition.Y * 32), CollisionRadius - 2, Color.BLACK);
            }
            else
            {
                Raylib.DrawCircle((int)(WorldPosition.X * 32), (int)(WorldPosition.Y * 32), CollisionRadius, Color.WHITE);
                Raylib.DrawCircle((int)(WorldPosition.X * 32), (int)(WorldPosition.Y * 32), CollisionRadius - 2, Color.BLACK);
            }
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
            Raylib.DrawLine(
    (int)(WorldPosition.X * 32),
    (int)(WorldPosition.Y * 32),
    (int)((WorldPosition.X + Velocity.X) * 32),
    (int)((WorldPosition.Y + Velocity.Y) * 32),
    Color.DARKGREEN
    );
            Raylib.DrawLine(
(int)(WorldPosition.X * 32),
(int)(WorldPosition.Y * 32),
(int)((WorldPosition.X + Forward.X) * 32),
(int)((WorldPosition.Y + Forward.Y) * 32),
Color.DARKPURPLE
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
