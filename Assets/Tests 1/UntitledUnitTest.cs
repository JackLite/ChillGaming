using Zenject;
using NUnit.Framework;
using Battle;
using System.IO;

[TestFixture]
public class UntitledUnitTest : ZenjectUnitTestFixture
{
    [SetUp]
    public void Install()
    {

        var text = File.ReadAllText("Assets/Resources/data.txt");
        Container.BindInterfacesAndSelfTo<BattleData>().AsSingle().WithArguments(text);
    }

    [Test]
    public void RunTest1()
    {
        var battleData = Container.Resolve<BattleData>();
        Assert.AreEqual(5, battleData.Data.settings.buffCountMax);
    }
}