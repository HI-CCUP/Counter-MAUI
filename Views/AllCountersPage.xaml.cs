using TestMauiApp.Controllers;
using TestMauiApp.Models;

namespace TestMauiApp.Views;

public partial class AllCountersPage : ContentPage
{
    private CountersController _controller;

    public AllCountersPage()
    {
        InitializeComponent();
        _controller = new CountersController();
        LoadCounters();
        BindingContext = new Models.AllCounters();
    }

    private void LoadCounters()
    {
        var savedCounters = _controller.LoadCounters();

        foreach (var counter in savedCounters)
        {
            AddCounterView(counter);
        }

        if (CountersStack.Children.Count == 0)
        {
            AddNewCounter("Counter 1", 0);
        }
    }

    private void AddCounter_Clicked(object sender, EventArgs e)
    {
        if (!int.TryParse(DefaultValueEntry.Text?.Trim(), out int defaultValue))
        {
            defaultValue = 0;
        }
        int nextNumber = CountersStack.Children.Count + 1;
        AddNewCounter($"Counter {nextNumber}", defaultValue);
    }

    private void AddNewCounter(string name, int defaultValue)
    {
        var counter = new Counter
        {
            Name = name,
            Count = defaultValue,
            DefaultValue = defaultValue
        };

        _controller.SaveCounter(counter);
        AddCounterView(counter);
    }

    private void AddCounterView(Counter counter)
    {
        var counterView = new CounterView(counter, _controller);
        counterView.OnDeleteRequested += CounterView_OnDeleteRequested;
        CountersStack.Children.Add(counterView);
    }

    private void CounterView_OnDeleteRequested(object? sender, CounterView e)
    {
        e.OnDeleteRequested -= CounterView_OnDeleteRequested;
        CountersStack.Children.Remove(e);
    }
}