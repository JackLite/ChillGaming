using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battle.Player;
using Zenject;

namespace Battle.UI
{
    class BuffController : IInitializable
    {
        private BuffBar.Factory _factory;
        private PlayerController _player;
        private IList<BuffBar> _buffBars;

        public BuffController(BuffBar.Factory factory, PlayerController player, BattleData battleData)
        {
            _factory = factory;
            _player = player;
            _buffBars = new List<BuffBar>();
        }

        public void Initialize()
        {
            foreach (var buff in _player.GetBuffsContainer().Buffs)
            {
                var bar = _factory.Create();
                bar.SetIcon(buff.icon);
                bar.SetText(buff.title);
                _buffBars.Add(bar);
            }
        }

        public void Reset()
        {
            foreach(var bar in _buffBars)
            {
                bar.Delete();
            }
            _buffBars.Clear();
            Initialize();
        }
    }
}
