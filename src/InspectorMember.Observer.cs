using System.ComponentModel;

namespace LgkProductions.Inspector;

partial class InspectorMember
{
    public event ValueChangedEventHandler? ValueChanged;

    public void Watch<TParent>(TParent parent) where TParent : INotifyPropertyChanged
        => parent.PropertyChanged += OnParentPropertyChanged;

    public void UnWatch<TParent>(TParent parent) where TParent : INotifyPropertyChanged
        => parent.PropertyChanged -= OnParentPropertyChanged;

    private void OnParentPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != Name)
            return;

        ValueChanged?.Invoke(this, GetValue(sender));
    }

    object? _oldValue;
    public void Tick(object parent)
    {
        var newValue = GetValue(parent);
        if (Equals(newValue, _oldValue))
            return;

        _oldValue = newValue;
        ValueChanged?.Invoke(this, newValue);
    }
}

public delegate void ValueChangedEventHandler(InspectorMember sender, object? value);
