using LgkProductions.Inspector.MetaData;

namespace LgkProductions.Inspector.UnitTests.MetaData;

public class MetaDataTest
{
    [Fact]
    public void CollectMetaData()
    {
        var collection = MetaDataCollector.Collect<TestModel>(new()
        {
            IncludeFields = false
        });

        var members = collection.ToArray();
    }
}
