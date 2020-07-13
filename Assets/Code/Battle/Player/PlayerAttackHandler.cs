using UnityEngine;
using Zenject;

namespace Battle.Player
{
    class PlayerAttackHandler : MonoBehaviour
    {
        private Animator _animator;

        [Inject]
        public void Init()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayAttackAnimation()
        {
            _animator.SetTrigger("Attack");
        }
    }
}
