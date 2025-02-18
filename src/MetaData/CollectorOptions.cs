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

    /// <summary>
    /// Custom post processor to modify the metadata collection per member.
    /// </summary>
    public MemberPostProcessor? MemberPostProcessor { get; init; }
}

/// <summary>
/// Custom post processor to modify the metadata collection per member.
/// </summary>
/// <param name="meber">Member that should be processed</param>
/// <param name="shouldInclude">Wether this member should be included</param>
public delegate void MemberPostProcessor(MetaDataMember meber, ref bool shouldInclude);
