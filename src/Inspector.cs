using LgkProductions.Inspector.MetaData;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LgkProductions.Inspector;

public sealed class Inspector : IDisposable
{
    /// <summary>
    /// The instance being inspected.
    /// <seealso cref="Attach"/>
    /// </summary>
    public object Instance { get; }

    /// <summary>
    /// The elements that make up the inspector.
    /// </summary>
    public IReadOnlyList<InspectorElement> Elements { get; }

    private Inspector(object instance, IReadOnlyList<InspectorElement> elements)
    {
        Instance = instance;
        Elements = elements;
    }

    /// <inheritdoc cref="Validator.TryValidateObject(object, ValidationContext, ICollection{ValidationResult}?, bool)"/>
    public bool TryValidate(ICollection<ValidationResult>? validationResults = null, bool validateAllProperties = true)
        => Validator.TryValidateObject(Instance, new ValidationContext(Instance), validationResults, validateAllProperties);

    /// <inheritdoc cref="Validator.TryValidateObject(object, ValidationContext, ICollection{ValidationResult}?, bool)"/>
    public bool TryValidate(out ICollection<ValidationResult> validationResults, bool validateAllProperties = true)
    {
        validationResults = [];
        return Validator.TryValidateObject(Instance, new ValidationContext(Instance), validationResults, validateAllProperties);
    }

    /// <summary>
    /// Detaches the inspector.
    /// <seealso cref="InspectorElement.Dispose"/>
    /// </summary>
    /// <exception cref="AggregateException">Aggregated exceptions of inspector elements</exception>
    public void Dispose()
    {
        List<Exception>? exceptions = null;
        foreach (var element in Elements)
        {
            try
            {
                element.Dispose();
            }
            catch (Exception ex)
            {
                exceptions ??= [];
                exceptions.Add(ex);
            }
        }

        if (exceptions is not null)
            throw new AggregateException(exceptions);
    }

    /// <summary>
    /// Attaches an <see cref="Inspector"/> to an <see langword="object"/> instance.
    /// </summary>
    /// <param name="instance">The instance to be expected</param>
    /// <param name="tickProvider"></param>
    /// <returns>Inspector attached to the provided instance</returns>
    public static Inspector Attach(object instance, ITickProvider tickProvider)
    {
        var type = instance.GetType();

        var metaData = MetaDataCollector.Collect(type, new()
        {
            IncludeFields = true
        });

        INotifyPropertyChanged? notify = instance as INotifyPropertyChanged;

        List<InspectorElement> elements = [];
        foreach (var member in metaData)
        {
            if (notify is not null)
            {
                elements.Add(InspectorElement.AttachNotify(notify, member));
            }
            else
            {
                elements.Add(InspectorElement.AttachPoll(instance, member, tickProvider));
            }
        }
        return new(instance, elements);
    }
}
