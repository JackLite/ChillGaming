using Battle.Signals;
using UnityEngine;

namespace Battle.Player
{
    class PlayerAnimationHandler
    {
        private readonly Animator _animator;
        private readonly PlayerController _player;

        public PlayerAnimationHandler(Animator animator, PlayerController player)
        {
            _animator = animator;
            _player = player;
        }

        public void PlayAttackAnimation(SuccessAttackedSignal signal)
        {
            if (signal.FromWho != _player) return;
            _animator.SetTrigger("Attack");
        }

        public void UpdateHealth(StatsChangedSignal signal)
        {
            if (signal.Player != _player) return;
            _animator.SetInteger("Health", (int)signal.Player.GetHealth());
        }
    }
}
