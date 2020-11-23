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
            isVisible = false;
        }
        public Projectile(Matrix3 globalTransform, string path)
            : base(globalTransform, path)
        {
            _sprite = new Sprite(path);
            _globalTransform = globalTransform;
            isVisible = false;
        }
        public override void Start()
        {
            base.Start();
        }
        public override void Update(float deltaTime)
        {
            UpdateTransform();
            if (isVisible)
            { LocalPosition += Velocity.Normalized * deltaTime; }
            _globalTransform = _localTransform;
        }
        public override void Draw()
        {
            if(isVisible == true) { _sprite.Draw(_globalTransform); }            
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
