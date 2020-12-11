using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    /// <summary>
    /// An actor that moves based on input given by the user
    /// </summary>
    class Player : Actor
    {
        private float _speed = 1;
        private Sprite _sprite;
        private bool _canMove = true;


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
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn</param>
        public Player(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, icon, color)
        {
            _sprite = new Sprite("Images/maleAdventure.png");
            _collisionRadius = 1; ;
        }

        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="rayColor">The color of the symbol that will appear when drawn to raylib</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn to the console</param>
        public Player(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
            _sprite = new Sprite("Images/maleAdventure.png");
            _collisionRadius = 1; ;
        }



        public void DisableControls()
        {
            _canMove = false;
        }


        public override void Start()
        {

            GameManager.onWin += DisableControls;
            base.Start();
        }



        public override void Update(float deltaTime)
        {
            if (!_canMove)
                return;

            //Gets the player's input to determine which direction the actor will move in on each axis 
            int xDirection = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_A))
                + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_D));
            int yDirection = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_W))
                + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_S));




            //Set the actors current velocity to be the a vector with the direction found scaled by the speed
            Acceleration = new Vector2(xDirection, yDirection);
            Velocity = Velocity.Normalized * Speed;
            base.Update(deltaTime);
        }



        public override void Draw()
        {
            _sprite.Draw(_globalTransform);
            base.Draw();
        }
    }
}
