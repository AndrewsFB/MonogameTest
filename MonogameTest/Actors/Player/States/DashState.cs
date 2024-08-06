using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameTest.Actors.Player
{
    internal class DashState : PlayerState
    {
        private float _dashForce;
        private float _dashFrames;
        private int _dashLimit;
        private int _dashCooldown;

        public DashState(Player player) : base(player)
        {
            _dashLimit = 15;
            _dashCooldown = 30;
            _dashForce = 7000 / _dashLimit;
        }

        internal override string Name => "Dash";

        internal override void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
        }

        internal override void Enter()
        {
            _dashFrames = 0;
            _player.GravityAffected = false;
        }

        internal override void Exit()
        {
            _player.GravityAffected = true;
            _player.VanishGhosts();
        }

        internal override void Update(GameTime gameTime)
        {
            _dashFrames += 1;
            if(_dashFrames < _dashLimit)
            {
                _player.MakeGhost();
                var dashforce = _dashForce * (float)gameTime.ElapsedGameTime.TotalSeconds;
                var dashDirection = dashforce * (_player.IsFacingRight ? 1 : -1);
                _player.Position += new Vector2(dashDirection, 0);
            }
            if(_dashFrames > _dashLimit)
            {
                _player.GravityAffected = true;
            }
            if (_dashFrames >= _dashCooldown)
            {
                _player.FallState.IsReadyForDoubleJump = true;
                _player.StateMachine.TransitionTo(_player.FallState);
            }
        }
    }
}
