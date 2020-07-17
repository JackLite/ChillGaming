using Battle.Player.Buffs;
using Battle.Player.Stats;
using Zenject;

namespace Battle.Player
{
    public class PlayerData
    {
        public StatsContainer Stats { get; }
        public BuffsContainer Buffs { get; }

        public PlayerData(StatsContainer stats, BuffsContainer buffs)
        {
            Stats = stats;
            Buffs = buffs;
        }

        public class Factory : PlaceholderFactory<bool, PlayerData>
        {
        }
    }
}
