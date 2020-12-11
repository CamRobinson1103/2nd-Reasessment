using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    /// <summary>
    /// An enemy is an actor that is able to "see" other actors.
    /// When given a target, an enemy will repeatedly check if it
    /// is in its sight range. 
    /// </summary>
    class Enemy : Actor
    {
        private Actor _target;
        private Sprite _sprite;
        private float _speed;

        public Actor Target
        {
            get { return _target; }
            set { _target = value; }
        }

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
        public Enemy(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, icon, color)
        {

            _sprite = new Sprite("Images/bomb.png");
            _collisionRadius = 1;

        }

        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="rayColor">The color of the symbol that will appear when drawn to raylib</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn to the console</param>
        public Enemy(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
            _sprite = new Sprite("Images/bomb.png");
            _collisionRadius = 1; ;

        }





        /// <summary>
        /// Checks to see if the target is within the given angle
        /// and within the given distance. Returns false if no
        /// target has been set. Both the angle and the distance are inclusive.
        /// </summary>
        /// <param name="maxAngle">The maximum angle (in radians) 
        /// that the target can be detected.</param>
        /// <param name="maxDistance">The maximum distance that the target can be detected.</param>
        /// <returns></returns>
        public bool CheckTargetInSight(float maxAngle, float maxDistance)
        {
            //Checks if the target has a value before continuing
            if (Target == null)
                return false;

            //Find the vector representing the distance between the actor and its target
            Vector2 direction = Target.LocalPosition - WorldPosition;
            //Get the magnitude of the distance vector
            float distance = direction.Magnitude;
            //Use the inverse cosine to find the angle of the dot product in radians
            float angle = (float)Math.Acos(Vector2.DotProduct(Forward, direction.Normalized));

            //Return true if the angle and distance are in range
            if (angle <= maxAngle && distance <= maxDistance)
                return true;

            return false;
        }





        public override void OnCollision(Actor other)
        {
            if (other is Player)
                GameManager.onWin?.Invoke();
            if (other is Scissors)
                GameManager.onWin?.Invoke();

            base.OnCollision(other);
        }

        public override void Start()
        {
            GameManager.onLose += Win;
            base.Start();
        }

        private void Win()
        {
            Raylib.DrawText("Congradulation! The bomb was stopped!!!!\nPress Esc to quit!", 150, 150, 100, Color.BLUE);
        }

        public override void Update(float deltaTime)
        {
            //If the target can be seen change the color to red
            //If the target can't be seen change the color to blue
            if (CheckTargetInSight(1.5f, 5))
            {
                _rayColor = Color.RED;
            }
            else
            {
                _rayColor = Color.BLUE;
            }
            Acceleration = new Vector2();
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
