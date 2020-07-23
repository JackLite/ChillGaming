using Battle;
using Battle.Player;
using Battle.Player.Stats;
using NSubstitute;
using NUnit.Framework;
using Zenject;

namespace Tests.EditorTests.PlayerControllerTests
{
    [TestFixture]
    class PlayerController_ApplyBuffsTest : ZenjectUnitTestFixture
    {
        private PlayerController _player;

        [SetUp]
        public void Install()
        {
            var text = Helper.ReadTestData();
            Container.BindInterfacesAndSelfTo<BattleData>().AsSingle().WithArguments(text);
            Container.BindFactory<bool, PlayerData, PlayerData.Factory>()
               .FromFactory<PlayerDataFactory>();
            SignalBusInstaller.Install(Container);
        }

        [Test, Combinatorial]
        public void TestArmor([Values(0, 100, 200, 9999, -500)] float defaultValue,
            [Values(0, 200, 100, 20, -40, -200)] float buff,
            [Values(StatsContainer.armorId, StatsContainer.damageId,
            StatsContainer.healthId, StatsContainer.vampirismId)] int statId)
        {
            Arrange(defaultValue, buff, statId);
            Act();
            MakeAssert(defaultValue, buff, statId);
        }

        private void Arrange(float defaultValue, float buff, int statId)
        {
            var data = Container.Resolve<BattleData>();
            data.Data.settings.buffCountMin = 1;
            data.Data.settings.buffCountMax = 1;

            var realFactory = Container.Resolve<PlayerData.Factory>();
            var playerData = realFactory.Create(true);
            playerData.Buffs.Buffs[0].stats = new BuffStat[]
            {
                new BuffStat{ statId = statId, value = buff }
            };

            SetStatValue(ref playerData, statId, defaultValue);

            var fakeFactory = Substitute.For<PlayerData.Factory>();
            fakeFactory.Create(true).Returns(playerData);

            var signalBus = Container.Resolve<SignalBus>();

            _player = new PlayerController(fakeFactory, signalBus);
        }

        private void Act() => _player.Initialize();

        private void MakeAssert(float defaultValue, float buff, int statId)
        {
            var expectedHealth = defaultValue + buff;
            Assert.That(_player.StatsContainer.Stats[statId].value, Is.EqualTo(expectedHealth));
        }

        private void SetStatValue(ref PlayerData playerData, int statId, float value)
        {
            foreach (var stat in playerData.Stats.Stats)
            {
                if (stat.Value.id == statId)
                {
                    stat.Value.value = value;
                    break;
                }
            }
        }
    }
}
