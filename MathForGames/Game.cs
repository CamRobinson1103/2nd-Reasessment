using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MathForGames
{
    class Game
    {
        private static bool _gameOver = false;

        //Static function used to set game over without an instance of game.
        public static void SetGameOver(bool value)
        {
            _gameOver = value;
        }


        //Called when the game begins. Use this for initialization.
        public void Start()
        {

        }


        //Called every frame.
        public void Update()
        {

        }

        //Used to display objects and other info on the screen.
        public void Draw()
        {

        }


        //Called when the game ends.
        public void End()
        {

        }


        //Handles all of the main game logic including the main game loop.
        public void Run()
        {
            Start();

            while(!_gameOver)
            {
                Update();
                Draw();
            }

            End();
        }
    }
}
