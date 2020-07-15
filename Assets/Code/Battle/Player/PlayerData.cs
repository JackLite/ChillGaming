using Battle.Player.Buffs;
using Battle.Player.Stats;
using System;
using UnityEngine;
using Zenject;

namespace Battle.Player
{
    class PlayerData
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
