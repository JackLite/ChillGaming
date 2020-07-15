using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battle.Player;

namespace Battle.Signals
{
    class BuffsChangedSignal
    {
        public PlayerController Player { get; private set; }
        public BuffsChangedSignal(PlayerController player)
        {
            Player = player;
        }
    }
}
