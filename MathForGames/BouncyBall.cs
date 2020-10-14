using System;
using System.Collections.Generic;
using System.Text;

namespace MathForGames
{
    class BouncyBall : Entity
    {
        private bool _goingRight;
        private bool _goingDown;
        public BouncyBall(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White) :base(x,y,icon,color)
        {
            _velocity.X = 2;
            _velocity.Y = 1;
            _goingRight = true;
            _goingDown = true;
        }
        public override void Update()
        {
            if (Position.X == 0) { _goingRight = true; }
            if (Position.X == Console.WindowWidth - 1) { _goingRight = false; }
            if (Position.Y == 0) { _goingDown = true; }
            if (Position.Y == Console.WindowHeight - 1) { _goingDown = false; }
            if (_goingRight) { _velocity.X = 2; }
            if (!_goingRight) { _velocity.X = -2; }
            if (_goingDown) { _velocity.Y = 1; }
            if (!_goingDown) { _velocity.Y = -1; }
            base.Update();
        }
    }
}
