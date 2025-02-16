# PostProcessor

Custom post processors can be used to customize the metadata collection.
This is especially useful in case you cannot add attributes to a type or it's members.

You can pass your post processor to [](xref:LgkProductions.Inspector.MetaData.CollectorOptions.MemberPostProcessor):

```cs
MetaDataCollection collection = MetaDataCollector.Collect<TestModel>(new()
{
    MemberPostProcessor = myPostProcessor
});
```

```cs
using var inspector = Inspector.Attach(
    instance, tickProvider,
    new CollectorOptions() {
        MemberPostProcessor = myPostProcessor
    }
);
```

## Build MetaData for foreign type

Consider the following class:

```cs
public sealed class TestModel {
    public string SomeProperty { get; set; }
    private int InaccessibleProperty { get; set; }
}
```

You can create a [](xref:LgkProductions.Inspector.MetaData.MemberPostProcessor) in a type-safe manner by using the [](xref:LgkProductions.Inspector.MetaData.MetaDataCollector.PostProcessorBuilder`1) class. This is only possible for accessible (e.g. `public`) members.

```cs
var postProcessor = MetaDataCollector.PostProcessor<TestModel>()
    .Member(x => x.SomeProperty,
        new RequiredAttribute(),
        new BoxGroupAttribute("SomeGroup")
    )
    .Member(x => x.InaccessibleProperty, // Compiler error
        new BoxGroupAttribute("SomeGroup"),
        new RangeAttribute(1, 2)
    )
    .Build();
```

## Fully custom postprocessor

```cs
(MetaDataMember member, ref bool shouldInclude) =>
{
    if(member.DeclaringType != typeof(TestModel))
        return;

    if (member.Name != "InaccessibleProperty")
        return;

    member.Description = "Fancy description";
    shouldInclude = true;
}
```
