using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    class Projectile : Actor
    {
        Sprite _sprite;
        public Projectile(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
            _sprite = new Sprite("sprites/Bullet/bulletRed.png");
        }
        public Projectile(Matrix3 globalTransform, string path)
            : base(globalTransform, path)
        {
            _sprite = new Sprite(path);
            _globalTransform = globalTransform;
        }
        public override void Start()
        {
            base.Start();
        }
        public override void Update(float deltaTime)
        {
            Matrix3.CreateTranslation(new Vector2(_localTransform.m11, _localTransform.m21));
            base.Update(deltaTime);
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
    }
}
