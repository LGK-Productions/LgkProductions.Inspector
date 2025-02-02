using LgkProductions.Inspector.MetaData;
using System.ComponentModel;

namespace LgkProductions.Inspector;

/// <summary>
/// Represents a member of an instance 
/// </summary>
public sealed class InspectorElement : IDisposable
{
    /// <summary>
    /// The instance that contains this inspected member.
    /// </summary>
    public object Instance { get; }

    /// <summary>
    /// Static metadata of the inspected member.
    /// </summary>
    public InspectorMember MemberInfo { get; }

    readonly ITickProvider? _tickProvider;
    private InspectorElement(object instance, InspectorMember member, ITickProvider? tickProvider)
    {
        Instance = instance;
        MemberInfo = member;

        _tickProvider = tickProvider;

        if (instance is INotifyPropertyChanged notify)
            notify.PropertyChanged += OnInstancePropertyChanged;

        if (tickProvider is not null)
            tickProvider.Tick += OnTick;
    }

    /// <summary>
    /// Gets or sets the valud of the inspected <see cref="MemberInfo"/> in <see cref="Instance"/>.
    /// </summary>
    public object? Value
    {
        get => MemberInfo.GetValue(Instance);
        set => MemberInfo.SetValue(Instance, value);
    }

    /// <summary>
    /// Occurs when the value of the inspected <see cref="MemberInfo"/> in <see cref="Instance"/> changes.
    /// </summary>
    public event ValueChangedEventHandler? ValueChanged;

    #region Notify
    /// <summary>
    /// Attaches a listener to the <see cref="INotifyPropertyChanged.PropertyChanged"/> event of <paramref name="instance"/> to observe the specified <paramref name="member"/>.
    /// </summary>
    /// <typeparam name="TInstance">Type of instance</typeparam>
    /// <param name="instance">The instance containing <paramref name="member"/></param>
    /// <param name="member">Static metadata of the meber to inspect</param>
    /// <returns>The attached inspector element</returns>
    /// <exception cref="ArgumentException"><paramref name="member"/> is not declared in <paramref name="instance"/></exception>
    public static InspectorElement AttachNotify<TInstance>(TInstance instance, InspectorMember member) where TInstance : INotifyPropertyChanged
    {
        if (!member.IsDeclaredIn(instance))
            throw new ArgumentException("The instance does not declare the member.", nameof(instance));

        return new(instance, member, tickProvider: null);
    }

    private void OnInstancePropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != MemberInfo.Name)
            return;

        ValueChanged?.Invoke(Instance, MemberInfo, MemberInfo.GetValue(Instance));
    }
    #endregion Notify

    #region Polling
    /// <summary>
    /// Attaches a polling system to specified <paramref name="member"/> of <paramref name="instance"/>.
    /// </summary>
    /// <param name="instance">The instance containing <paramref name="member"/></param>
    /// <param name="member">Static metadata of the meber to inspect</param>
    /// <param name="tickProvider">Tick provider used for polling</param>
    /// <returns>The attached inspector element</returns>
    /// <exception cref="ArgumentException"><paramref name="member"/> is not declared in <paramref name="instance"/></exception>
    public static InspectorElement AttachPoll(object instance, InspectorMember member, ITickProvider tickProvider)
    {
        if (!member.IsDeclaredIn(instance))
            throw new ArgumentException("The instance does not declare the member.", nameof(instance));

        return new(instance, member, tickProvider);
    }

    object? _cachedValue;
    void OnTick()
    {
        var newValue = MemberInfo.GetValue(Instance);
        if (Equals(newValue, _cachedValue))
            return;

        _cachedValue = newValue;
        ValueChanged?.Invoke(Instance, MemberInfo, newValue);
    }
    #endregion Polling

    /// <summary>
    /// Detaches this element from the instance.
    /// </summary>
    public void Dispose()
    {
        if (Instance is INotifyPropertyChanged notify)
            notify.PropertyChanged -= OnInstancePropertyChanged;

        if (_tickProvider is not null)
            _tickProvider.Tick -= OnTick;
    }
}

/// <summary>
/// Handles changes of a single member.
/// </summary>
/// <param name="instance">The instance that contains <paramref name="member"/></param>
/// <param name="member">Static metadata of the changed member</param>
/// <param name="newValue">The new valud of the member</param>
public delegate void ValueChangedEventHandler(object instance, InspectorMember member, object? newValue);
