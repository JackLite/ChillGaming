using System;
using Battle.Signals;
using Battle.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Battle.Installers
{
    class BattleInstaller : MonoInstaller
    {
        [SerializeField]
        private Settings settings;

        public override void InstallBindings()
        {
            InstallBattleData();
            InstallUI();
            InstallSignals();
        }

        private void InstallBattleData()
        {
            Container.BindInterfacesAndSelfTo<BattleData>()
                .AsSingle()
                .WithArguments(settings.data.text)
                .NonLazy();
        }

        private void InstallUI()
        {
            Container.BindInterfacesAndSelfTo<RestartInput>()
                .AsSingle()
                .WithArguments(settings.restartWithoutBuffs, settings.restartWithBuffs);
        }

        private void InstallSignals()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<PlayerAttackedSignal>();
            Container.DeclareSignal<BattleRestartedSignal>();
            Container.DeclareSignal<SuccessAttackedSignal>();
        }

        [Serializable]
        public struct Settings
        {
            public TextAsset data;
            public Button restartWithoutBuffs;
            public Button restartWithBuffs;
        }
    }
}
