using MonogameTest.Mechanics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameTest.Actors
{
    internal class Actor
    {
        internal float Velocity;
        internal bool IsFacingRight;
        internal Vector2 Position;
        internal bool IsOnFloor { get; set; }
        internal bool GravityAffected { get; set; }

        private Rectangle _body;
        private Rectangle _collisionBox;
        private IList<GhostTrail> _ghostTrails;

        private Vector2 _screenSize;

        internal Actor(Vector2 screenSize)
        {
            Position = new Vector2(500, 0);
            Velocity = 300;

            _body = new Rectangle(0, 0, 20, 20);
            _collisionBox = new Rectangle(0, 0, 20, 20);
            GravityAffected = true;

            _ghostTrails = new List<GhostTrail>();

            _screenSize = screenSize;
        }

        internal virtual void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            var texture = new Texture2D(graphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });
            spriteBatch.Draw(texture, _body, Color.White);

            foreach(GhostTrail trail in _ghostTrails)
            {
                trail.Draw(spriteBatch, graphicsDevice, texture);
            }
        }

        internal virtual void Update(GameTime gameTime)
        {
            if ((int)Position.X > 0 && (int)Position.X < _screenSize.X-_body.Width) 
            {
                _body.X = (int)Position.X;
                _collisionBox.X = (int)Position.X;
            }
            else
            {
                Position.X = _body.X;
                _collisionBox.X = _body.X;
            }

            if ((int)Position.Y > 0 && (int)Position.Y < _screenSize.Y+_body.Height)
            {
                _body.Y = (int)Position.Y;
                _collisionBox.Y = (int)Position.Y;
            }
            else
            {
                Position.Y = _body.Y;
                Position.Y = _collisionBox.Y;
            }
            
            if(_body.X < 0)
            {
                _body.X = 0;
                _collisionBox.X = _body.X;
            }
            else if(_body.X < 0) 
            {
                _body.Y = 0;
                _collisionBox.Y = _body.Y;
            }

            if (_body.X > _screenSize.X)
            {
                _body.X = (int)_screenSize.X;
                _collisionBox.X = (int)_screenSize.X;
            }
            else if (_body.X < 0)
            {
                _body.Y = (int)_screenSize.Y;
                Position.Y = (int)_screenSize.Y;
            }


            foreach (GhostTrail trail in _ghostTrails)
            {
                trail.Update(gameTime);
            }
        }

        internal bool WillCollide(Vector2 move, Rectangle body)
        {
            var previous_position = new Vector2(_collisionBox.X, _collisionBox.Y);
            _collisionBox.X += (int)move.X;
            _collisionBox.Y += (int)move.Y;
            var will_collide = _collisionBox.Intersects(body);
            _collisionBox.X = (int)previous_position.X;
            _collisionBox.Y = (int)previous_position.Y;

            return will_collide;
        }

        internal void MakeGhost()
        {
            _ghostTrails.Add(new GhostTrail(_body));
        }

        internal void VanishGhosts()
        {
            _ghostTrails.Clear();
        }
    }
}
