using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames3D
{
    class Actor
    {
        protected Matrix4 _globalTransform = new Matrix4();
        protected Matrix4 _localTransform = new Matrix4();
        protected Matrix4 _translation = new Matrix4();
        protected Matrix4 _rotation = new Matrix4();
        protected Matrix4 _scale = new Matrix4();
        protected float Scale { get; set; }
        protected Vector3 _velocity = new Vector3(0, 0, 0);
        protected Actor _parent;
        protected Actor[] _children = new Actor[0];
        protected bool isChild = false;
        protected float _currentRadianRotationY;
        public float RadianRotationY = 0;
        protected float _rotationspeedX = 0;
        protected Color defaultColor = Color.WHITE;
        private float _heatlh;
        public float Health { get; set; }
        private float speed;

        public bool Started { get; private set; }
        public Actor(float x, float y, float z, Color color)
        {
            _localTransform = new Matrix4();
            LocalPosition = new Vector3(x, y, z);
            defaultColor = color;

        }
        public virtual void Start()
        {
            Started = true;
        }
        public virtual void Update(float deltaTime)
        {
            UpdateTransform();
            _rotationspeedX = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_R)) * 0.02f;
            _currentRadianRotationY += _rotationspeedX;
            LocalPosition += _velocity * deltaTime;
        }
        public virtual void Draw()
        {

        }

        public virtual void End()
        {
            Started = false;
        }
        public Vector3 Forward
        {
            get;
            set;
        }
        public Vector3 WorldPosition
        {
            get { return new Vector3(_globalTransform.m14, _globalTransform.m24, _globalTransform.m34); }
        }
        public Matrix4 GlobalTransform
        {
            get { return _globalTransform; }
        }
        public Vector3 LocalPosition
        {
            get
            {
                return new Vector3(_localTransform.m14, _localTransform.m24, _localTransform.m34);
            }
            set
            {
                _translation.m14 = value.X;
                _translation.m24 = value.Y;
                _translation.m34 = value.Z;
            }
        }
        public Vector3 Velocity
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
            for (int i = 0; i < _children.Length; i++)
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
            for (int i = 0; i < _children.Length; i++)
            {
                if (child != _children[i])
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
        public void Rotate(float angle)
        {
            _currentRadianRotationY += angle;
            SetRotation(_currentRadianRotationY);
        }
        public void SetTranslation(Vector3 position)
        {
            _translation = Matrix4.CreateTranslation(position);
        }
        public void SetTranslation(float x, float y, float z)
        {
            _translation.m14 = x; _translation.m24 = y; _translation.m34 = z;
        }
        public void SetPosition(float x, float y, float z)
        {
            _localTransform.m14 = x;
            _localTransform.m24 = y;
            _localTransform.m34 = z;
        }
        public void SetRotation(float radians)
        {
            _rotation = Matrix4.CreateRotationX(radians);
            _currentRadianRotationY = radians;
        }
        public void SetRotationSpeed(float speed)
        {
            _rotationspeedX = speed;
        }
        public void SetScale(float x, float y, float z)
        {
            _scale = Matrix4.CreateScale(x, y, z);
        }
        public void SetScale(float scale)
        {
            _scale = Matrix4.CreateScale(scale, scale, scale);
            Scale = scale;
        }
        private void UpdateTransform()
        {
            _rotation = Matrix4.CreateRotationY(_rotationspeedX += _currentRadianRotationY);
            _localTransform = _translation * _rotation * _scale;
            if (_parent != null)
            { _globalTransform = _parent._globalTransform * _localTransform; }
            else
            { _globalTransform = Game.GetCurrentScene().World * _localTransform; }
        }

    }
}
