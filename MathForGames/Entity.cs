using System;
using System.Collections.Generic;
using System.Text;
using MathLib;

namespace MathForGames
{
    class Entity                            //A char on the screen that holds information related to moving or performing a task
    {
        protected char _icon = '*';
        protected Vector2 _position;
        protected Vector2 _velocity;
        protected ConsoleColor _color;
        public bool _visible { get; set; }


        public Vector2 Position                               //Gets the x and y values from the postion vector and can set the postion to an x and y value
        {
            get
            {
                return _position; 
            }
            set
            {
                _position = value;
            }
        } 
        public Vector2 Veloctity                           //Gets the x value and y value from the entities velocity vector and can set the velocity to and x and y value
        {
            get
            {
                return _velocity;
            }
            set
            {
                _velocity = value;
            }
        }
        public Entity(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)               //creates an entity at (x,y) that looks like 'icon' with the consolecolor for the icon
        {
            _icon = icon;
            _position = new Vector2(x, y);
            _velocity = new Vector2();
            _color = color;
        }
/*        public virtual void HitObject()
        {
            Veloctity.X = -Veloctity.X;
            Veloctity.Y = -Veloctity.Y;
        }*/
        public virtual void Start()
        {

        }

        public virtual void Update()
        {
            _position += _velocity;
            _position += _velocity;
            _position.X = Math.Clamp(_position.X, 0, Console.WindowWidth - 1);
            _position.Y = Math.Clamp(_position.Y, 0, Console.WindowHeight - 1);
        }
        public virtual void Draw()
        {
            Console.ForegroundColor = _color;
            Console.SetCursorPosition((int)_position.X, (int)_position.Y);
            Console.Write(_icon);
            Console.ForegroundColor = Game.DefaultColor;
        }
        public virtual void End()
        {

        }
    }
}
