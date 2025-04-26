namespace LgkProductions.Inspector.UnitTests;

public class NameProcessingTest
{
    [Fact]
    public void BasicNameProcessingTest()
    {
        Assert.Equal("Pascal Case", MemberNameProcessor.ProcessMemberName("PascalCase"));
        Assert.Equal("Camel Case", MemberNameProcessor.ProcessMemberName("camelCase"));
        Assert.Equal("Snake Case", MemberNameProcessor.ProcessMemberName("SNAKE_CASE"));
        Assert.Equal("Symbols1#+ Test", MemberNameProcessor.ProcessMemberName("Symbols1#+Test"));
    }
}