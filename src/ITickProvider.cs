namespace LgkProductions.Inspector;

/// <summary>
/// Provides ticks for the polling system.
/// </summary>
public interface ITickProvider
{
    /// <summary>
    /// Triggers polling.
    /// </summary>
    public event Action? Tick;
}
