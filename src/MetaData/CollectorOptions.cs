namespace LgkProductions.Inspector.MetaData;

/// <summary>
/// Options for the <see cref="MetaDataCollector"/>.
/// </summary>
/// <seealso cref="MetaDataCollector.Collect{T}(LgkProductions.Inspector.MetaData.CollectorOptions)"/>
/// <seealso cref="MetaDataCollector.Collect(Type, LgkProductions.Inspector.MetaData.CollectorOptions)"/>
public sealed record CollectorOptions
{
    /// <summary>
    /// Whether to include fields by default.
    /// </summary>
    public bool IncludeFields { get; init; }
}
