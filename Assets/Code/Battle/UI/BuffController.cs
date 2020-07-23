using System.Collections.Generic;
using Battle.Player;
using Zenject;

namespace Battle.UI
{
    class BuffController : IInitializable
    {
        private readonly BuffBar.Factory _factory;
        private readonly PlayerController _player;
        private readonly IList<BuffBar> _buffBars;

        public BuffController(BuffBar.Factory factory, PlayerController player)
        {
            _factory = factory;
            _player = player;
            _buffBars = new List<BuffBar>();
        }

        public void Initialize()
        {
            foreach (var buff in _player.BuffsContainer.Buffs)
            {
                var bar = _factory.Create(buff.title, buff.icon);
                _buffBars.Add(bar);
            }
        }

        public void Reset()
        {
            foreach (var bar in _buffBars)
                bar.Delete();

            _buffBars.Clear();
            Initialize();
        }
    }
}
