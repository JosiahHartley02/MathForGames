using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    class Player : Actor

    {
        private float _speed = 1;
        Sprite _trail;
        Matrix3 lastLocation;
        int delayedFrameCounter = 0;
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

        public Player(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
            _sprite = new Sprite("sprites/Tanks/tankRed.png");
            _trail = new Sprite("sprites/Tanks/tracksLarge.png");
            _collisionRadius = 15;
            _collidable = true;
        }

        public override void Update(float deltaTime)
        {
            //Gets the player's input to determine which direction the actor will move in on each axis 
            int Throttle = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_S))
                + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_W));

            float rotation = (Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_A))
                - Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_D)));

            //Set the actors current velocity to be the a vector with the direction found scaled by the speed 
            Acceleration = new Vector2(_localTransform.m11 * Throttle, _localTransform.m21 * Throttle);            
            Rotate(rotation * deltaTime * 2);
            base.Update(deltaTime);
            if (Target != null)
            {
                if (Game.GetCurrentScene().TestForCollisionWith(this, Target))
                { Velocity = Forward * -3; Target.Velocity = Forward * 3; }
            }
            _globalTransform = _localTransform;
            lastLocation = _globalTransform;
            if (Game.GetCurrentScene().TestForCollisionWith(this, this.Target.Children[0].Projectiles[0]))  //When Targets Bullet Collides With This, Reset Position
            {
                Random rnd = new Random();
                int xSpawnPosition = rnd.Next(1, 31);
                int ySpawnPosition = rnd.Next(1, 23);
                _translation.m13 = xSpawnPosition;
                _translation.m23 = ySpawnPosition;
                Game.GetCurrentScene().Score.Y++;
            }


        }
        public override void Draw()
        {
            if (_sprite != null && IsVisible != false)
            {
                _sprite.Draw(_localTransform * Matrix3.CreateRotation(1.5708f));
                if (_trail != null)
                    if (delayedFrameCounter == 24)
                    { _trail.Draw(lastLocation * Matrix3.CreateRotation(1.5708f)); delayedFrameCounter = 0; }

            }

            base.Draw();
        }
    }
}
