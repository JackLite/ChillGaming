using Battle.Player;
using UnityEngine;

namespace Battle.Signals
{
    class PlayerAttackSignal
    {
        public PlayerId FromWho { get; private set; }
        public float Damage { get; private set; }
        public PlayerAttackSignal(PlayerId fromWho, float damage)
        {
            FromWho = fromWho;
            Damage = damage;
        }
    }
}
