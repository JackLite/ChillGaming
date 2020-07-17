using Battle.Player;
using UnityEngine;

namespace Battle.Signals
{
    class SuccessAttackSignal
    {
        public PlayerController FromWho { get; }
        public PlayerController Target { get; }
        public float Damage { get; }

        public SuccessAttackSignal(PlayerController fromWho, PlayerController target, float damage)
        {
            FromWho = fromWho;
            Target = target;
            Damage = damage;
        }
    }
}
