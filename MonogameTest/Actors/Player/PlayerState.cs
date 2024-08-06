using MonogameTest.Actors.Player;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameTest.Actors.Player
{
    internal abstract class PlayerState : State
    {
        public Player _player;

        internal PlayerState(Player player)
        {
            _player = player;
        }
    }
}
