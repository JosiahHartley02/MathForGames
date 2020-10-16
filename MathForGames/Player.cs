using System;
using System.Collections.Generic;
using System.Text;
using MathLib;

namespace MathForGames
{
    class Player : Entity
    {
        public int direction { get; set; }
        public float _power = 1;
        public Player(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White) : base(x, y, icon,color)
        {
            direction = 1;
        }

        public override void Update()
        {            
            ConsoleKey keyPressed = Game.GetNextKey();
            switch (keyPressed)
            {
                case ConsoleKey.A:
                    if (direction != 3)
                    {
                        _icon = '◄';
                        direction = 3;
                    }
                    else if (direction == 3)
                    {
                        _velocity.X = -1;
                    }
                    break;
                case ConsoleKey.D:
                    if (direction != 1)
                    {
                        direction = 1;
                        _icon = '►';
                    }
                    else if (direction == 1)
                    {
                        _velocity.X = 1;
                    }  
                    break;
                case ConsoleKey.W:
                    if (direction != 4)
                    {
                        direction = 4;
                        _icon = '▲';
                    }
                    else if ( direction == 4)
                    {
                        _velocity.Y = -1;
                    }                            
                    break;
                case ConsoleKey.S:
                    if (direction != 2)
                    {
                        direction = 2;
                            _icon = '▼';
                    }
                    else if (direction == 2)
                    {
                        _velocity.Y = 1;
                    }  
                    break;
                case ConsoleKey.B:
                    Game.PlaceGolfBall(Game.player1);
                    break;
                case ConsoleKey.D1:
                    _power = 1;
                    break;
                case ConsoleKey.D2:
                    _power = 2;
                    break;
                case ConsoleKey.D3:
                    _power = 4;
                    break;
                case ConsoleKey.D4:
                    _power = 8;
                    break;
                case ConsoleKey.D5:
                    _power = 16;
                    break;
                case ConsoleKey.V:
                    Game.HitGolfBall();
                    break;
                default:
                    _velocity.X = 0;
                    _velocity.Y = 0;
                    break;
            }
            base.Update();
        }
    }
}
