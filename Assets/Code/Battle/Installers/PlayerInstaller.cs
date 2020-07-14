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
            Container.DeclareSignal<HealthChangeSignal>();

            Container.BindFactory<StatBar, StatBar.Factory>()
                .FromComponentInNewPrefab(settings.statPrefab)
                .WithGameObjectName("Stat")
                .UnderTransform(settings.panel);

            Container.BindInterfacesAndSelfTo<StatController>().AsSingle();
        }

        private void InstallPlayer()
        {
            Container.BindFactory<PlayerData, PlayerData.Factory>()
                .FromFactory<PlayerDataFactory>();

            Container.BindInterfacesAndSelfTo<PlayerAnimationHandler>()
                .AsSingle()
                .WithArguments(settings.modelAnimator);

            Container.BindInterfacesAndSelfTo<PlayerId>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerInput>()
                .AsSingle()
                .WithArguments(settings.attackButton);

            Container.BindInterfacesAndSelfTo<PlayerController>()
                .AsSingle();

            Container.BindSignal<PlayerAttackSignal>()
                .ToMethod<PlayerController>(x => x.OnAttack).FromResolve();
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
