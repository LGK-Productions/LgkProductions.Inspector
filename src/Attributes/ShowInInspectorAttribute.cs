using LgkProductions.Inspector.MetaData;

namespace LgkProductions.Inspector.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
public sealed class ShowInInspectorAttribute : InspectorAttribute
{
    public override void Apply(MetaDataMember memberInfo, ref bool shouldInclude)
        => shouldInclude = true;
}
