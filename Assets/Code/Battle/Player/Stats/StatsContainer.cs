using System;
using UnityEngine;

namespace Battle.Player.Stats
{
    class StatsContainer
    {
        public const int healthId = 0;
        public const int armorId = 1;
        public const int damageId = 2;
        public const int vampirismId = 3;

        public Stat Health { get; }
        public Stat Armor { get; }
        public Stat Damage { get; }
        public Stat Vampirism { get; }

        public Stat[] Stats { get; }

        public StatsContainer(Stat[] stats)
        {
            Stats = stats;
            foreach(var stat in stats)
            {
                switch(stat.id)
                {
                    case healthId:
                        Debug.Log(stat.GetHashCode());
                        Health = stat;
                        break;
                    case armorId:
                        Armor = stat;
                        break;
                    case damageId:
                        Damage = stat;
                        break;
                    case vampirismId:
                        Vampirism = stat;
                        break;
                    default:
                        Debug.LogWarning($"Uknown stat with id {stat.id}");
                        break;
                }
            }
        }
    }
}
