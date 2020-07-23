using Battle;
using Battle.Player.Stats;
using NUnit.Framework;
using System.Linq;
using Zenject;

namespace Tests.EditorTests
{
    [TestFixture]
    public class BattleDataTest : ZenjectUnitTestFixture
    {
        [SetUp]
        public void Install()
        {
            var text = Helper.ReadTestData();
            Container.BindInterfacesAndSelfTo<BattleData>().AsSingle().WithArguments(text);
        }

        [Test]
        public void TestSettingsSection()
        {
            var battleData = Container.Resolve<BattleData>();
            Assert.AreEqual(5, battleData.Data.settings.buffCountMax);
            Assert.AreEqual(0, battleData.Data.settings.buffCountMin);
            Assert.IsFalse(battleData.Data.settings.allowDuplicateBuffs);
        }

        #region StatsTests

        [Test]
        public void TestStatsCount()
        {
            var battleData = Container.Resolve<BattleData>();
            Assert.AreEqual(4, battleData.Data.stats.Length);
        }

        [Test]
        public void TestHealthStat()
        {
            var stat = GetStat(StatsContainer.healthId);
            Assert.AreEqual(100, stat.value);
        }

        [Test]
        public void TestArmorStat()
        {
            var stat = GetStat(StatsContainer.armorId);
            Assert.AreEqual(25, stat.value);
        }

        [Test]
        public void TestDamageStat()
        {
            var stat = GetStat(StatsContainer.damageId);
            Assert.AreEqual(25, stat.value);
        }

        [Test]
        public void TestVampirismStat()
        {
            var stat = GetStat(StatsContainer.vampirismId);
            Assert.AreEqual(0, stat.value);
        }

        private Stat GetStat(int statId)
        {
            var battleData = Container.Resolve<BattleData>();
            return battleData.Data.stats.Where(s => s.id == statId).First();
        }

        #endregion

        [Test]
        public void TestBuffs()
        {
            var battleData = Container.Resolve<BattleData>();
            Assert.AreEqual(4, battleData.Data.buffs.Length);
            foreach (var buff in battleData.Data.buffs)
                Assert.Greater(buff.stats.Length, 0);
        }
    }
}