using Battle.Player;

namespace Battle.Signals
{
    public class StatsChangedSignal
    {
        public PlayerController Player { get; }

        public StatsChangedSignal(PlayerController player)
        {
            Player = player;
        }
    }
}
