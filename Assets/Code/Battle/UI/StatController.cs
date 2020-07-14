using Battle.Player;
using UnityEngine;
using Zenject;

namespace Battle.UI
{
    class StatController : IInitializable
    {
        private StatBar.Factory _factory;
        private PlayerController _player;

        public StatController(StatBar.Factory factory, PlayerController player)
        {
            _factory = factory;
            _player = player;
        }

        public void Initialize()
        {
            foreach(var stat in _player.GetStatContainer().Stats)
            {
                var statBar = _factory.Create();
                statBar.SetText(stat.value.ToString());
                statBar.SetIcon(stat.icon);
            }
        }
    }
}
