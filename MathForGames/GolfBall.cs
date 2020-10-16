using System;
using System.Collections.Generic;
using System.Text;

namespace MathForGames
{
    class GolfBall : Entity
    {
        private int rolls;
        public GolfBall(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White) :base(x,y,icon,color)
        {

        }
        public override void Update()
        {            
            if (!_visible) { _color = ConsoleColor.Black; }
            else { _color = ConsoleColor.White; }
            base.Update();
            Veloctity.X = 0;
            Veloctity.Y = 0;
        }
    }
}
