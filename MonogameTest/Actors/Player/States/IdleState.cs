using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameTest.Actors.Player
{
    internal class IdleState : PlayerState
    {
        internal override string Name => "Idle";

        internal IdleState(Player player) : base(player)
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
        }

        internal override void Update(GameTime gameTime)
        {
            if (!_player.IsOnFloor && _player.StateMachine.CurrentState != _player.FallState)
            {
                _player.StateMachine.TransitionTo(_player.FallState);
            }
        }
    }
}
