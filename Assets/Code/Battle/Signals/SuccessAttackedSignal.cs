using Battle.Player;

namespace Battle.Signals
{
    class SuccessAttackedSignal
    {
        public PlayerController FromWho { get; }
        public PlayerController Target { get; }
        public float Damage { get; }

        public SuccessAttackedSignal(PlayerController fromWho, PlayerController target, float damage)
        {
            FromWho = fromWho;
            Target = target;
            Damage = damage;
        }
    }
}
