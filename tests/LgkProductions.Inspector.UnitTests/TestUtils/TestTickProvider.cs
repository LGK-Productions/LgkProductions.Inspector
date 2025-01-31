namespace LgkProductions.Inspector.UnitTests.TestUtils;

internal sealed class TestTickProvider : ITickProvider
{
    public event Action? Tick;

    public void TriggerTick()
        => Tick?.Invoke();
}
