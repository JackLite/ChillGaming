using Battle.Signals;
using UnityEngine;
using Zenject;

namespace Battle.Installers
{
    class BattleInstaller : MonoInstaller
    {
        public TextAsset settings;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.Bind<TextAsset>().FromInstance(settings).AsSingle().WithConcreteId ("BattleSettings");
            Container.BindInterfacesAndSelfTo<BattleData>().AsSingle().NonLazy();

            Container.DeclareSignal<PlayerAttackSignal>();
        }
    }
}
