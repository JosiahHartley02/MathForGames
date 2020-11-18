using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames3D
{
    class Player : Actor
    {
        public Player(float x, float y, float z, Color color) : base(x, y, z, color)
        {
            LocalPosition = new Vector3(x, y, z);
            defaultColor = color;
        }
        public override void Start()
        {
            Health = 100;
            base.Start();
        }
        public override void Draw()
        {
            Raylib.DrawSphere(new System.Numerics.Vector3(WorldPosition.X, WorldPosition.Y, WorldPosition.Z), .3f, Color.BLUE);
        }
        public override void Update(float deltaTime)
        {
            int xDirection = (Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_A)) - (Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_D))));
            int zDirection = (Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_W)) - (Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_S))));
            int yRotation = (Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_Q)) - (Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_E))));
            _translation.m14 += (xDirection * 1f) * deltaTime;
            _translation.m34 += (zDirection * 1f) * deltaTime;
            RotateY(yRotation);
            base.Update(deltaTime);
        }
        public override void End()
        {
            base.End();
        }
    }
}
