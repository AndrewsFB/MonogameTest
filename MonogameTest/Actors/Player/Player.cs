using MonogameTest.Actors.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MonogameTest.Actors.Player
{
    internal class Player : Actor
    {
        public StateMachine StateMachine;
        public IdleState IdleState;
        public JumpState JumpState;
        public SuperJumpState SuperJumpState;
        public WalkState WalkState;
        public FallState FallState;
        public DashState DashState;
        public DashJumpState DashJumpState;
        public DoubleJumpState DoubleJumpState;
     
        public Player(Microsoft.Xna.Framework.Vector2 screenSize) :  base(screenSize)
        {
            IdleState = new IdleState(this);
            JumpState = new JumpState(this);
            SuperJumpState = new SuperJumpState(this);
            DoubleJumpState = new DoubleJumpState(this);
            WalkState = new WalkState(this);
            FallState = new FallState(this);
            DashState = new DashState(this);
            DashJumpState = new DashJumpState(this);

            StateMachine = new StateMachine(IdleState);
        }

        internal void Move(float x)
        {
            var isNotaJumping = StateMachine.CurrentState != JumpState && StateMachine.CurrentState != DoubleJumpState;
            if (!IsOnFloor && isNotaJumping)
            {
                StateMachine.TransitionTo(FallState);
                x /= (float)1.5;
            }
            else if (IsOnFloor && isNotaJumping)
            {
                StateMachine.TransitionTo(WalkState);
            }

            if (x < 0)
            {
                IsFacingRight = false;
                Position.X += (int)x;
            }
            else if(x > 0)
            {
                IsFacingRight = true;
                Position.X += (int)x;
            }
        }

        internal void Dash()
        {
            StateMachine.TransitionTo(DashState);
        }

        internal void DashJump()
        {
            if (IsOnFloor)
            {
                StateMachine.TransitionTo(DashJumpState);
            }
        }

        internal void Jump()
        {
            if (IsOnFloor)
            {
                StateMachine.TransitionTo(JumpState);
            }
        }

        internal void SuperJump()
        {
            if (IsOnFloor)
            {
                StateMachine.TransitionTo(SuperJumpState);
            }
        }

        internal void DoubleJump()
        {
            if (!IsOnFloor && StateMachine.CurrentState == FallState && FallState.IsReadyForDoubleJump)
            {
                StateMachine.TransitionTo(DoubleJumpState);
            }
        }

        internal void CancelJump()
        {
            var isJumping = (StateMachine.CurrentState == JumpState && StateMachine.CurrentState == DoubleJumpState);
            if (!IsOnFloor && isJumping)
            {
                StateMachine.TransitionTo(FallState);
            }
        }

        internal override void Update(GameTime gameTime) 
        {
            base.Update(gameTime);

            StateMachine.CurrentState.Update(gameTime);
        }

        internal override void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            base.Draw(spriteBatch, graphicsDevice);

            StateMachine.CurrentState.Draw(spriteBatch, graphicsDevice);
        }
    }
}

