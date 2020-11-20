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
        Sprite _trail;
        Matrix3 lastLocation;
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
            _sprite = new Sprite("sprites/Tanks/tankRed.png");
            _trail = new Sprite("sprites/Tanks/tracksLarge.png");
        }

        public override void Update(float deltaTime)
        {
            //Gets the player's input to determine which direction the actor will move in on each axis 
            int Throttle = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_S))
                + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_W));

            float rotation = (Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_A))
                - Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_D)));

            //Set the actors current velocity to be the a vector with the direction found scaled by the speed 
            Velocity = new Vector2(_localTransform.m11 * Throttle, _localTransform.m21 * Throttle);
            Velocity = Velocity.Normalized * Speed; 
            /*Translate += _velocity * deltaTime;
            Rotation += counterclockwiseRotation * deltaTime;
            Scale = new Vector2(Scale.X += scale, Scale.Y += scale);*/
            Rotate(rotation * deltaTime);
            base.Update(deltaTime);
            _globalTransform = _localTransform;
            lastLocation = _globalTransform;
        }
        public override void Draw()
        {
            if(_sprite != null && isColliding == false)
            {
                _sprite.Draw(_localTransform * Matrix3.CreateRotation(1.5708f));
                if (_trail != null)
                    _trail.Draw(lastLocation * Matrix3.CreateRotation(1.5708f));
            }

            base.Draw();
        }
    }
}
