using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    class Projectile : Actor
    {
        public Projectile(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
            _sprite = new Sprite("sprites/Tanks/tankBlue.png");
            _isVisible = false;
            _collisionRadius = 5;
            _collidable = true;
        }
        public Projectile(Matrix3 globalTransform, string path)
            : base(globalTransform, path)
        {
            _sprite = new Sprite(path);
            _globalTransform = globalTransform;
            _isVisible = false;
            _collisionRadius = 5;
            _collidable = true;
        }
        public override void Start()
        {
            _canCollide = true;
            base.Start();
        }
        public override void Update(float deltaTime)
        {
            _collidable = false;
            UpdateTransform();
            if (_isVisible)
            { LocalPosition += Velocity.Normalized * deltaTime * 7; _collidable = true; }
            else { _isColliding = false; }
            _globalTransform = _localTransform;
            Game.GetCurrentScene().TestForCollision(this);
            if (isColliding)
            { _isVisible = false; }
            
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
