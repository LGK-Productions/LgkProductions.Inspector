# Custom Attributes

In case you want to have custom attributes not supported by the backend but still want to use the [](xref:LgkProductions.Inspector.MetaData.MetaDataCollector), you can implement a custom attribute using this advanced api:

## Create a custom attribute

First create a custom attribute and inherit from [](xref:LgkProductions.Inspector.Attributes.InspectorAttribute).
The [](xref:LgkProductions.Inspector.Attributes.InspectorAttribute.Apply(LgkProductions.Inspector.MetaData.MetaDataMember,System.Boolean@)) method is used to write your custom metadata to the [](xref:LgkProductions.Inspector.MetaData.MetaDataMember.CustomMetaData) dictionary.

```cs
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public sealed class MyCustomAttribute(string someValue) : InspectorAttribute
{
    public const string MetadataKey = "MyCustomAttribute";

    public string SomeValue { get; } = someValue;

    public override void Apply(MetaDataMember memberInfo, ref bool shouldInclude)
    {
        memberInfo.CustomMetaData.Add(MetadataKey, SomeValue);
    }
}
```

## Access custom metadata

You can access your custom metadata via the [](xref:LgkProductions.Inspector.MetaData.MetaDataMember.CustomMetaData) dictionary.

```cs
InspectorElement element = ...;

MetaDataMember member = element.MemberInfo;
if(member.CustomMetaData.TryGetValue(MetadataKey, out var someValue)){
    // Do stuff
}
```
