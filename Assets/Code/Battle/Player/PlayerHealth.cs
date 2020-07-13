using Battle.Signals;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Battle.Player
{
    class PlayerHealth : IInitializable
    {
        private PlayerId _playerId;
        private SignalBus _signalBus;

        public PlayerHealth(PlayerId playerId, SignalBus signalBus)
        {
            _playerId = playerId;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<PlayerAttackSignal>(OnAttack);
        }

        private void OnAttack(PlayerAttackSignal attackSignal)
        {
            if (attackSignal.FromWho == _playerId) return;

        }
    }
}
