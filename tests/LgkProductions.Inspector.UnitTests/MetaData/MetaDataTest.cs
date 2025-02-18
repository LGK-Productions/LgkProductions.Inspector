using LgkProductions.Inspector.Attributes;
using LgkProductions.Inspector.MetaData;
using System.ComponentModel.DataAnnotations;

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

    [Fact]
    public void CollectMetaData_WithPostProcessor()
    {
        var collection = MetaDataCollector.Collect<TestModel>(new()
        {
            IncludeFields = false,
            MemberPostProcessor = (MetaDataMember member, ref bool shouldInclude) =>
            {
                if (member.Name != "Abc")
                    return;

                Assert.False(shouldInclude);
                member.Description = "Hi!";
                shouldInclude = true;
            }
        });

        var members = collection.ToArray();
        Assert.Contains(members, member => member.Name == "Abc" && member.Description == "Hi!");
    }

    [Fact]
    public void CollectMetaData_WithPostProcessorBuilder()
    {
        MetaDataCollection collection = MetaDataCollector.Collect<TestModel>(new()
        {
            IncludeFields = false,
            MemberPostProcessor = MetaDataCollector.PostProcessor<TestModel>()
                .Member(x => x.Abc2, new RequiredAttribute(), new BoxGroupAttribute("SomeGroup"))
                .Build()
        });

        var members = collection.ToArray();
        Assert.Contains(members, member => member.Name == "Abc2" && member.IsRequired && member.Group?.Title == "SomeGroup");
    }
}
