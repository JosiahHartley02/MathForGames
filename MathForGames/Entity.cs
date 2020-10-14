using System;
using System.Collections.Generic;
using System.Text;
using MathLib;

namespace MathForGames
{
    class Entity
    {
        private char _icon = '*';
        private Vector2 _position;
        private Vector2 _velocity;
        private ConsoleColor _color;



        public Entity(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
        {
            _icon = icon;
            _position = new Vector2(x, y);
            _velocity = new Vector2();
            _color = color;
        }
        public void Start()
        {

        }

        public void Update()
        {
            ConsoleKey keyPressed = Game.GetNextKey();

            switch (keyPressed)
            {
                case ConsoleKey.A:
                    _velocity.X = -1;
                    break;
                case ConsoleKey.D:
                    _velocity.X = 1;
                    break;
                case ConsoleKey.W:
                    _velocity.Y = -1;
                    break;
                case ConsoleKey.S:
                    _velocity.Y = 1;
                    break;
                default:
                    _velocity.X = 0;
                    _velocity.Y = 0;
                    break;
            }
            _position.X += _velocity.X;
            _position.Y += _velocity.Y;
            _position.X = Math.Clamp(_position.X, 0, Console.WindowWidth);
            _position.Y = Math.Clamp(_position.Y, 0, Console.WindowHeight);
        }
        public void Draw()
        {
            Console.SetCursorPosition((int)_position.X, (int)_position.Y);
            Console.Write(_icon);
        }
        public void End()
        {

        }
    }
}
