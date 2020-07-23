using System;
using Battle.Player;
using Battle.Signals;
using Battle.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Battle.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField]
        private Settings settings;

        public override void InstallBindings()
        {
            InitExecutionOrder();
            InstallPlayer();
            InstallUI();
            InstallSignals();
        }

        private void InitExecutionOrder()
        {
            Container.BindInitializableExecutionOrder<PlayerController>(-10);
        }

        private void InstallUI()
        {
            Container.BindFactory<int, string, StatBar, StatBar.Factory>()
                .FromNewComponentOnNewPrefab(settings.statPrefab)
                .WithGameObjectName("Stat")
                .UnderTransform(settings.panel);

            Container.BindInterfacesAndSelfTo<StatController>().AsSingle();

            Container.BindFactory<string, string, BuffBar, BuffBar.Factory>()
                .FromNewComponentOnNewPrefab(settings.statPrefab)
                .WithGameObjectName("Buff")
                .UnderTransform(settings.panel);

            Container.BindInterfacesAndSelfTo<BuffController>().AsSingle();

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

            Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle();
        }

        private void InstallSignals()
        {
            Container.DeclareSignal<StatsChangedSignal>();
            Container.DeclareSignal<BuffsChangedSignal>();

            Container.BindSignal<BuffsChangedSignal>()
                .ToMethod<BuffController>(x => x.Reset).FromResolve();

            Container.BindSignal<PlayerAttackedSignal>()
                .ToMethod<PlayerController>(x => x.OnAttack).FromResolve();

            Container.BindSignal<BattleRestartedSignal>()
                .ToMethod<PlayerController>(x => x.ReInitialize).FromResolve();

            Container.BindSignal<SuccessAttackedSignal>()
                .ToMethod<PlayerController>(x => x.OnSuccessAttack).FromResolve();
            
            Container.BindSignal<SuccessAttackedSignal>()
                .ToMethod<PlayerAnimationHandler>(x => x.PlayAttackAnimation).FromResolve();

            Container.BindSignal<StatsChangedSignal>()
                .ToMethod<StatController>(x => x.OnStatsChanged).FromResolve();

            Container.BindSignal<StatsChangedSignal>()
                .ToMethod<PlayerAnimationHandler>(x => x.UpdateHealth).FromResolve();

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
