﻿using LgkProductions.Inspector.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace LgkProductions.Inspector.MetaData;

/// <summary>
/// Static metadata of a member in a type.
/// </summary>
/// <param name="memberInfo">Reflection metadata</param>
/// <param name="typeOfValue">Type of the value of this member</param>
[DebuggerDisplay("{DisplayName}")]
public sealed partial class MetaDataMember(MemberInfo memberInfo, Type typeOfValue)
{
    internal bool Equals(MemberInfo other)
        => memberInfo == other;

    /// <summary>
    /// Type of the value of this member.
    /// </summary>
    /// <seealso cref="PropertyInfo.PropertyType"/>
    /// <seealso cref="PropertyInfo.PropertyType"/>
    public Type Type { get; } = typeOfValue;

    /// <inheritdoc cref="MemberInfo.DeclaringType"/>
    public Type DeclaringType { get; } = memberInfo.DeclaringType;

    /// <summary>
    /// See <see cref="MemberInfo.Name"/>
    /// </summary>
    public string Name { get; } = memberInfo.Name;

    /// <summary>
    /// See <see cref="DisplayNameAttribute"/> and <see cref="DisplayAttribute.Name"/>
    /// </summary>
    public string DisplayName { get; set; } = memberInfo.Name;

    /// <summary>
    /// See <see cref="DescriptionAttribute"/> and <see cref="DisplayAttribute.Description"/>
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// See <see cref="CategoryAttribute"/>, <see cref="DisplayAttribute.GroupName"/> and <see cref="BoxGroupAttribute"/>
    /// </summary>
    public GroupLayout? Group { get; set; }

    internal void SetGroupName(string? groupName)
    {
        if (groupName == null)
            return;

        if (Group is null)
            Group = new() { Title = groupName };
        else
            Group.Title = groupName;
    }

    /// <summary>
    /// Define how the layout behaves.
    /// </summary>
    public LayoutFlags LayoutFlags { get; set; } = LayoutFlags.Default;

    /// <summary>
    /// See <see cref="InlineInspectorAttribute"/>
    /// </summary>
    public bool UseInlineInspector { get; set; }

    /// <summary>
    /// See <see cref="TabAttribute"/>
    /// </summary>
    public string? TabName { get; set; }

    /// <summary>
    /// See <see cref="DefaultValueAttribute"/>
    /// </summary>
    public object? DefaultValue { get; set; }

    /// <summary>
    /// See <see cref="ReadOnlyAttribute"/>
    /// </summary>
    public bool IsReadOnly { get; set; }

    /// <inheritdoc cref="PropertyInfo.GetValue(object)"/>
    public GetValueDelegate GetValue { get; set; } = static (obj) => throw new NotImplementedException("This member is write-only");

    /// <inheritdoc cref="PropertyInfo.SetValue(object, object)"/>
    public SetValueDelegate SetValue { get; set; } = static (obj, value) => throw new NotImplementedException("This member is read-only");

    /// <summary>
    /// See <see cref="PropertyOrderAttribute"/> and <see cref="DisplayAttribute.Order"/>
    /// </summary>
    public float? Order { get; set; }

    /// <summary>
    /// See <see cref="StringLengthAttribute.MinimumLength"/> and <see cref="MinLengthAttribute"/>
    /// </summary>
    public int? MinLength { get; set; }

    /// <summary>
    /// See <see cref="StringLengthAttribute.MaximumLength"/> and <see cref="MaxLengthAttribute"/>
    /// </summary>
    public int? MaxLength { get; set; }

    /// <summary>
    /// See <see cref="RangeAttribute.Minimum"/>
    /// </summary>
    public object? MinValue { get; set; }

    /// <summary>
    /// See <see cref="RangeAttribute.Maximum"/>
    /// </summary>
    public object? MaxValue { get; set; }

    /// <summary>
    /// See <see cref="RequiredAttribute"/>
    /// </summary>
    public bool IsRequired { get; set; }

    /// <summary>
    /// See <see cref="SpaceAttribute"/>
    /// </summary>
    public Thickness Spacing { get; set; }

    /// <summary>
    /// See <see cref="LineAttribute"/>
    /// </summary>
    public bool HasLineAbove { get; set; }

    readonly Dictionary<string, object> _customMetaData = [];

    /// <summary>
    /// May hold metadata not representable by other properties.
    /// </summary>
    [Obsolete("Use SetMetaData and TryGetMetaData instead")]
    public Dictionary<string, object> CustomMetaData => _customMetaData;

    /// <summary>
    /// Sets metadata of type <typeparamref name="T"/> with the given <paramref name="key"/>.
    /// </summary>
    /// <typeparam name="T">Type of the custom metadata</typeparam>
    /// <param name="key">Key of the metadata</param>
    /// <param name="value">Value of the metadata</param>
    public void SetMetaData<T>(MetaDataKey<T> key, T value) where T : notnull
        => _customMetaData[key.Name] = value;

    /// <summary>
    /// Gets metadata of type <typeparamref name="T"/> with the given <paramref name="key"/>.
    /// </summary>
    /// <typeparam name="T">Type of the custom metadata</typeparam>
    /// <param name="key">Key of the metadata</param>
    /// <param name="value">Value of the metadata</param>
    /// <returns>Wether metadata with the key was found</returns>
    public bool TryGetMetaData<T>(MetaDataKey<T> key, [MaybeNullWhen(false)] out T value)
    {
        value = default;

        if (!_customMetaData.TryGetValue(key.Name, out var rawValue))
            return false;

        if (rawValue is not T typedValue)
            return false;

        value = typedValue;
        return true;
    }

    /// <summary>
    /// Wether this member is declared in <paramref name="instance"/>.
    /// </summary>
    /// <param name="instance">The instance to check</param>
    /// <returns>Wether the type of instance declares this member</returns>
    public bool IsDeclaredIn(object instance)
        => DeclaringType.IsAssignableFrom(instance.GetType());

    /// <inheritdoc cref="Validator.TryValidateValue(object, ValidationContext, ICollection{ValidationResult}?, IEnumerable{ValidationAttribute})"/>
    public bool TryValidate(object value, ICollection<ValidationResult>? validationResults = null)
        => Validator.TryValidateValue(value, new ValidationContext(value), validationResults, memberInfo.GetCustomAttributes<ValidationAttribute>());

    /// <inheritdoc cref="Validator.TryValidateValue(object, ValidationContext, ICollection{ValidationResult}?, IEnumerable{ValidationAttribute})"/>
    public bool TryValidate(object value, out ICollection<ValidationResult> validationResults)
    {
        validationResults = [];
        return Validator.TryValidateValue(value, new ValidationContext(value), validationResults, memberInfo.GetCustomAttributes<ValidationAttribute>());
    }
}

public delegate object? GetValueDelegate(object obj);
public delegate void SetValueDelegate(object obj, object? value);
