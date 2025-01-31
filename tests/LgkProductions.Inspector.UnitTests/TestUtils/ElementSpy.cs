using LgkProductions.Inspector.MetaData;

namespace LgkProductions.Inspector.UnitTests.TestUtils;

internal sealed class ElementSpy : IDisposable
{
    readonly InspectorElement _element;
    public ElementSpy(InspectorElement element)
    {
        _element = element;

        element.ValueChanged += OnValueChanged;
    }

    public List<object?> Values { get; } = [];
    private void OnValueChanged(object instance, InspectorMember member, object? newValue)
    {
        Assert.Same(_element.Instance, instance);
        Assert.Same(_element.MemberInfo, member);

        Values.Add(newValue);
    }

    public void Dispose()
    {
        _element.ValueChanged -= OnValueChanged;
    }
}
