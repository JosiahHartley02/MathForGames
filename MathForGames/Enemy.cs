using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    class Enemy : Actor
    {
        private Actor _target;
        private Color _alertColor;
        public Actor Target
        {
            get { return _target; }
            set { _target = value; }
        }

        public Enemy(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, icon, color)
        {

        }

        public Enemy(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
            _alertColor = Color.RED;            
        }

        public bool CheckTargetInSight(float maxViewingAngle, float maxViewingDistance)
        {
            if (Target == null)
                return false;

            Vector2 direction = Target.Position - Position;
            float distance = (direction.Magnitude);
            float angle = (float)Math.Acos(Vector2.DotProduct(Forward, direction.Normalized));
            if (angle <= maxViewingAngle && distance <= maxViewingDistance)
                return true;

            return false;
        }

        public override void Update(float deltaTime)
        {
            if(CheckTargetInSight(0.261799f,7))
            {
                _rayColor = Color.RED;
            }
            else
            {
                _rayColor = Color.BLUE;
            }
            base.Update(deltaTime);
        }
        public override void Draw()
        {
            Raylib.DrawLine(
                (int)(Position.X * 32 + 16),
                (int)(Position.Y * 32 + 16),
                (int)(Position.X * 32 + 1000000),
                (int)(Position.Y *32 - 261799),
                Color.PINK
                );
            Raylib.DrawLine(
                (int)(Position.X * 32 + 16),
                (int)(Position.Y * 32 + 16),
                (int)(Position.X * 32 + 1000000),
                (int)(Position.Y * 32 + 261799),
                Color.PINK
                );
            base.Draw();
        }
    }
}
