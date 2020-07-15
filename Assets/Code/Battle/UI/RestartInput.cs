using Battle.Signals;
using UnityEngine.UI;
using Zenject;

namespace Battle.UI
{
    class RestartInput : IInitializable
    {
        private Button _restartWithoutBuffs;
        private SignalBus _signalBus;

        public RestartInput(Button restartWithoutBuffs, SignalBus signalBus)
        {
            _restartWithoutBuffs = restartWithoutBuffs;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _restartWithoutBuffs.onClick.AddListener(() => _signalBus.Fire(new BattleRestartedSignal(false)));
        }
    }
}
