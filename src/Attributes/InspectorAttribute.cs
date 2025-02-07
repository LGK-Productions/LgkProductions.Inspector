using LgkProductions.Inspector.MetaData;

namespace LgkProductions.Inspector.Attributes;

/// <summary>
/// Base attribute for custom inspector attributes.
/// </summary>
public abstract class InspectorAttribute : Attribute
{
    /// <summary>
    /// Apply the inspector attribute to metadata.
    /// </summary>
    /// <param name="memberInfo">The metadata this data should be applied to</param>
    /// <param name="shouldInclude">Whether this meta should be included in metadata collection</param>
    public abstract void Apply(MetaDataMember memberInfo, ref bool shouldInclude);
}
