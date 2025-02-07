namespace LgkProductions.Inspector.MetaData;

/// <summary>
/// A key for metadata.
/// </summary>
/// <typeparam name="T">Value</typeparam>
/// <param name="Name">The name of the key</param>
public sealed record MetaDataKey<T>(string Name) : IMetaDataKey;

interface IMetaDataKey
{
    string Name { get; }
}
