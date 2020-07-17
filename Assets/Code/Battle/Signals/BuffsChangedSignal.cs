using Battle.Player;

namespace Battle.Signals
{
    public class BuffsChangedSignal
    {
        public PlayerController Player { get; }
        public BuffsChangedSignal(PlayerController player)
        {
            Player = player;
        }
    }
}
