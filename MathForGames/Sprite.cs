using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Sprite
    {
        private Texture2D _texture;

        //Width of the loaded texture
        public int Width
        {
            get
            {
                return _texture.width;
            }
            set
            {
                _texture.width = value;
            }
        }

        //Height of the loaded texture
        public int Height
        {
            get
            {
                return _texture.height;
            }
            set
            {
                _texture.height = value;
            }
        }

        /// <summary>
        /// Sprite(Texture2D texture)
        /// </summary>
        /// <param name="texture">Sets the sprites image to be the given texture</param>
        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        /// <summary>
        /// Loads the texture at the given path
        /// </summary>
        /// <param name="texture">The file path of the texture</param>
        public Sprite(string path)
        {
            _texture = Raylib.LoadTexture(path);
        }

        /// <summary>
        /// Draws the sprite using the rotation, translation, and scale
        /// of the given transform
        /// </summary>
        /// <param name="transform"></param>
        public void Draw(Matrix3 transform)
        {
            //Finds the scale of the sprite
            float xMagnitude = (float)Math.Round(new Vector2(transform.m11, transform.m21).Magnitude);
            float yMagnitude = (float)Math.Round(new Vector2(transform.m12, transform.m22).Magnitude);
            Width = (int)xMagnitude;
            Height = (int)yMagnitude;

            //Sets the sprite center to the transform origin
            System.Numerics.Vector2 pos = new System.Numerics.Vector2(transform.m13, transform.m23);
            System.Numerics.Vector2 forward = new System.Numerics.Vector2(transform.m11, transform.m21);
            System.Numerics.Vector2 up = new System.Numerics.Vector2(transform.m12, transform.m22);

            if (pos.X < 0 && pos.Y > 0)
                pos.X = Math.Abs(pos.X);
            else if (pos.X > 0 && pos.Y < 0)
                pos.Y = Math.Abs(pos.Y);

            pos -= (forward / forward.Length()) * Width / 2;
            pos -= (up / up.Length()) * Height / 2;

            //Find the transform rotation in radians 
            float rotation = (float)Math.Atan2(transform.m21, transform.m11);

            //Draw the sprite
            Raylib.DrawTextureEx(_texture, pos * 32,
                (float)(rotation * 180.0f / Math.PI), 32, Color.WHITE);

        }
    }
}
