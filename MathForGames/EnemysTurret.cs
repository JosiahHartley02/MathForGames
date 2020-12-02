using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class EnemysTurret : Actor
    {
        public EnemysTurret(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
    : base(x, y, rayColor, icon, color)
        {
            _sprite = new Sprite("sprites/Tanks/barrelBlue.png");
            _collisionRadius = 0;
            Collidable = false;
        }


        public override void Start()
        {
            base.Start();
        }
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }
        public override void Draw()
        {
            if (_sprite != null && IsVisible != false)
            {
                _sprite.Draw(_globalTransform * Matrix3.CreateRotation(-1.5708f));
            }
            base.Draw();
            Raylib.DrawLine(
(int)(WorldPosition.X * 32),
(int)(WorldPosition.Y * 32),
(int)((WorldPosition.X + Forward.X) * 32),
(int)((WorldPosition.Y + Forward.Y) * 32),
Color.DARKGREEN
);
        }
        public override void End()
        {
            base.End();
        }

    }
}
