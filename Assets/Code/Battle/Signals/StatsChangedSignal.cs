using Battle.Player;

namespace Battle.Signals
{
    class StatsChangedSignal
    {
        public PlayerController Player { get; }

        public StatsChangedSignal(PlayerController player)
        {
            Player = player;
        }
    }
}
