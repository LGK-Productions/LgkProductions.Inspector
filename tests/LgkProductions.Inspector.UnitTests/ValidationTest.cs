using LgkProductions.Inspector.UnitTests.TestUtils;
using System.ComponentModel.DataAnnotations;

namespace LgkProductions.Inspector.UnitTests;

public class ValidationTest
{
    [Fact]
    public void ValidateEmail_WhenValid()
    {
        EmailModel model = new() { Email = "valid@mail.net" };

        var inspector = Inspector.Attach(model, new TestTickProvider());

        Assert.True(inspector.TryValidate(out var errors));
        Assert.Empty(errors);

        List<ValidationResult> errors2 = [];
        Assert.True(inspector.TryValidate(errors2));
        Assert.Empty(errors2);
    }

    [Fact]
    public void ValidateEmail_WhenNotValid()
    {
        EmailModel model = new() { Email = "not-valid$mail.net" };

        var inspector = Inspector.Attach(model, new TestTickProvider());

        Assert.False(inspector.TryValidate(out var errors));
        Assert.Single(errors);

        List<ValidationResult> errors2 = [];
        Assert.False(inspector.TryValidate(errors2));
        Assert.Single(errors2);
    }

    sealed class EmailModel
    {
        [EmailAddress]
        public required string Email { get; set; }
    }
}
