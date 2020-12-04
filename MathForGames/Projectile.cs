using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    class Projectile : Actor
    {
        private float _existence;
        private float _lifespan = 3;
        public float Existence { get { return _existence; } set { _existence = value; } }
        public Projectile(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
            _sprite = new Sprite("sprites/Tanks/tankBlue.png");
            _isVisible = false;
            _collisionRadius = 5;
            _collidable = true;
        }
        public Projectile(Matrix3 localTransform, string path) : base(localTransform,path)
        {
            _localTransform = localTransform;
            _sprite = new Sprite(path);
        }
        public override void Start()
        {
            Collidable = true;
            _isVisible = false;
            base.Start();
        }
        public override void Update(float deltaTime)
        {
            if(_existence > _lifespan)
            { _isVisible = false; }
            UpdateTransform();
            
            //Toggles Between Visible Physical Characteristics And Invisible Numerical Characteristics
            if (_isVisible)
            { LocalPosition += Velocity.Normalized * deltaTime * 10; _collidable = true; }
            else { _collidable = false; _isColliding = false; }

            //Applies Current Changes To Global Transform
            _globalTransform = _localTransform;

            //Creates Boucing Off Walls Effect By Testing Global Position
            if (WorldPosition.X < 0f) { Velocity.X = Math.Abs(Velocity.X);}
            if (WorldPosition.X > 32f) { Velocity.X = -Math.Abs(Velocity.X);}
            if (WorldPosition.Y < 0) { Velocity.Y = Math.Abs(Velocity.Y);}
            if (WorldPosition.Y > 24) { Velocity.Y = -Math.Abs(Velocity.Y);}
            //Tests For Collision
            LookAt(WorldPosition + Velocity);
            Game.GetCurrentScene().TestForCollision(this);
            if (isColliding) { _isVisible = false; }
            if(IsVisible)
            { _existence += deltaTime; }
        }
        public override void Draw()
        {
            if (_isVisible == true) { _sprite.Draw(_globalTransform); }            
            base.Draw();
        }
        public override void End()
        {
            base.End();
        }
        private void UpdateTransform()
        {
            _localTransform = _translation * _rotation * _scale;
        }
    }
}
