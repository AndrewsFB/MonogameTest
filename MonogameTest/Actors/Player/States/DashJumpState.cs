using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameTest.Actors.Player
{
    internal class DashJumpState : PlayerState
    {
        private float _jumpForce;
        private float _jumpFrames;
        private float _jumpLimit;
        private float _dashDistance;
        private int _dashCooldown;

        internal override string Name => "Dash Jump";

        internal DashJumpState(Player player) : base(player)
        {
            _jumpLimit = 15;
            _jumpForce = 8500 / _jumpLimit;
            _dashDistance = 850;
            _dashCooldown = 30;
        }

        internal override void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
        }

        internal override void Enter()
        {
            _jumpFrames = 0;
        }

        internal override void Exit()
        {
            _player.VanishGhosts();
        }

        internal override void Update(GameTime gameTime)
        {
            _jumpFrames += 1;
            if (_jumpFrames < _jumpLimit)
            {
                _player.MakeGhost();
                var jumpforce = -_jumpForce * (float)gameTime.ElapsedGameTime.TotalSeconds;
                var dashDistante = (_dashDistance * (_player.IsFacingRight ? 1 : -1)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                _player.Position += new Vector2(dashDistante, jumpforce);
            }
            if (_jumpFrames >= _dashCooldown)
            {
                _player.FallState.IsReadyForDoubleJump = true;
                _player.StateMachine.TransitionTo(_player.FallState);
            }
        }
    }
}
