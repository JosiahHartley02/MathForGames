using System;
using System.Collections.Generic;
using System.Text;

namespace MathForGames
{
    class GolfBall : Entity
    {
        bool _hit;
        public GolfBall(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White) :base(x,y,icon,color)
        {

        }
        public override void Update()
        {
            if(_hit)
            {

            }
            else
            {

            }
            base.Update();
        }
    }
}
