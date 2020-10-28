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
        private bool _jumping = false;
        private int _jumpUpdateCounter = 0;

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


        public Player(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, icon, color)
        {
            
        }

        public Player(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {

        }
        public override void Update(float deltaTime)
        {
            int xVelocity = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_A)) + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_D));
            int yVelocity = (int)Velocity.Y;
            if (Game.GetKeyPressed((int)KeyboardKey.KEY_W) == true && _jumping == false && _jumpUpdateCounter == 0 && Transform.Position.Y == Console.WindowHeight - 7.2f)
            {
                yVelocity = -1;
                _jumping = true;
                _jumpUpdateCounter = 7;
            }
            if (_jumpUpdateCounter > 0)
            { _jumpUpdateCounter -= 1; yVelocity = -1; }
            else if (_jumpUpdateCounter == 0 && _jumping == true) { _jumping = false; yVelocity = 0; }
            else if (_jumping == false && Transform.Position.Y < Console.WindowHeight - 7.2f)
            { yVelocity = 1; }
            else if (Transform.Position.Y > Console.WindowHeight - 7.2f) { yVelocity = 0; Transform.Position.Y = Console.WindowHeight - 7.2f; }
            Velocity = new Vector2(xVelocity, yVelocity);
            Velocity = Velocity.Normalized * Speed;
            base.Update(deltaTime);
        }
        /*public override void Update(float deltaTime)
        {            
            int xVelocity = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_A)) //sets xVelocity value by converting two truefalse statements, "KeyADown" and "KeyDDown" which will either return [0] or [1] by converting the bool |GetKeyDown| testing for 'A' or 'D' key to be down
                + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_D));  //if 'A' key is down then the value is [(-)1], if the 'D' key is down the value is [1], so A adds [(-)1] speed which converts speed the other way, and 'D' key adds [1] which makes the actor move right, pressing both will result in a value of [0] or not moving 

            int yVelocity = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_W)) //yVelocity value is equal to the value of (-)key 'W' isTrue and the value of key 'S' is true
                + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_S));

            Velocity = new Vector2(xVelocity, yVelocity);                             //sets the new velocity based on the keys being pressed
            Velocity = Velocity.Normalized * Speed;                                  //Normalizes the speed as to prevent superspeed
            
            base.Update(deltaTime);
        }*/
    }
}
