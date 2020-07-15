using Battle.Player;
using UnityEngine;

namespace Battle.Signals
{
    class StatsChangedSignal
    {
        public PlayerController Player { get; private set; }

        public StatsChangedSignal(PlayerController player)
        {
            Player = player;
        }
    }
}
