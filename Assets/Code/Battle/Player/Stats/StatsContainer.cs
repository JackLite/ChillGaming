using System.Collections.Generic;

namespace Battle.Player.Stats
{
    public class StatsContainer
    {
        public const int healthId = 0;
        public const int armorId = 1;
        public const int damageId = 2;
        public const int vampirismId = 3;

        public Stat Health => Stats[healthId];
        public Stat Armor => Stats[armorId];
        public Stat Damage => Stats[damageId];
        public Stat Vampirism => Stats[vampirismId];

        public Dictionary<int, Stat> Stats { get; }

        public StatsContainer(Stat[] stats)
        {
            Stats = new Dictionary<int, Stat>();
            foreach (var stat in stats)
                Stats.Add(stat.id, stat);
        }
    }
}
