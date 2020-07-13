using Battle.Player;
using Battle.Signals;
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
            Container.BindInterfacesAndSelfTo<PlayerAttackHandler>().FromInstance(settings.playerAttackHandler);
            Container.BindInterfacesAndSelfTo<PlayerId>().AsSingle();
            Container.BindInterfacesTo<PlayerAttack>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle().WithArguments(settings.attackButton);

            Container.BindInterfacesAndSelfTo<PlayerHealth>().AsSingle();

        }

        [Serializable]
        private struct Settings
        {
            public Button attackButton;
            public PlayerAttackHandler playerAttackHandler;
        }
    }
}
