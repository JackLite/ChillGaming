using Battle;
using Battle.Player;
using Battle.Signals;
using NUnit.Framework;
using UnityEngine;
using Zenject;

namespace Tests.EditorTests.PlayerControllerTests
{
    [TestFixture]
    class PlayerController_UnderAttackTest : ZenjectUnitTestFixture
    {
        private PlayerController _playerUnderAttack;
        private PlayerController _attacker;

        [SetUp]
        public void Install()
        {
            var text = Helper.ReadTestData();
            Container.BindInterfacesAndSelfTo<BattleData>().AsSingle().WithArguments(text);
            SignalBusInstaller.Install(Container);
            Container.BindFactory<bool, PlayerData, PlayerData.Factory>()
               .FromFactory<PlayerDataFactory>();
            Container.DeclareSignal<StatsChangedSignal>();
            Container.DeclareSignal<SuccessAttackedSignal>();
        }

        [Test, Combinatorial]
        public void Test([Values(100, 200, 400, 600, 0, -9999)] float damage,
            [Values(0, 100, 200, 400, 100000)] float health)
        {
            Arrange(damage, health);
            Act();
            MakeAssert(damage, health);
        }

        private void Arrange(float damage, float health)
        {
            var data = Container.Resolve<BattleData>();
            data.Data.settings.buffCountMax = 0;

            var signalBus = Container.Resolve<SignalBus>();

            _playerUnderAttack = new PlayerController(Container.Resolve<PlayerData.Factory>(), signalBus);
            _playerUnderAttack.Initialize();

            _attacker = new PlayerController(Container.Resolve<PlayerData.Factory>(), signalBus);
            _attacker.Initialize();

            _playerUnderAttack.StatsContainer.Health.value = health;

            _attacker.StatsContainer.Health.value = 100;
            _attacker.StatsContainer.Damage.value = damage;
        }

        private void Act()
        {
            _playerUnderAttack.OnAttack(new PlayerAttackedSignal(_attacker));
        }

        private void MakeAssert(float damage, float health)
        {
            var fixedDamage = Mathf.Clamp(damage, 0, damage);
            var expectedHealth = health - fixedDamage * 0.75f;
            expectedHealth = Mathf.Clamp(expectedHealth, 0, expectedHealth);

            Assert.That(expectedHealth, Is.EqualTo(_playerUnderAttack.Health));
        }
    }
}
