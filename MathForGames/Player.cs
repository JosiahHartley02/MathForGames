using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    /// <summary>
    /// An actor that moves based on input given by the user
    /// </summary>
    class Player : Actor
    {
        private float _speed = 1;

        public float Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
            }
        }

        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn</param>
        public Player(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, icon, color)
        {
            
        }

        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="rayColor">The color of the symbol that will appear when drawn to raylib</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn to the console</param>
        public Player(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
            _transform = new Matrix3(1, 0, x, 0, 1, y, 0, 0, 1);
            _translate = new Matrix3(1,0,0,0,1,0,0,0,1);
            _rotation = new Matrix3(1,0,0,0,1,0,0,0,1);
            _scale = new Matrix3(1,0,0,0,1,0,0,0,1);
        }

        public override void Update(float deltaTime)
        {
            //Gets the player's input to determine which direction the actor will move in on each axis 
            int xDirection = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_A))
                + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_D));
            int yDirection = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_W))
                + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_S));
            int yScale = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_UP))
                + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_DOWN));
            int xScale = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_RIGHT))
                + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_LEFT));
            int rotation = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_APOSTROPHE))
                + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_PERIOD));

            //Set the actors current velocity to be the a vector with the direction found scaled by the speed
            Translate = new Matrix3(0,0,(xDirection / (float)Math.Sqrt(Math.Pow(xDirection, 2)
                + (float)Math.Pow(yDirection, 2)) * Speed),0,0, (yDirection
                / (float)Math.Sqrt(Math.Pow(xDirection, 2)
                + (float)Math.Pow(yDirection, 2))
                * Speed), 0,0,0);
            Scale = new Matrix3(xScale, 0, 0, 0, yScale, 0, 0, 0, 0);
            Rotate = new Matrix3(Rotate.m11 + rotation, Rotate.m12 + rotation, 0, Rotate.m21 + rotation, Rotate.m22 + rotation, 0, 0, 0, 0);
            _transform *= _translate * _rotation * _scale;
            
            Velocity = new Vector2(xDirection, yDirection);
            Velocity = Velocity.Normalized * Speed;
            Position = new Vector2(_transform.m13, _transform.m23);
            base.Update(deltaTime);

            
        }
        
        public override void Draw()
        {
            Position = new Vector2(_transform.m13, _transform.m23);
            base.Draw();
        }
    }
}
