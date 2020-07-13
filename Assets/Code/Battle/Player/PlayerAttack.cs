using Battle.Signals;
using UnityEngine;
using Zenject;

namespace Battle.Player
{
    class PlayerAttack : IAttack
    {
        private PlayerId _playerId;
        private PlayerAttackHandler _playerAttackHandler;
        private SignalBus _signalBus;

        public PlayerAttack(
            PlayerId playerId, 
            PlayerAttackHandler playerAttackHandler, 
            SignalBus signalBus)
        {
            _playerId = playerId;
            _playerAttackHandler = playerAttackHandler;
            _signalBus = signalBus;
        }

        public void Attack()
        {
            _signalBus.Fire(new PlayerAttackSignal(_playerId, default));
            _playerAttackHandler.PlayAttackAnimation();
        }
    }
}
