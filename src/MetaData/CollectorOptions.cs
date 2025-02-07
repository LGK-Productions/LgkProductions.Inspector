namespace LgkProductions.Inspector.MetaData;

/// <summary>
/// Options for the <see cref="MetaDataCollector"/>.
/// </summary>
/// <seealso cref="MetaDataCollector.Collect{T}(CollectorOptions)"/>
/// <seealso cref="MetaDataCollector.Collect(Type, CollectorOptions)"/>
public sealed record CollectorOptions
{
    /// <summary>
    /// Whether to include fields by default.
    /// </summary>
    public bool IncludeFields { get; init; }
}
