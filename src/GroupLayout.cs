namespace LgkProductions.Inspector;

/// <summary>
/// Defines group layouts.
/// </summary>
public sealed record GroupLayout
{
    /// <summary>
    /// The name of the group.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// The orientation of the group.
    /// </summary>
    public Orientation Orientation { get; set; } = Orientation.Vertical;

    /// <summary>
    /// Defines how the layout behaves.
    /// </summary>
    public LayoutFlags LayoutFlags { get; set; } = LayoutFlags.Default;
}
