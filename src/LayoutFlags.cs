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
    /// Disable fold button in the layout
    /// </summary>
    NotFoldable = 0b10 | ExpandedInitially,
    /// <summary>
    /// Expand the group  initially.
    /// </summary>
    /// <seealso cref="Foldable"/>
    ExpandedInitially = 0b1,
    /// <summary>
    /// Hide the label of the layout.
    /// </summary>
    NoLabel = 0b100,
    /// <summary>
    /// Disable the layout frame (e.g. buttons).
    /// </summary>
    NoElements = 0b1000,
    /// <summary>
    /// Disable the layout background.
    /// </summary>
    NoBackground = 0b10000,
}
