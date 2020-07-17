using Battle.Player;
using Battle.Signals;
using Battle.UI;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Battle.Installers
{
    class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Settings settings;

        public override void InstallBindings()
        {
            InstallPlayer();

            InstallUI();

        }

        private void InstallUI()
        {
            Container.DeclareSignal<StatsChangedSignal>();
            Container.DeclareSignal<BuffsChangedSignal>();

            Container.BindFactory<int, StatBar, StatBar.Factory>()
                .FromNewComponentOnNewPrefab(settings.statPrefab)
                .WithGameObjectName("Stat")
                .UnderTransform(settings.panel);

            Container.BindInterfacesAndSelfTo<StatController>().AsSingle();
            Container.BindSignal<StatsChangedSignal>().ToMethod<StatController>(x => x.OnStatsChanged).FromResolve();

            Container.BindFactory<BuffBar, BuffBar.Factory>()
                .FromNewComponentOnNewPrefab(settings.statPrefab)
                .WithGameObjectName("Buff")
                .UnderTransform(settings.panel);

            Container.BindInterfacesAndSelfTo<BuffController>().AsSingle();

            Container.BindSignal<BuffsChangedSignal>().ToMethod<BuffController>(x => x.Reset).FromResolve();
        }

        private void InstallPlayer()
        {
            Container.BindFactory<bool, PlayerData, PlayerData.Factory>()
                .FromFactory<PlayerDataFactory>();

            Container.BindInterfacesAndSelfTo<PlayerAnimationHandler>()
                .AsSingle()
                .WithArguments(settings.modelAnimator);

            Container.BindInterfacesAndSelfTo<PlayerInput>()
                .AsSingle()
                .WithArguments(settings.attackButton);

            Container.BindInterfacesAndSelfTo<PlayerController>()
                .AsSingle();

            Container.BindSignal<PlayerAttackSignal>()
                .ToMethod<PlayerController>(x => x.OnAttack).FromResolve();

            Container.BindSignal<BattleRestartedSignal>()
                .ToMethod<PlayerController>(x => x.ReInitialize).FromResolve();

            Container.BindSignal<SuccessAttackSignal>()
                .ToMethod<PlayerController>(x => x.OnSuccessAttack).FromResolve();
        }

        [Serializable]
        private struct Settings
        {
            public Button attackButton;
            public Animator modelAnimator;

            [Header("UI")]
            public GameObject healthBarPrefab;
            public Transform panel;
            public GameObject statPrefab;
        }
    }
}
