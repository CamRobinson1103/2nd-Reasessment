using System;
using System.Collections.Generic;
using System.Text;

namespace MathForGames
{
    class Scene
    {
        private Actor[] _actors;

        public Scene()
        {
            _actors = new Actor[0];
        }

        public void AddActor(Actor actor)
        {
            Actor[] appendedArray = new Actor[_actors.Length + 1];
            for (int i = 0; i < _actors.Length; i++)
            {
                 appendedArray[i] = _actors[i];
            }
            appendedArray[_actors.Length] = actor;
            _actors = appendedArray;
        }

        public void Start()
        {

        }

        public void Update()
        {

        }

        public void Draw()
        {

        }

        public void End()
        {

        }
    }
}
