using System;
using System.Collections.Generic;

namespace Battle.Helpers
{
    static class StatHelper
    {
        private static readonly Dictionary<StatType, int> _statIdMap;
        static StatHelper()
        {
            _statIdMap = new Dictionary<StatType, int>()
            {
                { StatType.health, 0 },
                { StatType.armor, 1 },
                { StatType.attack, 2 },
                { StatType.vampirism, 3 }
            };
        }

        public static int GetStatId(StatType statType)
        {
            return _statIdMap[statType];
        }

        public static void ApplyBuffsToStat(ref Stat stat, IEnumerable<Buff> buffs)
        {
            foreach(var buff in buffs)
            {
                if (!TryGetBuffStat(stat, buff.stats, out var buffStat)) continue;

                stat.value += buffStat.value;
            }
        }

        private static bool TryGetBuffStat(Stat stat, IEnumerable<BuffStat> buffStats, out BuffStat result)
        {
            result = null;
            foreach (var buffStat in buffStats)
            {
                if (buffStat.statId == stat.id)
                {
                    result = buffStat;
                    return true;
                }
            }
            return false;
        }
    }

    public enum StatType
    {
        health,
        armor,
        attack,
        vampirism
    }
}
