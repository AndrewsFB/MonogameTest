﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameTest.Actors.Player
{
    internal class DoubleJumpState : PlayerState
    {
        private float _jumpForce;
        private float _jumpFrames;
        private float _jumpLimit;

        internal DoubleJumpState(Player player) : base(player)
        {
            _jumpLimit = 15;
            _jumpForce = 10000 / _jumpLimit;
        }

        internal override string Name => "Double Jump";

        internal override void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
        }

        internal override void Enter()
        {
            _jumpFrames = 0;
        }

        internal override void Exit()
        {
            _player.FallState.IsReadyForDoubleJump = false;
        }

        internal override void Update(GameTime gameTime)
        {
            _jumpFrames += 1;
            var jumpforce = -_jumpForce * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _player.Position += new Vector2(0, jumpforce);
            if (_jumpFrames >= _jumpLimit)
            {
                _player.StateMachine.TransitionTo(_player.FallState);
            }
        }
    }
}
