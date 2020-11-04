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
        Sprite _sprite;
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
            _sprite = new Sprite("sprites/player.png");
            
        }

        public override void Update(float deltaTime)
        {
            //Gets the player's input to determine which direction the actor will move in on each axis 
            int xDirection = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_A))
                + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_D));
            int yDirection = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_W))
                + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_S));
            int scale = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_COMMA))
                + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_PERIOD));
            float rotation = (Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_LEFT))
                - Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_RIGHT))) * 4;

            //Set the actors current velocity to be the a vector with the direction found scaled by the speed
            Velocity = new Vector2(xDirection, yDirection);
            Velocity = Velocity.Normalized * Speed;
            /*Translate += _velocity * deltaTime;
            Rotation += counterclockwiseRotation * deltaTime;
            Scale = new Vector2(Scale.X += scale, Scale.Y += scale);*/
            SetScale(_scale.m11 += scale, _scale.m22 += scale);
            Rotate(4 * deltaTime);
            base.Update(deltaTime);
            _globalTransform = _localTransform;
        }
        public override void Draw()
        {
            if(_sprite != null)
            {
                _sprite.Draw(_localTransform);
            }

            base.Draw();
        }
    }
}
