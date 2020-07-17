using Battle.Player;

namespace Battle.Signals
{
    class PlayerAttackedSignal
    {
        public PlayerController FromWho { get; }
        public PlayerAttackedSignal(PlayerController fromWho)
        {
            FromWho = fromWho;
        }
    }
}
