using System.Collections.Generic;
using System.Linq;
using Battle.Player.Buffs;
using Battle.Player.Stats;
using Zenject;

namespace Battle.Player
{
    class PlayerDataFactory : IFactory<bool, PlayerData>
    {
        private readonly BattleData _battleData;

        public PlayerDataFactory(BattleData battleData) => _battleData = battleData;

        public PlayerData Create(bool withBuffs)
        {
            var stats = CreateStatsContainer();
            var buffs = !withBuffs ? new BuffsContainer(new Buff[0]) : CreateBuffsContainer();
            var playerData = new PlayerData(stats, buffs);
            return playerData;
        }

        private StatsContainer CreateStatsContainer()
        {
            var stats = new Stat[_battleData.Data.stats.Length];
            for (var i = 0; i < stats.Length; i++)
                stats[i] = (Stat)_battleData.Data.stats[i].Clone();

            return new StatsContainer(stats);
        }

        private BuffsContainer CreateBuffsContainer()
        {
            var settings = _battleData.Data.settings;
            var buffsCount = UnityEngine.Random.Range(settings.buffCountMin, settings.buffCountMax + 1);
            var buffs = GenerateRandomBuffs(_battleData.Data.buffs, buffsCount, settings.allowDuplicateBuffs);
            return new BuffsContainer(buffs.ToArray());
        }

        private IEnumerable<Buff> GenerateRandomBuffs(IList<Buff> buffs, int count, bool isAllowToDuplicate)
        {
            var generatedBuffs = new LinkedList<Buff>();
            if (count == 0) return generatedBuffs;

            var numbers = GenerateInts(count, 0, buffs.Count, isAllowToDuplicate);
            foreach (var num in numbers)
            {
                generatedBuffs.AddLast(buffs[num]);
            }
            return generatedBuffs;
        }

        private IEnumerable<int> GenerateInts(int count, int min, int max, bool withDuplicates)
        {
            if (count > max && !withDuplicates)
                count = max;

            var res = new LinkedList<int>();
            var filled = 0;
            while (filled < count)
            {
                var num = UnityEngine.Random.Range(min, max);
                if (!withDuplicates && res.Contains(num)) continue;
                res.AddLast(num);
                filled++;
            }
            return res.OrderBy(x => x);
        }
    }
}
