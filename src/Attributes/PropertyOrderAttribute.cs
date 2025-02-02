using LgkProductions.Inspector.MetaData;
using System.Runtime.CompilerServices;

namespace LgkProductions.Inspector.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
public sealed class PropertyOrderAttribute([CallerLineNumber] float order = 0) : InspectorAttribute
{
    public override void Apply(MetaDataMember memberInfo, ref bool shouldInclude)
        => memberInfo.Order = order;
}
