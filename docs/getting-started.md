# Getting started

This library consists of two layers:

- Inspector
- MetaData

## Inspector

The inspector layers builds upon the metadata layer and is attached to an actual instance and therefore also has access to the actual values.  
The inspector provides a system to monitor changes in the inspected instance.

Most users likely want to use this high-level api.

### Attach an inspector

```cs
using var inspector = Inspector.Attach(instance, tickProvider);
```

> [!WARNING]
> It's important to call `Dispose` or use the `using` pattern to detach the inspector at some point in time.

### TickProvider

A [](xref:LgkProductions.Inspector.ITickProvider) provides a global tick event that is used for polling changes.

### INotifyPropertyChanged

Polling is quite ineffient. To fix this problem you can implement the [](xref:System.ComponentModel.INotifyPropertyChanged) interface to notify the inspector of changes instead.

Use source-generators like [CommunityToolkit.Mvvm](https://www.nuget.org/packages/CommunityToolkit.Mvvm) to simplify the implementation.

## MetaData

The MetaData layer only holds static metadata about a [](xref:System.Type) like

- Available properties / fields
- Details like [](xref:LgkProductions.Inspector.MetaData.MetaDataMember.DisplayName) or [](xref:LgkProductions.Inspector.MetaData.MetaDataMember.Description)
- Supported values like [](xref:LgkProductions.Inspector.MetaData.MetaDataMember.MinValue)
- Layout information like [](xref:LgkProductions.Inspector.MetaData.MetaDataMember.GroupName)

MetaData is stored in [](xref:LgkProductions.Inspector.MetaData.MetaDataMember)s and can be collected by using the [](xref:LgkProductions.Inspector.MetaData.MetaDataCollector).
