using System.Collections.Generic;
using System.Linq;
using Battle.Player;
using Battle.Player.Stats;
using Battle.Signals;
using Zenject;

namespace Battle.UI
{
    class StatController : IInitializable
    {
        private readonly StatBar.Factory _factory;
        private readonly PlayerController _player;
        private IList<StatBar> _statBars;

        public StatController(StatBar.Factory factory, PlayerController player)
        {
            _factory = factory;
            _player = player;
        }

        public void Initialize()
        {
            _statBars = new List<StatBar>(_player.GetStatContainer().Stats.Count);
            foreach (var stat in _player.GetStatContainer().Stats)
            {
                var statBar = _factory.Create(stat.Value.id, stat.Value.icon);
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
                var statBar = GetStatBar(stat.Value.id);
                statBar.SetText(FormatStatValue(stat.Value));
            }
        }

        private string FormatStatValue(Stat stat)
        {
            return stat.id == StatsContainer.healthId ? stat.value.ToString("0.00") : stat.value.ToString();
        }

        private StatBar GetStatBar(int statId)
        {
            return _statBars.Where(sb => sb.StatId == statId).First();
        }
    }
}
