using Battle.Player;

namespace Battle.Signals
{
    class BuffsChangedSignal
    {
        public PlayerController Player { get; }
        public BuffsChangedSignal(PlayerController player)
        {
            Player = player;
        }
    }
}
