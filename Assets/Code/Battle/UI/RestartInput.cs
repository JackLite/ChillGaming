using Battle.Signals;
using UnityEngine.UI;
using Zenject;

namespace Battle.UI
{
    class RestartInput : IInitializable
    {
        private readonly Button _restartWithoutBuffs;
        private readonly Button _restartWithBuffs;
        private readonly SignalBus _signalBus;

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
