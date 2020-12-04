using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    class PlayersTurret : Actor
    {
        public PlayersTurret(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
            _sprite = new Sprite("sprites/Tanks/barrelRed.png");
            _collisionRadius = -10;
            Collidable = false;
        }

        public override void Update(float deltaTime)
        {
            if(isColliding) { _isVisible = false; }
            _rotationSpeed = (Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_Q))
                - Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_E))) * .1f;
            bool fire = Game.GetKeyDown((int)KeyboardKey.KEY_SPACE);
            if (fire == true)
            { LaunchProjectile(_projectiles[0]); }
            base.Update(deltaTime);

        }
        public override void Draw()
        {
            if (_sprite != null && IsVisible != false)
            {
                _sprite.Draw(_globalTransform);
            }

            base.Draw();
        }
    }
}

