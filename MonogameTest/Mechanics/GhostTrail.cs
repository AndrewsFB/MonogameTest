using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameTest.Mechanics
{
    internal class GhostTrail
    {
        private Rectangle _body;
        private int _ghostFrames;
        private int _ghostLimit;

        public bool Vanish { get; private set; }

        public GhostTrail(Rectangle body) 
        {
            _body = body;
            _ghostFrames = 0;
            _ghostLimit = 15;
        }

        public void Update(GameTime gameTime) 
        { 
            _ghostFrames += 1;
            if(_ghostFrames >= _ghostLimit)
            {
                Vanish = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Texture2D texture) 
        {
            if (!Vanish)
            {
                spriteBatch.Draw(texture, _body, Color.White * 0.2f);
            }
        }
    }
}
