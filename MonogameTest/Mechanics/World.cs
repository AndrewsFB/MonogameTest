using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonogameTest.Actors;

namespace MonogameTest
{
    internal sealed class World
    {
        private static World _instance;

        public float Gravity { get; private set; }

        private World() 
        {
            Gravity = 300;
        }

        public static World GetInstance()
        {
            if (_instance == null)
            {
                _instance = new World();
            }
            return _instance;
        }

        internal void ApplyGravity(Actor actor, float gravity, Rectangle[] rects)
        {
            foreach (Rectangle rect in rects)
            {
                if (actor.WillCollide(new Vector2(0, gravity), rect))
                {
                    actor.IsOnFloor = true;
                    return;
                }
            }
            if (actor.GravityAffected)
            {
                actor.Position.Y += (int)gravity;
                actor.IsOnFloor = false;
            }
        }
    }
}
