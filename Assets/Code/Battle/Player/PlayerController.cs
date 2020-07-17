using Battle.Player.Buffs;
using Battle.Player.Stats;
using Battle.Signals;
using System;
using UnityEngine;
using Zenject;

namespace Battle.Player
{
    class PlayerController : IInitializable
    {
        private PlayerAnimationHandler _animationHandler;
        private PlayerData _playerData;
        private PlayerData.Factory _playerDataFactory;
        private SignalBus _signalBus;

        public PlayerController(
            PlayerAnimationHandler animationHandler,
            PlayerData.Factory playerDataFactory,
            SignalBus signalBus)
        {
            _animationHandler = animationHandler;
            _playerDataFactory = playerDataFactory;
            _playerData = playerDataFactory.Create(true);
            _signalBus = signalBus;
        }

        public void OnAttack(PlayerAttackSignal signal)
        {
            if (signal.FromWho == this)
            {
                _animationHandler.PlayAttackAnimation();
                return;
            }

            if (GetHealth() <= 0 || signal.FromWho.GetHealth() <= 0) return;

            TakeDamage(signal.FromWho);
        }

        public void OnSuccessAttack(SuccessAttackSignal signal)
        {
            if (signal.FromWho != this) return;

            if (GetHealth() <= 0 || signal.Target.GetHealth() <= 0) return;

            var damage = signal.Damage;
            _playerData.Stats.Health.value += damage * _playerData.Stats.Vampirism.value / 100;
            _signalBus.Fire(new StatsChangedSignal(this));
        }

        private void TakeDamage(PlayerController fromWho)
        {
            if (_playerData.Stats.Health.value == 0) return;

            var damage = fromWho.GetDamage();

            var adjustDamage = damage - damage * _playerData.Stats.Armor.value / 100;

            if (adjustDamage < 0) adjustDamage = 0;
            _playerData.Stats.Health.value -= adjustDamage;

            if (_playerData.Stats.Health.value <= 0)
            {
                _playerData.Stats.Health.value = 0;
            }

            _animationHandler.SetHealth(_playerData.Stats.Health.value);
            _signalBus.Fire(new StatsChangedSignal(this));
            _signalBus.Fire(new SuccessAttackSignal(fromWho, this, adjustDamage));
        }

        public float GetDamage()
        {
            return _playerData.Stats.Damage.value;
        }

        public float GetHealth()
        {
            return _playerData.Stats.Health.value;
        }

        public StatsContainer GetStatContainer()
        {
            return _playerData.Stats;
        }

        public BuffsContainer GetBuffsContainer()
        {
            return _playerData.Buffs;
        }

        public void ReInitialize(BattleRestartedSignal signal)
        {
            _playerData = _playerDataFactory.Create(signal.WithBuffs);
            ApplyBuffs();
            _animationHandler.SetHealth(_playerData.Stats.Health.value);
            _signalBus.Fire(new StatsChangedSignal(this));
            _signalBus.Fire(new BuffsChangedSignal(this));
        }

        public void Initialize()
        {
            ApplyBuffs();
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
                if (stat.id == statMod.statId)
                {
                    stat.value += statMod.value;
                    return;
                }
            }
        }
    }
}
