namespace LgkProductions.Inspector.UnitTests;

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
