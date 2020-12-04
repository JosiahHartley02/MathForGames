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
            LookAt(Target);
            Vector2 displacement = new Vector2(Target.WorldPosition.X - WorldPosition.X, Target.WorldPosition.Y - WorldPosition.Y);
            if (_projectiles[0].IsVisible == false)
            {
                if (displacement.Magnitude < 7.5f)
                    LaunchProjectile(_projectiles[0]);
            }
            base.Update(deltaTime);
        }
        public override void Draw()
        {
            if (_sprite != null && IsVisible != false)
            {
                _sprite.Draw(_globalTransform);
            }
            base.Draw();
        }
        public override void End()
        {
            base.End();
        }

    }
}
