using Battle.Player.Stats;
using Battle.Signals;
using System;
using UnityEngine;
using Zenject;

namespace Battle.Player
{
    class PlayerController
    {
        private PlayerAnimationHandler _animationHandler;
        private PlayerData _playerData;
        private SignalBus _signalBus;

        public PlayerController(
            PlayerAnimationHandler animationHandler, 
            PlayerData.Factory playerDataFactory,
            SignalBus signalBus)
        {
            _animationHandler = animationHandler;
            _playerData = playerDataFactory.Create();
            _signalBus = signalBus;
        }

        public void OnAttack(PlayerAttackSignal signal)
        {
            if (_playerData.Stats.Health.value <= 0 || signal.FromWho.GetHealth() <= 0) return;
            if (signal.FromWho != this)
            {
                TakeDamage(signal.FromWho.GetDamage());
                return;
            }
            _animationHandler.PlayAttackAnimation();
        }

        private void TakeDamage(float damage)
        {
            if(_playerData.Stats.Health.value == 0) return;

            _playerData.Stats.Health.value -= damage;
            if(_playerData.Stats.Health.value <= 0)
            {
                _playerData.Stats.Health.value = 0;
            }

            _animationHandler.SetHealth(_playerData.Stats.Health.value);
            _signalBus.Fire(new HealthChangeSignal(this));
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
    }
}
