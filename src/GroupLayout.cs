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
    /// Whether the group should be foldable.
    /// </summary>
    public bool IsFoldable { get; set; }

    /// <summary>
    /// Whether the group should have a frame (including title etc).
    /// </summary>
    public bool HasFrame { get; set; } = true;
}
