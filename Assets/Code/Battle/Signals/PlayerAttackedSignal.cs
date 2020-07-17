using Battle.Player;

namespace Battle.Signals
{
    public class PlayerAttackedSignal
    {
        public PlayerController FromWho { get; }
        public PlayerAttackedSignal(PlayerController fromWho)
        {
            FromWho = fromWho;
        }
    }
}
