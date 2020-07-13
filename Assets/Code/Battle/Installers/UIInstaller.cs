using Battle.Signals;
using UnityEngine;
using Zenject;

namespace Battle.Installers
{
    class UIInstaller : MonoInstaller
    {
        public GameObject healthBarPrefab;

        public override void InstallBindings()
        {
            Container.DeclareSignal<HealthChangeSignal>();
        }
    }
}
