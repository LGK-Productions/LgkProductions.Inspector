using System.Collections;

namespace LgkProductions.Inspector.MetaData;

public sealed partial class MetaDataCollection : IEnumerable<MetaDataMember>
{
    readonly IEnumerable<MetaDataMember> _members;
    internal MetaDataCollection(IEnumerable<MetaDataMember> members)
        => _members = members;

    public IEnumerator<MetaDataMember> GetEnumerator()
        => _members.OrderBy(x => x.Order ?? 0).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}
