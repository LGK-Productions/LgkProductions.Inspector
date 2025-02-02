using LgkProductions.Inspector.UnitTests.TestUtils;

namespace LgkProductions.Inspector.UnitTests;

public class ListTest
{
    [Fact]
    public void Tick_ShouldNotFail_OnIndexer()
    {
        TestTickProvider tickProvider = new();

        List<int> model = [1, 2, 3];
        _ = Inspector.Attach(model, tickProvider);

        tickProvider.TriggerTick();

        // We should not throw
    }

    sealed class ListModel
    {
        public List<int> List { get; set; } = [1, 2, 3];
    }
}
