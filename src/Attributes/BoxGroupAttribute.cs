using LgkProductions.Inspector.MetaData;

namespace LgkProductions.Inspector.Attributes;

public sealed class BoxGroupAttribute(string groupName) : InspectorAttribute
{
    public override void Apply(MetaDataMember memberInfo, ref bool shouldInclude)
        => memberInfo.GroupName = groupName;
}
