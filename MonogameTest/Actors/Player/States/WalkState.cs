using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameTest.Actors.Player
{
    internal class WalkState : PlayerState
    {
        internal override string Name => "Walk";

        internal WalkState(Player player) : base(player)
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
            _player.StateMachine.TransitionTo(_player.IdleState);
        }
    }
}
