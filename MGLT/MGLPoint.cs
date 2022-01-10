using System;
using Microsoft.Xna.Framework;

namespace MGLT
{
    public class MGLPoint
    {
        public float X { get; set; }
        public float Y { get; set; }

        public MGLPoint(float x, float y)
        {
            X = x;
            Y = y;
        }

        public MGLPoint(MGLPoint p)
        {
            X = p.X;
            Y = p.Y;
        }

        public static implicit operator MGLPoint(ValueTuple<int, int> t)
        {
            return new MGLPoint(t.Item1, t.Item2);
        }

        public static implicit operator MGLPoint(ValueTuple<double, double> t)
        {
            return new MGLPoint((float)t.Item1, (float)t.Item2);
        }

        public static implicit operator MGLPoint(ValueTuple<float, float> t)
        {
            return new MGLPoint(t.Item1, t.Item2);
        }

        public static implicit operator Vector2(MGLPoint p)
        {
            return new Vector2(p.X, p.Y);
        }

        public static implicit operator MGLPoint(Vector2 p)
        {
            return new MGLPoint(p.X, p.Y);
        }

        public double Distance(MGLPoint p)
        {
            return Math.Sqrt(Math.Pow(X - p.X, 2) + Math.Pow(Y - p.Y, 2));
        }

        // define our operator overrides
        public static double operator >(MGLPoint a, MGLPoint b)
        {
            return a.Distance(b);
        }

        public static double operator <(MGLPoint a, MGLPoint b)
        {
            return a.Distance(b);
        }

        public static MGLPoint operator *(MGLPoint a, MGLPoint b)
        {
            return new MGLPoint(a.X * b.X, a.Y * b.Y);
        }

        public static MGLPoint operator /(MGLPoint a, MGLPoint b)
        {
            return new MGLPoint(a.X / b.X, a.Y / b.Y);
        }

        public static MGLPoint operator +(MGLPoint a, MGLPoint b)
        {
            return new MGLPoint(a.X + b.X, a.Y + b.Y);
        }

        public static MGLPoint operator -(MGLPoint a, MGLPoint b)
        {
            return new MGLPoint(a.X - b.X, a.Y - b.Y);
        }

        public static bool operator ==(MGLPoint a, MGLPoint b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(MGLPoint a, MGLPoint b)
        {
            return !(a == b);
        }

        // define our function overrides
        public override bool Equals(object obj)
        {
            return this == (MGLPoint)obj;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
