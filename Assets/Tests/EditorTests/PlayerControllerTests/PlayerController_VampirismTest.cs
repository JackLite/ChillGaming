using Battle;
using Battle.Player;
using Battle.Signals;
using NUnit.Framework;
using UnityEngine;
using Zenject;

namespace Tests.EditorTests.PlayerControllerTests
{
    [TestFixture]
    class PlayerController_VampirismTest : ZenjectUnitTestFixture
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
            [Values(100)] float targetHealth,
            [Values(100)] float attakerHealth,
            [Values(0, 25, 50, 100, 200, -20)] float vampirism)
        {
            Arrange(damage, targetHealth, attakerHealth, vampirism);
            Act();
            MakeAssert(damage, targetHealth, attakerHealth, vampirism);
        }

        private void Arrange(float damage, float targetHealth, float attackerHealth, float vampirism)
        {
            var data = Container.Resolve<BattleData>();
            data.Data.settings.buffCountMax = 0;

            var signalBus = Container.Resolve<SignalBus>();

            _playerUnderAttack = new PlayerController(Container.Resolve<PlayerData.Factory>(), signalBus);
            _playerUnderAttack.Initialize();

            _attacker = new PlayerController(Container.Resolve<PlayerData.Factory>(), signalBus);
            _attacker.Initialize();

            _playerUnderAttack.StatsContainer.Health.value = targetHealth;

            _attacker.StatsContainer.Health.value = attackerHealth;
            _attacker.StatsContainer.Damage.value = damage;
            _attacker.StatsContainer.Vampirism.value = vampirism;
        }

        private void Act()
        {
            var damage = CalculateAdjustDamage(_attacker.Damage, _playerUnderAttack.StatsContainer.Damage.value, _playerUnderAttack.Health);
            _attacker.OnSuccessAttack(new SuccessAttackedSignal(_attacker, _playerUnderAttack, damage));
        }

        private void MakeAssert(float damage, float targetHealth, float attackerHealth, float attackerVampirism)
        {
            var fixedVammpirism = Mathf.Clamp(attackerVampirism, 0, attackerVampirism);
            var targetArmor = _playerUnderAttack.StatsContainer.Armor.value;
            var adjustDamage = CalculateAdjustDamage(damage, targetArmor, targetHealth);

            var expectedHealth = attackerHealth + adjustDamage * fixedVammpirism / 100f;

            Assert.That(expectedHealth, Is.EqualTo(_attacker.Health));
        }

        private float CalculateAdjustDamage(float damage, float targetArmor, float targetHealth)
        {
            var fixedDamage = Mathf.Clamp(damage, 0, damage);
            var adjustDamage = fixedDamage - fixedDamage * targetArmor / 100f;
            return Mathf.Clamp(adjustDamage, 0, targetHealth);
        }
    }
}
