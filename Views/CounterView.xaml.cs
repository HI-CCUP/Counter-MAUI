using TestMauiApp.Controllers;
using TestMauiApp.Models;

namespace TestMauiApp.Views;

public partial class CounterView : ContentView
{
    private Counter _counter;
    private CountersController _controller;
    private bool _isInitialized = false;

    public event EventHandler<CounterView>? OnDeleteRequested;

    public CounterView(Counter counter, CountersController controller)
    {
        InitializeComponent();
        BindingContext = new Models.Counter();
        _counter = counter;
        _controller = controller;

        LoadDataToView();
        _isInitialized = true;
    }

    private void LoadDataToView()
    {
        NameEntry.Text = _counter.Name;
        CounterLabel.Text = _counter.Count.ToString();
        RedSlider.Value = _counter.Red;
        GreenSlider.Value = _counter.Green;
        BlueSlider.Value = _counter.Blue;
        CounterFrame.BorderColor = Color.FromRgb(_counter.Red, _counter.Green, _counter.Blue);
    }

    private void Increment_Clicked(object sender, EventArgs e)
    {
        _counter.Count++;
        CounterLabel.Text = _counter.Count.ToString();
        _controller.SaveCounter(_counter);
    }

    private void Decrement_Clicked(object sender, EventArgs e)
    {
        _counter.Count--;
        CounterLabel.Text = _counter.Count.ToString();
        _controller.SaveCounter(_counter);
    }

    private void Reset_Clicked(object sender, EventArgs e)
    {
        _counter.Count = _counter.DefaultValue;
        CounterLabel.Text = _counter.Count.ToString();
        _controller.SaveCounter(_counter);
    }

    private void Delete_Clicked(object sender, EventArgs e)
    {
        _controller.DeleteCounterFile(_counter.Id);
        OnDeleteRequested?.Invoke(this, this);
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!_isInitialized) return;
        _counter.Name = NameEntry.Text;
        _controller.SaveCounter(_counter);
    }

    private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (!_isInitialized) return;

        _counter.Red = (int)RedSlider.Value;
        _counter.Green = (int)GreenSlider.Value;
        _counter.Blue = (int)BlueSlider.Value;

        CounterFrame.BorderColor = Color.FromRgb(_counter.Red, _counter.Green, _counter.Blue);
        _controller.SaveCounter(_counter);
    }
}