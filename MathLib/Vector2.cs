using System;

namespace MathLib
{
    public class Vector2
    {
        private float _x;
        private float _y;


        public float X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }
        public float Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }
        public Vector2()
        {
            _x = 0;
            _y = 0;
        }
        public Vector2(float x, float y)
        {
            _x = x;
            _y = y;
        }
        public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
        {
            float x = lhs.X + rhs.X;
            float y = lhs.Y + rhs.Y;
            return new Vector2(x, y);
        }
        public static Vector2 operator *(Vector2 lhs, Vector2 scalar)
        {
            return new Vector2(lhs.X * scalar.X, lhs.X * scalar.X);
        }
        public float GetMagnitude()
        {
            return (float) Math.Sqrt(X * X + Y * Y);
        }
    }
}
