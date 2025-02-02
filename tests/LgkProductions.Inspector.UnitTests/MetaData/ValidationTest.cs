using LgkProductions.Inspector.MetaData;
using System.ComponentModel.DataAnnotations;

namespace LgkProductions.Inspector.UnitTests.MetaData;

public class ValidationTest
{
    [Fact]
    public void TryValidate_ShouldSucceed_WhenValid()
    {
        var metadata = MetaDataCollector.Collect<EmailModel>(new());
        Assert.Single(metadata);
        var entry1 = metadata.First();

        Assert.True(entry1.TryValidate("valid@mail.net", out var errors));
        Assert.Empty(errors);

        List<ValidationResult> errors2 = [];
        Assert.True(entry1.TryValidate("valid@mail.net", errors2));
        Assert.Empty(errors2);
    }

    [Fact]
    public void TryValidate_ShouldFail_WhenNotValid()
    {
        var metadata = MetaDataCollector.Collect<EmailModel>(new());
        Assert.Single(metadata);
        var entry1 = metadata.First();

        Assert.False(entry1.TryValidate("not-valid$mail.net", out var errors));
        Assert.Single(errors);

        List<ValidationResult> errors2 = [];
        Assert.False(entry1.TryValidate("not-valid$mail.net", errors2));
        Assert.Single(errors2);
    }

    sealed class EmailModel
    {
        [EmailAddress]
        public string? Email { get; set; }
    }

    [Fact]
    public void TryValidate_ShouldNotFail_WhenNoValidationAttribute()
    {
        var metadata = MetaDataCollector.Collect<SimpleModel>(new());
        Assert.Single(metadata);
        var entry1 = metadata.First();

        Assert.True(entry1.TryValidate("abc", out var errors));
        Assert.Empty(errors);

        List<ValidationResult> errors2 = [];
        Assert.True(entry1.TryValidate("abc", errors2));
        Assert.Empty(errors2);
    }

    [Fact]
    public void TryValidate_ShouldFail_WhenWrongType()
    {
        var metadata = MetaDataCollector.Collect<SimpleModel>(new());
        Assert.Single(metadata);
        var entry1 = metadata.First();

        Assert.False(entry1.TryValidate(42, out var errors));
        Assert.Single(errors);

        List<ValidationResult> errors2 = [];
        Assert.False(entry1.TryValidate(42, errors2));
        Assert.Single(errors2);
    }

    sealed class SimpleModel
    {
        public string? Value { get; set; }
    }
}
