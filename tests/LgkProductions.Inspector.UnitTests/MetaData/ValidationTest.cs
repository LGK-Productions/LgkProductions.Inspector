using LgkProductions.Inspector.MetaData;
using System.ComponentModel.DataAnnotations;

namespace LgkProductions.Inspector.UnitTests.MetaData;

public class ValidationTest
{
    [Fact]
    public void ValidateEmail_WhenValid()
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
    public void ValidateEmail_WhenNotValid()
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
}
