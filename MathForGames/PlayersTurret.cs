using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    class PlayersTurret : Actor
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
        /// <param name="rayColor">The color of the symbol that will appear when drawn to raylib</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn to the console</param>
        public PlayersTurret(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
            _sprite = new Sprite("sprites/Tanks/barrelRed.png");

        }

        public override void Update(float deltaTime)
        {
            _rotationspeed = (Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_Q))
                - Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_E))) * .1f;
            bool fire = Game.GetKeyDown((int)KeyboardKey.KEY_SPACE);
            if (fire == true)
            { LaunchProjectile(); }
            base.Update(deltaTime);

        }
        public override void Draw()
        {
            if (_sprite != null && isColliding == false)
            {
                _sprite.Draw(_globalTransform * Matrix3.CreateRotation(-1.5708f));
            }

            base.Draw();
        }
    }
}
