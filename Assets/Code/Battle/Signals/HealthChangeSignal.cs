using Battle.Player;
using UnityEngine;

namespace Battle.Signals
{
    class HealthChangeSignal
    {
        public PlayerController Player { get; private set; }

        public HealthChangeSignal(PlayerController player)
        {
            Player = player;
        }
    }
}
