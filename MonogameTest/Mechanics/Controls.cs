using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameTest
{
    public class Controls
    {
        private static Controls _instance;

        KeyboardState _currentKeyState;
        KeyboardState _previousKeyState;

        GamePadState _currentGamepadState;
        GamePadState _previousGamepadState;

        private Controls()
        {
        }

        public static Controls GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Controls();
            }
            return _instance;
        }

        public void Prepare()
        {
            _previousKeyState = _currentKeyState;
            _currentKeyState = Keyboard.GetState();

            _previousGamepadState = _currentGamepadState;
            _currentGamepadState = GamePad.GetState(0);
        }

        public bool CheckCanceled(Input input)
        {
            var triggered = false;

            switch (input)
            {
                case Input.Dash: triggered = !IsPressed(Keys.Q, Buttons.RightShoulder); break;
                case Input.Jump: triggered = !IsPressed(Keys.Space, Buttons.A); break;
                case Input.Action: triggered = !IsPressed(Keys.E, Buttons.X); break;
            }

            return triggered;
        }

        public bool Trigger(Input input)
        {
            var triggered = false;

            switch (input)
            {
                case Input.Up : triggered = IsPressed(Keys.Up, Buttons.DPadUp) || IsPressed(Keys.W, Buttons.LeftThumbstickUp); break;
                case Input.Down: triggered = IsPressed(Keys.Down, Buttons.DPadDown) || IsPressed(Keys.S, Buttons.LeftThumbstickDown); break;
                case Input.Left: triggered = IsPressed(Keys.Left, Buttons.DPadLeft) || IsPressed(Keys.A, Buttons.LeftThumbstickLeft); break;
                case Input.Right: triggered = IsPressed(Keys.Right, Buttons.DPadRight) || IsPressed(Keys.D, Buttons.LeftThumbstickRight); break;
                case Input.Dash: triggered = HasBeenPressed(Keys.Q, Buttons.RightShoulder); break;
                case Input.Jump: triggered = HasBeenPressed(Keys.Space, Buttons.A); break;
                case Input.SuperJump: triggered = HasBeenPressed(Keys.LeftShift, Buttons.Y); break;
                case Input.Action: triggered = HasBeenPressed(Keys.E, Buttons.X); break;
            }

            return triggered;
        }

        private bool IsPressed(Keys key, Buttons button)
        {
            return _currentKeyState.IsKeyDown(key) || _currentGamepadState.IsButtonDown(button);
        }

        private bool HasBeenPressed(Keys key, Buttons button)
        {
            return (_currentKeyState.IsKeyDown(key) && !_previousKeyState.IsKeyDown(key)) ||
                   (_currentGamepadState.IsButtonDown(button) && !_previousGamepadState.IsButtonDown(button));
        }

        public enum Input 
        {
            Right,
            Left,
            Up,
            Down,
            Jump,
            SuperJump,
            Dash,
            Action
        }
    }
}
