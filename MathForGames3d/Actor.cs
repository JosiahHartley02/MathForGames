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
        protected Vector3 _velocity = new Vector3(0, 0, 0);
        protected Actor _parent;
        protected Actor[] _children = new Actor[0];
        protected bool isChild = false;
        protected float _currentRadianRotation;
        protected float _rotationspeed = 0;
        protected Color defaultColor = Color.WHITE;
        public bool Started { get; private set; }
        public Actor(float x, float y, float z, Color color)
        {
            _localTransform = new Matrix4();
            LocalPosition = new Vector3(x, y, z);
            defaultColor = color;
        }
        public Vector3 Forward
        {
            get;
            set;
        }
        public Vector3 WorldPosition
        {
            get { return new Vector3(_globalTransform.m13, _globalTransform.m23, _globalTransform.m33); }
        }
        public Matrix4 GlobalTransform
        {
            get { return _globalTransform; }
        }
        public Vector3 LocalPosition
        {
            get
            {
                return new Vector3(_localTransform.m13, _localTransform.m23, _localTransform.m33);
            }
            set
            {
                _translation.m13 = value.X;
                _translation.m23 = value.Y;
                _translation.m33 = value.Z;
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
            _currentRadianRotation += angle;
            SetRotation(_currentRadianRotation);
        }
        public void SetTranslation(Vector3 position)
        {
            _translation = Matrix4.CreateTranslation(position);
        }
        public void SetRotation(float radians)
        {
            _rotation = Matrix4.CreateRotationX(radians);
            _currentRadianRotation = radians;
        }
        public void SetRotationSpeed(float speed)
        {
            _rotationspeed = speed;
        }
        public void SetScale(float x, float y, float z)
        {
            _scale = Matrix4.CreateScale(x, y, z);
        }
        private void UpdateTransform()
        {
            SetRotation(_rotationspeed + _currentRadianRotation);
            _localTransform = _translation * _rotation * _scale;
            if (_parent != null)
                _globalTransform = _parent._globalTransform * _localTransform;
            else
                _globalTransform = Game.GetCurrentScene().World * _localTransform;
        }
        public virtual void Start()
        {
            Started = true;
        }
        public virtual void Update(float deltaTime)
        {
            UpdateTransform();
            LocalPosition += _velocity * deltaTime;
        }
        public virtual void Draw()
        {
            Raylib.DrawSphere(new System.Numerics.Vector3(WorldPosition.X, WorldPosition.Y, WorldPosition.Z), 1, defaultColor);
        }
        public virtual void End()
        {
            Started = false;
        }
    }
}
