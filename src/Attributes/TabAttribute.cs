using LgkProductions.Inspector.MetaData;

namespace LgkProductions.Inspector.Attributes;

/// <summary>
/// Separates elements into tabs in the inspector.
/// </summary>
/// <param name="tabName"></param>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
public sealed class TabAttribute(string tabName) : InspectorAttribute
{
    public override void Apply(MetaDataMember memberInfo, ref bool shouldInclude)
        => memberInfo.TabName = tabName;
}
