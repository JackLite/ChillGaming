using Battle.Player;
using UnityEngine;

namespace Battle.Signals
{
    class PlayerAttackSignal
    {
        public PlayerController FromWho { get; private set; }
        public PlayerAttackSignal(PlayerController fromWho)
        {
            FromWho = fromWho;
        }
    }
}
