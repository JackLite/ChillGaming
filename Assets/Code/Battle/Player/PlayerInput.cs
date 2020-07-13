using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Battle.Player
{
    class PlayerInput : IInitializable
    {
        private Button _attackBtn;
        private IAttack _playerAttack;
        public PlayerInput(Button attackButton, IAttack playerAttack)
        {
            _attackBtn = attackButton;
            _playerAttack = playerAttack;
        }

        public void Initialize()
        {
            _attackBtn.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _playerAttack.Attack();
        }
    }
}
