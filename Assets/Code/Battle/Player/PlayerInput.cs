using Battle.Signals;
using UnityEngine.UI;
using Zenject;

namespace Battle.Player
{
    class PlayerInput : IInitializable
    {
        private readonly Button _attackBtn;
        private readonly PlayerController _playerController;
        private readonly SignalBus _signalBus;

        public PlayerInput(Button attackButton, PlayerController playerController, SignalBus signalBus)
        {
            _attackBtn = attackButton;
            _playerController = playerController;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _attackBtn.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _signalBus.Fire(new PlayerAttackedSignal(_playerController));
        }
    }
}
