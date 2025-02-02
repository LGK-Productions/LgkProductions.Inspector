using LgkProductions.Inspector.MetaData;

namespace LgkProductions.Inspector.Attributes;

public abstract class InspectorAttribute : Attribute
{
    public abstract void Apply(MetaDataMember memberInfo, ref bool shouldInclude);
}
