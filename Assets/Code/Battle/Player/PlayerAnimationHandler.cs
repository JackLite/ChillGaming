using System;
using UnityEngine;

namespace Battle.Player
{
    class PlayerAnimationHandler
    {
        private readonly Animator _animator;

        public PlayerAnimationHandler(Animator animator) => _animator = animator;

        public void PlayAttackAnimation()
        {
            _animator.SetTrigger("Attack");
        }

        public void SetHealth(float health)
        {
            _animator.SetInteger("Health", (int)health);
        }
    }
}
