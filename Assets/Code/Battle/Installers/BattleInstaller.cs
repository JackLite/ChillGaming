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
        public Settings settings;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.Bind<TextAsset>().FromInstance(settings.data)
                .AsSingle()
                .WithConcreteId ("BattleSettings");
            
            Container.BindInterfacesAndSelfTo<BattleData>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<RestartInput>()
                .AsSingle()
                .WithArguments(settings.restartWithoutBuffs, settings.restartWithBuffs);

            Container.DeclareSignal<PlayerAttackSignal>();
            Container.DeclareSignal<BattleRestartedSignal>();
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
