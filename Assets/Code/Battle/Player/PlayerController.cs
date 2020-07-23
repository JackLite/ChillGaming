using Battle.Player.Buffs;
using Battle.Player.Stats;
using Battle.Signals;
using UnityEngine;
using Zenject;

namespace Battle.Player
{
    public class PlayerController : IInitializable
    {
        private readonly PlayerData.Factory _playerDataFactory;
        private readonly SignalBus _signalBus;
        private PlayerData _playerData;

        public float Health => _playerData.Stats.Health.value;
        public float Damage => _playerData.Stats.Damage.value;
        public StatsContainer StatsContainer => _playerData.Stats;
        public BuffsContainer BuffsContainer => _playerData.Buffs;

        public PlayerController(PlayerData.Factory playerDataFactory, SignalBus signalBus)
        {
            _playerDataFactory = playerDataFactory;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _playerData = _playerDataFactory.Create(true);
            ApplyBuffs();
        }

        public void OnAttack(PlayerAttackedSignal signal)
        {
            if (signal.FromWho == this) return;

            if (Health <= 0 || signal.FromWho.Health <= 0) return;

            TakeDamage(signal.FromWho);
        }

        public void OnSuccessAttack(SuccessAttackedSignal signal)
        {
            if (signal.FromWho != this) return;

            if (Health <= 0 || signal.Target.Health <= 0) return;

            var fixedVampirism = Mathf.Clamp(_playerData.Stats.Vampirism.value, 0, 
                _playerData.Stats.Vampirism.value);

            _playerData.Stats.Health.value += signal.Damage * fixedVampirism / 100;
            _signalBus.Fire(new StatsChangedSignal(this));
        }

        private void TakeDamage(PlayerController fromWho)
        {
            if (_playerData.Stats.Health.value == 0) return;

            var adjustDamage = CalculateAdjustDamage(fromWho.Damage);

            var resultHealth = _playerData.Stats.Health.value - adjustDamage;
            _playerData.Stats.Health.value = Mathf.Clamp(resultHealth, 0, resultHealth);

            _signalBus.Fire(new StatsChangedSignal(this));
            _signalBus.Fire(new SuccessAttackedSignal(fromWho, this, adjustDamage));
        }

        private float CalculateAdjustDamage(float damage)
        {
            var adjustDamage = damage - damage * _playerData.Stats.Armor.value / 100;
            adjustDamage = Mathf.Clamp(adjustDamage, 0, adjustDamage);
            return adjustDamage;
        }

        public void ReInitialize(BattleRestartedSignal signal)
        {
            _playerData = _playerDataFactory.Create(signal.WithBuffs);
            ApplyBuffs();
            _signalBus.Fire(new StatsChangedSignal(this));
            _signalBus.Fire(new BuffsChangedSignal(this));
        }

        private void ApplyBuffs()
        {
            foreach (var buff in _playerData.Buffs.Buffs)
            {
                foreach (var statMod in buff.stats)
                {
                    ModifyStat(statMod);
                }
            }
        }

        private void ModifyStat(BuffStat statMod)
        {
            foreach (var stat in _playerData.Stats.Stats)
            {
                if (stat.Value.id == statMod.statId)
                {
                    stat.Value.value += statMod.value;
                    return;
                }
            }
        }
    }
}
