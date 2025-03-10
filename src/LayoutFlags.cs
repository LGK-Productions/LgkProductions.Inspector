namespace LgkProductions.Inspector;

/// <summary>
/// Define how a layout behaves.
/// </summary>
[Flags]
public enum LayoutFlags
{
    /// <summary>
    /// Default layout flags.
    /// </summary>
    Default = 0,
    /// <summary>
    /// Make the layout foldable.
    /// </summary>
    Foldable = 0b1,
    /// <summary>
    /// Expand the group  initially.
    /// </summary>
    /// <seealso cref="Foldable"/>
    ExpandedInitially = 0b10 | Foldable,
    /// <summary>
    /// Hide the label of the layout.
    /// </summary>
    NoLabel = 0b100,
    /// <summary>
    /// Disable the layout frame (e.g. buttons).
    /// </summary>
    NoFrame = 0b1000,
    /// <summary>
    /// Disable the layout background.
    /// </summary>
    NoBackground = 0b10000,
}
