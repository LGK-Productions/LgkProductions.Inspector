using LgkProductions.Inspector.MetaData;

namespace LgkProductions.Inspector.Attributes;

/// <summary>
/// Define how a layout behaves.
/// </summary>
/// <param name="flags"></param>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
public sealed class LayoutFlagsAttribute(LayoutFlags flags) : InspectorAttribute
{
    /// <summary>
    /// Define how the layout behaves.
    /// </summary>
    public LayoutFlags Flags { get; } = flags;

    public override void Apply(MetaDataMember memberInfo, ref bool shouldInclude)
        => memberInfo.LayoutFlags |= Flags;
}
