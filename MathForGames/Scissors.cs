using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    class Scissors : Actor
    {
        private float _speed = 1;
        private Sprite _sprite;
        private Actor _enemy;

        public Scissors(float x, float y, Actor enemy1, Actor enemy2, Actor enemy3, char icon = ' ', ConsoleColor color = ConsoleColor.White)
           : base(x, y, icon, color)
        {
            _enemy = enemy1;
            _enemy = enemy2;
            _enemy = enemy3;
            _collisionRadius = 1; ;
            _sprite = new Sprite("Images/scissors.png");

        }

        public Scissors(float x, float y, Actor enemy1, Actor enemy2, Actor enemy3, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
           : base(x, y, rayColor, icon, color)
        {
            _sprite = new Sprite("Images/scissors.png");
            _enemy = enemy1;
            _enemy = enemy2;
            _enemy = enemy3;
            _collisionRadius = 1; ;
        }

        private bool CheckPlayerDistance()
        {
            float distance = (_enemy.Position - Position).Magnitude;
            return distance <= 1;
        }

        public override void Update(float deltaTime)
        {
            //If the player is in range of the goal, end the game
            if (CheckPlayerDistance())
                Game.SetGameOver(true);

            base.Update(deltaTime);
        }


        public override void Draw()
        {
            _sprite.Draw(_globalTransform);
            base.Draw();
        }
    }
}
