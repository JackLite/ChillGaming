using Battle;
using Battle.Player;
using NUnit.Framework;
using Zenject;

namespace Tests.EditorTests
{
    [TestFixture]
    public class PlayerDataFactoryTest : ZenjectUnitTestFixture
    {

        [SetUp]
        public void Install()
        {
            var text = Helper.ReadTestData();
            Container.BindInterfacesAndSelfTo<BattleData>().AsSingle().WithArguments(text);
        }

        [Test]
        public void TestStats()
        {
            var data = Container.Resolve<BattleData>();
            var factory = new PlayerDataFactory(data);
            var playerData = factory.Create(false);
            Assert.AreEqual(100, playerData.Stats.Health.value);
            Assert.AreEqual(25, playerData.Stats.Armor.value);
            Assert.AreEqual(25, playerData.Stats.Damage.value);
            Assert.AreEqual(0, playerData.Stats.Vampirism.value);
        }

        [Test]
        public void TestBuffsCountGreaterMaxAndNotAllowDuplicates()
        {
            var data = Container.Resolve<BattleData>();
            data.Data.settings.buffCountMin = 7;
            data.Data.settings.buffCountMax = 7;
            var factory = new PlayerDataFactory(data);
            var playerData = factory.Create(true);
            Assert.AreEqual(4, playerData.Buffs.Buffs.Length);
        }

        [Test]
        public void TestBuffsCountGreaterMaxAndAllowDuplicates()
        {
            var data = Container.Resolve<BattleData>();
            data.Data.settings.buffCountMin = 7;
            data.Data.settings.buffCountMax = 7;
            data.Data.settings.allowDuplicateBuffs = true;
            var factory = new PlayerDataFactory(data);
            var playerData = factory.Create(true);
            Assert.AreEqual(7, playerData.Buffs.Buffs.Length);
        }

        [Test]
        public void TestBuffsMinGreaterThenMax()
        {
            var data = Container.Resolve<BattleData>();
            data.Data.settings.buffCountMin = 7;
            data.Data.settings.buffCountMax = 0;
            data.Data.settings.allowDuplicateBuffs = true;
            var factory = new PlayerDataFactory(data);
            var playerData = factory.Create(true);
            Assert.AreEqual(0, playerData.Buffs.Buffs.Length);
        }

        [Test]
        public void TestWithoutBuffs()
        {
            var data = Container.Resolve<BattleData>();
            var factory = new PlayerDataFactory(data);
            data.Data.settings.buffCountMin = 1;
            data.Data.settings.buffCountMax = 7;
            var playerData = factory.Create(false);
            Assert.AreEqual(0, playerData.Buffs.Buffs.Length);
        }
    }
}