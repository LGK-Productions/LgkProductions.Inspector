using LgkProductions.Inspector.MetaData;

namespace LgkProductions.Inspector.Attributes;

/// <summary>
/// Groups fields or properties in the inspector.
/// </summary>
/// <param name="groupName"></param>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
public sealed class BoxGroupAttribute(string groupName) : InspectorAttribute
{
    /// <summary>
    /// The name of the group.
    /// </summary>
    public string GroupName { get; } = groupName;

    /// <summary>
    /// The orientation of the group.
    /// </summary>
    public Orientation Orientation { get; set; } = Orientation.Vertical;

    public override void Apply(MetaDataMember memberInfo, ref bool shouldInclude)
        => memberInfo.Group = new()
        {
            Title = GroupName,
            Orientation = Orientation
        };
}
