using LgkProductions.Inspector.Attributes;
using LgkProductions.Inspector.MetaData;

namespace LgkProductions.Inspector.UnitTests.Attributes;

public class LayoutFlagsAttributeTest
{
    [Fact]
    public void LayoutFlags_Default_ShouldEqualZero()
    {
        Assert.Equal(default, LayoutFlags.Default);
    }

    [Fact]
    public void LayoutFlags_ExpandedInitially_ContainsFoldable()
    {
        Assert.Equal(LayoutFlags.Foldable | LayoutFlags.ExpandedInitially, LayoutFlags.ExpandedInitially);
        Assert.True(LayoutFlags.ExpandedInitially.HasFlag(LayoutFlags.Foldable));
    }

    [Fact]
    public void TestLayoutFlags()
    {
        var metaData = MetaDataCollector.Collect<TestModel>(new());

        Assert.Contains(metaData, x => x.Name == nameof(TestModel.Default1) && x.LayoutFlags == LayoutFlags.Default);
        Assert.Contains(metaData, x => x.Name == nameof(TestModel.Default2) && x.LayoutFlags == LayoutFlags.Default);

        Assert.Contains(metaData, x => x.Name == nameof(TestModel.Foldable) && x.LayoutFlags == LayoutFlags.Foldable);

        Assert.Contains(metaData, x => x.Name == nameof(TestModel.InitiallyExpanded1) && x.LayoutFlags == (LayoutFlags.Foldable | LayoutFlags.ExpandedInitially));
        Assert.Contains(metaData, x => x.Name == nameof(TestModel.InitiallyExpanded2) && x.LayoutFlags == (LayoutFlags.Foldable | LayoutFlags.ExpandedInitially));
        Assert.Contains(metaData, x => x.Name == nameof(TestModel.InitiallyExpanded3) && x.LayoutFlags == (LayoutFlags.Foldable | LayoutFlags.ExpandedInitially));

        Assert.Contains(metaData, x => x.Name == nameof(TestModel.NoFrameNoBackground1) && x.LayoutFlags == (LayoutFlags.NoFrame | LayoutFlags.NoBackground));
        Assert.Contains(metaData, x => x.Name == nameof(TestModel.NoFrameNoBackground2) && x.LayoutFlags == (LayoutFlags.NoFrame | LayoutFlags.NoBackground));
    }

    public sealed class TestModel
    {
        public int Default1 { get; set; }

        [LayoutFlags(LayoutFlags.Default)]
        public int Default2 { get; set; }

        [LayoutFlags(LayoutFlags.Foldable)]
        public int Foldable { get; set; }

        [LayoutFlags(LayoutFlags.ExpandedInitially)]
        public int InitiallyExpanded1 { get; set; }

        [LayoutFlags(LayoutFlags.Foldable | LayoutFlags.ExpandedInitially)]
        public int InitiallyExpanded2 { get; set; }

        [LayoutFlags(LayoutFlags.Foldable)]
        [LayoutFlags(LayoutFlags.ExpandedInitially)]
        public int InitiallyExpanded3 { get; set; }

        [LayoutFlags(LayoutFlags.NoFrame | LayoutFlags.NoBackground)]
        public int NoFrameNoBackground1 { get; set; }

        [LayoutFlags(LayoutFlags.NoFrame)]
        [LayoutFlags(LayoutFlags.NoBackground)]
        public int NoFrameNoBackground2 { get; set; }
    }
}
