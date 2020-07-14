using Battle.Player.Buffs;
using Battle.Player.Stats;
using System;
using UnityEngine;
using Zenject;

namespace Battle.Player
{
    class PlayerDataFactory : IFactory<PlayerData>
    {
        private BattleData _battleData;

        public PlayerDataFactory(BattleData battleData)
        {
            _battleData = battleData;
        }

        public PlayerData Create()
        {
            var stats = CreateStatsContainer();
            var buffs = CreateBuffsContainer();
            var playerData = new PlayerData(stats, buffs);
            return playerData;
        }

        private StatsContainer CreateStatsContainer()
        {
            var stats = new Stat[_battleData.Data.stats.Length];
            for (var i = 0; i < stats.Length; i++)
                stats[i] = (Stat) _battleData.Data.stats[i].Clone();

            return new StatsContainer(stats);
        }

        private BuffsContainer CreateBuffsContainer()
        {
            return new BuffsContainer(new Buff[0]);
//            var buffsCount = UnityEngine.Random.Range(_battleData.Data.settings.buffCountMin, )
        }
    }
}
