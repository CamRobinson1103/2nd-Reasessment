using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;

namespace MathLibrary
{
    public class Vector4
    {
        private float _x;
        private float _y;
        private float _z;
        private float _W;

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

        public float Z
        {
            get
            {
                return _z;
            }
            set
            {
                _z = value;
            }
        }

        public float W
        {
            get
            {
                return _W;
            }
            set
            {
                _W = value;
            }
        }

        public float Magnitude
        {
            get
            {
                return (float)Math.Sqrt(X * X + Y * Y + Z * Z + W * W);
            }
        }

        public Vector4 Normalized
        {
            get
            {
                return Normalize(this);
            }
        }

        public Vector4()
        {
            _x = 0;
            _y = 0;
            _z = 0;
            _W = 0;
        }

        public Vector4(float x, float y, float z, float w)
        {
            _x = x;
            _y = y;
            _z = z;
            _W = w;
        }

        /// <summary>
        /// Returns the normalized version of a the vector passed in.
        /// </summary>
        /// <param name="vector">The vector that will be normalized</param>
        /// <returns></returns>
        public static Vector4 Normalize(Vector4 vector)
        {
            if (vector.Magnitude == 0)
                return new Vector4();

            return vector / vector.Magnitude;
        }

        /// <summary>
        /// Returns the dot product of the two vectors given.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static float DotProduct(Vector4 lhs, Vector4 rhs)
        {
            return (lhs.X * rhs.X) + (lhs.Y * rhs.Y) + (lhs.Z * rhs.Z);
        }

        public static Vector4 CrossProduct(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4(lhs.Y * rhs.Z - lhs.Z * rhs.Y, lhs.Z * rhs.X - lhs.X * rhs.Z, lhs.X * rhs.Y - lhs.Y * rhs.X, 0);
        }

        public static Vector4 operator +(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z, lhs.W + rhs.W);
        }

        public static Vector4 operator -(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z, lhs.W - rhs.W);
        }

        public static Vector4 operator *(Vector4 lhs, float scalar)
        {
            return new Vector4(lhs.X * scalar, lhs.Y * scalar, lhs.Z * scalar, lhs.W * scalar);
        }

        public static Vector4 operator *(float scalar, Vector4 lhs)
        {
            return new Vector4(lhs.X * scalar, lhs.Y * scalar, lhs.Z * scalar, lhs.W * scalar);
        }

        public static Vector4 operator /(Vector4 lhs, float scalar)
        {
            return new Vector4(lhs.X / scalar, lhs.Y / scalar, lhs.Z / scalar, lhs.W / scalar);
        }

        public static implicit operator Vector4((float, float, float, float) tuple)
        {
            return new Vector4(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
        }

    }



}
