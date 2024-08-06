using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogameTest.Actors.Player;
using MonogameTest.Debug;

namespace MonogameTest
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player _player;

        private Rectangle rect;

        private SpriteFont _debug_font;

        private Dashboard _dashboard;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();

            _player = new Player(new Vector2(1920, 1080));
            rect = new Rectangle(0, 1000, 1920, 10);
        }

        protected override void Initialize()
        {
            base.Initialize();
            _debug_font = Content.Load<SpriteFont>("DebugFont");
            _dashboard = new Dashboard(_debug_font, _player);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);

            var gravity = World.GetInstance().Gravity * (float)gameTime.ElapsedGameTime.TotalSeconds; 
            World.GetInstance().ApplyGravity(_player, gravity, new Rectangle[] { rect });

            _player.Update(gameTime);

            Input(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);

            _spriteBatch.Begin();

            _player.Draw(_spriteBatch, GraphicsDevice);

            var texture = new Texture2D(GraphicsDevice, 1, 1);
            texture.SetData<Color>(new Color[] { Color.White });
            _spriteBatch.Draw(texture, rect, Color.Red);

            _dashboard.Draw(_spriteBatch, GraphicsDevice);

            _spriteBatch.End();
        }

        private void Input(GameTime gameTime)
        {
            var controls = Controls.GetInstance();
            controls.Prepare();

            var velocity = _player.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            var isReadyToDoubleJump = (_player.StateMachine.CurrentState == _player.FallState && _player.FallState.IsReadyForDoubleJump);
            var isDashing = (_player.StateMachine.CurrentState == _player.DashState || _player.StateMachine.CurrentState == _player.DashJumpState);
            var isOnFloor = _player.IsOnFloor;
            var isSuperJumping = (_player.StateMachine.CurrentState == _player.SuperJumpState);

            if (!isDashing && !isSuperJumping)
            {
                if (controls.Trigger(Controls.Input.Right))
                {
                    _player.Move(velocity);
                }
                else if (controls.Trigger(Controls.Input.Left))
                {
                    _player.Move(-velocity);
                }
            }

            if (controls.Trigger(Controls.Input.SuperJump) && isOnFloor)
            {
                _player.SuperJump();
            }
            else if (controls.Trigger(Controls.Input.Jump) && isDashing && isOnFloor)
            {
                _player.DashJump();
            }
            else if (controls.Trigger(Controls.Input.Dash))
            {
                _player.Dash();
            }
            else if (controls.Trigger(Controls.Input.Jump) && isReadyToDoubleJump)
            {
                 _player.DoubleJump();
            }
            else if (controls.Trigger(Controls.Input.Jump))
            {
                _player.Jump();
            }
            else if (!controls.CheckCanceled(Controls.Input.Jump))
            {
                _player.CancelJump();
            }
        }
    }
}
