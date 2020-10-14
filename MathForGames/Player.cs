using System;
using System.Collections.Generic;
using System.Text;
using MathLib;

namespace MathForGames
{
    class Player : Entity
    {
        bool _faceRight;
        bool _faceDown;
        float _power = 1;
        public Player(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White) : base(x, y, icon,color)
        {

        }

        public override void Update()
        {            
            ConsoleKey keyPressed = Game.GetNextKey();

            switch (keyPressed)
            {
                case ConsoleKey.A:
                    _velocity.X = -1;
                    _faceRight = false;
                    break;
                case ConsoleKey.D:
                    _velocity.X = 1;
                    _faceRight = true;
                    break;
                case ConsoleKey.W:
                    _velocity.Y = -1;
                    _faceDown = false;
                    break;
                case ConsoleKey.S:
                    _velocity.Y = 1;
                    _faceDown = true;
                    break;
                case ConsoleKey.Q:
                    
                default:
                    _velocity.X = 0;
                    _velocity.Y = 0;
                    break;
            }
            base.Update();
        }
    }
}
