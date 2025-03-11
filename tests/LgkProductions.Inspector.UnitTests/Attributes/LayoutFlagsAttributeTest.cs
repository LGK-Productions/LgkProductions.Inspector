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
        Assert.Equal(LayoutFlags.NotFoldable | LayoutFlags.ExpandedInitially, LayoutFlags.NotFoldable);
        Assert.True(LayoutFlags.NotFoldable.HasFlag(LayoutFlags.ExpandedInitially));
    }

    [Fact]
    public void TestLayoutFlags()
    {
        var metaData = MetaDataCollector.Collect<TestModel>(new());

        Assert.Contains(metaData, x => x.Name == nameof(TestModel.Default1) && x.LayoutFlags == LayoutFlags.Default);
        Assert.Contains(metaData, x => x.Name == nameof(TestModel.Default2) && x.LayoutFlags == LayoutFlags.Default);

        Assert.Contains(metaData, x => x.Name == nameof(TestModel.NotFoldable) && x.LayoutFlags == (LayoutFlags.NotFoldable | LayoutFlags.ExpandedInitially));

        Assert.Contains(metaData, x => x.Name == nameof(TestModel.InitiallyExpanded1) && x.LayoutFlags == (LayoutFlags.NotFoldable | LayoutFlags.ExpandedInitially));
        Assert.Contains(metaData, x => x.Name == nameof(TestModel.InitiallyExpanded2) && x.LayoutFlags == (LayoutFlags.NotFoldable | LayoutFlags.ExpandedInitially));
        Assert.Contains(metaData, x => x.Name == nameof(TestModel.InitiallyExpanded3) && x.LayoutFlags == (LayoutFlags.NotFoldable | LayoutFlags.ExpandedInitially));

        Assert.Contains(metaData, x => x.Name == nameof(TestModel.NoFrameNoBackground1) && x.LayoutFlags == (LayoutFlags.NoElements | LayoutFlags.NoBackground));
        Assert.Contains(metaData, x => x.Name == nameof(TestModel.NoFrameNoBackground2) && x.LayoutFlags == (LayoutFlags.NoElements | LayoutFlags.NoBackground));
    }

    public sealed class TestModel
    {
        public int Default1 { get; set; }

        [LayoutFlags(LayoutFlags.Default)]
        public int Default2 { get; set; }

        [LayoutFlags(LayoutFlags.NotFoldable)]
        public int NotFoldable { get; set; }

        [LayoutFlags(LayoutFlags.NotFoldable)]
        public int InitiallyExpanded1 { get; set; }

        [LayoutFlags(LayoutFlags.NotFoldable | LayoutFlags.ExpandedInitially)]
        public int InitiallyExpanded2 { get; set; }

        [LayoutFlags(LayoutFlags.NotFoldable)]
        [LayoutFlags(LayoutFlags.ExpandedInitially)]
        public int InitiallyExpanded3 { get; set; }

        [LayoutFlags(LayoutFlags.NoElements | LayoutFlags.NoBackground)]
        public int NoFrameNoBackground1 { get; set; }

        [LayoutFlags(LayoutFlags.NoElements)]
        [LayoutFlags(LayoutFlags.NoBackground)]
        public int NoFrameNoBackground2 { get; set; }
    }
}
