using LgkProductions.Inspector.UnitTests.TestUtils;
using System.ComponentModel;

namespace LgkProductions.Inspector.UnitTests;

public class InspectorTest
{
    [Fact]
    public void AttachPoll()
    {
        TestTickProvider testTickProvider = new();
        PollRequiredModel model = new();

        Inspector inspector = Inspector.Attach(model, testTickProvider);
        Assert.Single(inspector.Elements);

        using ElementSpy spy = new(inspector.Elements[0]);

        testTickProvider.TriggerTick();
        testTickProvider.TriggerTick(); // No change => no event
        model.Value = 1;
        model.Value = 2;
        testTickProvider.TriggerTick();
        model.Value = 3;
        model.Value = 4;
        testTickProvider.TriggerTick();
        testTickProvider.TriggerTick();

        Assert.Equal(3, spy.Values.Count);
        Assert.Equal([0, 2, 4], spy.Values);
    }

    [Fact]
    public void AttachNotify()
    {
        TestTickProvider testTickProvider = new();
        NotifyModel model = new();

        Inspector inspector = Inspector.Attach(model, testTickProvider);
        Assert.Single(inspector.Elements);

        using ElementSpy spy = new(inspector.Elements[0]);

        model.Value = 1;
        model.Value = 2;
        model.Value = 3;
        model.Value = 4;

        Assert.Equal(4, spy.Values.Count);
        Assert.Equal([1, 2, 3, 4], spy.Values);
    }

    [Fact]
    public void ValueProperty()
    {
        TestTickProvider testTickProvider = new();
        NotifyModel model = new();

        Inspector inspector = Inspector.Attach(model, testTickProvider);
        Assert.Single(inspector.Elements);

        Assert.Equal(0, model.Value);
        inspector.Elements[0].Value = 1;
        Assert.Equal(1, model.Value);
        inspector.Elements[0].Value = 42;
        Assert.Equal(42, model.Value);
    }
}

sealed class PollRequiredModel
{
    public int Value { get; set; }
}

sealed class NotifyModel : INotifyPropertyChanged
{
    public int Value
    {
        get => field;
        set
        {
            field = value;
            PropertyChanged?.Invoke(this, new(nameof(Value)));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}