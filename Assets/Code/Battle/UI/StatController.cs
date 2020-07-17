using System.Collections.Generic;
using System.Linq;
using Battle.Player;
using Battle.Signals;
using UnityEngine;
using Zenject;

namespace Battle.UI
{
    class StatController : IInitializable
    {
        private StatBar.Factory _factory;
        private PlayerController _player;
        private IList<StatBar> _statBars;

        public StatController(StatBar.Factory factory, PlayerController player)
        {
            _factory = factory;
            _player = player;
            _statBars = new List<StatBar>(_player.GetStatContainer().Stats.Length);
        }

        public void Initialize()
        {
            foreach(var stat in _player.GetStatContainer().Stats)
            {
                var statBar = _factory.Create(stat.id);
                statBar.SetIcon(stat.icon);
                _statBars.Add(statBar);
            }
            UpdateUI();
        }

        public void OnStatsChanged(StatsChangedSignal signal)
        {
            if (signal.Player != _player) return;
            UpdateUI();
        }

        private void UpdateUI()
        {
            foreach (var stat in _player.GetStatContainer().Stats)
            {
                var statBar = GetStatBar(stat.id);
                statBar.SetText(stat.value.ToString("0.0"));
            }
        }

        private StatBar GetStatBar(int statId)
        {
            return _statBars.Where(sb => sb.StatId == statId).First();
        }
    }
}
