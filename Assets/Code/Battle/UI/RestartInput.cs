using Battle.Signals;
using UnityEngine.UI;
using Zenject;

namespace Battle.UI
{
    class RestartInput : IInitializable
    {
        private Button _restartWithoutBuffs;
        private Button _restartWithBuffs;
        private SignalBus _signalBus;

        public RestartInput(Button restartWithoutBuffs, Button restartWithBuffs, SignalBus signalBus)
        {
            _restartWithoutBuffs = restartWithoutBuffs;
            _restartWithBuffs = restartWithBuffs;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _restartWithoutBuffs.onClick.AddListener(() => _signalBus.Fire(new BattleRestartedSignal(false)));
            _restartWithBuffs.onClick.AddListener(() => _signalBus.Fire(new BattleRestartedSignal(true)));
        }
    }
}
