using System;
using System.Collections.Generic;
using System.Text;
using MathLib;

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
        public void HitGolfBall(Player player)
        {
            switch (player.direction)
            {
                case 1:
                    Veloctity.X += player._power;
                    break;
                case 2:
                    Veloctity.Y += player._power;
                    break;
                case 3:
                    Veloctity.X -= player._power;
                    break;
                case 4:
                    Veloctity.Y -= player._power;
                    break;
            }
        }
        public void PlaceGolfBall(Player player,Scene scene)
        {
            _visible = true;
            float x = player.Position.X;
            float y = player.Position.Y;
            if (player.direction == 1)
            {
                if (scene.CheckPositionAvailable(x += 1, y))
                { MoveBall(player, 1, 0); }
            }
            if (player.direction == 2)
            {
                if (scene.CheckPositionAvailable(x, y += 1))
                { MoveBall(player, 0, 1); }
            }
            if (player.direction == 3)
            {
                if (scene.CheckPositionAvailable(x -= 1, y))
                { MoveBall(player, -1, 0); }
            }
            if (player.direction == 4)
            {
                if (scene.CheckPositionAvailable(x, y - 1))
                { MoveBall(player, 0, -1); }
            }
        }
        void MoveBall(Player player, float xValIncrease, float yValIncrease)
        {
            Position.X = player.Position.X + xValIncrease;
            Position.Y = player.Position.Y + yValIncrease;
        }
    }
}
