using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathForGames
{
    /// <summary>
    /// A goal is an actor that checks if the player has collided with it.
    /// If so the player wins the game.
    /// </summary>
    class Message : Actor
    {
        public Message(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White) : base(x, y, icon, color)
        {

        }

        public Message(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White) : base(x, y, rayColor, icon, color)
        {

        }

        public override void Draw()
        {

            Raylib.DrawText("One of these bombs are real! Difuse it!", ((int)(WorldPosition.X * 32) - 100), ((int)(WorldPosition.Y * 32) - 75), 30, Color.BLUE);
            base.Draw();
        }

        public override void End()
        {
            base.End();
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
            GameManager.onWin += DrawWinText;
            base.Start();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }

        private void DrawWinText()
        {
            Raylib.DrawText("Congradulation! The bomb was stopped!!!!\nPress Esc to quit!", 200, 400, 40, Color.BLUE);
        }
    }
}
