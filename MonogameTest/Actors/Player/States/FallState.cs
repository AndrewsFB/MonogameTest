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
    internal class FallState : PlayerState
    {
        internal override string Name => "Fall";
        internal bool IsReadyForDoubleJump{ get; set; }

        public FallState(Player player) : base(player)
        {
        }

        internal override void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
        }

        internal override void Enter()
        {
        }

        internal override void Exit()
        {
            IsReadyForDoubleJump = false;
        }

        internal override void Update(GameTime gameTime)
        {
            if (_player.IsOnFloor)
            {
                _player.StateMachine.TransitionTo(_player.IdleState);
            }
        }
    }
}
