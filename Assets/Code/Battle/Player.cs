using Battle.Helpers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Battle
{
   /* class Player : IDamagable
    {
        private readonly Dictionary<int, Stat> _stats = new Dictionary<int, Stat>();
        private IList<Buff> _buffs;
        private SignalBus _signalBus;

        public Player (IList<Stat> stats, IList<Buff> buffs, SignalBus signalBus)
        {
            foreach(var stat in stats)
            {
                _stats.Add(stat.id, stat);
            }
            _buffs = buffs;
            _signalBus = signalBus;
        }

        public void TakeDamage(float amount)
        {
            var healthStat = _stats[StatHelper.GetStatId(StatType.health)];
            var newHealth = healthStat.value - amount;
            healthStat.value = Mathf.Clamp(newHealth, 0, newHealth);
            //if (healthStat.value <= 0) _signalBus.Fire<PlayerDiedSignal>(new PlayerDiedSignal(this));
        }

        private float CalculateAttackDamage()
        {
            var attackStat = _stats[StatHelper.GetStatId(StatType.attack)];
            StatHelper.ApplyBuffsToStat(ref attackStat, _buffs);
            return attackStat.value;
        }
    }*/
}
