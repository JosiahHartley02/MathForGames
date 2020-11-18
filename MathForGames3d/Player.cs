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
           /* Raylib.DrawCylinder(new System.Numerics.Vector3(WorldPosition.X, WorldPosition.Y, WorldPosition.Z), Scale * .3f, Scale * .3f, 1, 100, defaultColor);*/
            Raylib.DrawCube(new System.Numerics.Vector3(WorldPosition.X - .4f, WorldPosition.Y, WorldPosition.Z), .3f, .3f, 1f, defaultColor);
            Raylib.DrawCube(new System.Numerics.Vector3(WorldPosition.X + .4f, WorldPosition.Y, WorldPosition.Z), .3f, .3f, 1f, defaultColor);
            Raylib.DrawCube(new System.Numerics.Vector3(WorldPosition.X, WorldPosition.Y + .3f, WorldPosition.Z), .7f, .3f, 1f, defaultColor);
            Raylib.DrawCube(new System.Numerics.Vector3(WorldPosition.X , WorldPosition.Y + .6f, WorldPosition.Z), .3f, .3f, 2f, Color.BLACK);
        }
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }
        public override void End()
        {
            base.End();
        }
    }
}
