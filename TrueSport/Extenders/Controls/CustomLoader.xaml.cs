using CommunityToolkit.Mvvm.ComponentModel;

namespace TrueSport;

public partial class CustomLoader : ContentView
{
    public static BindableProperty IsActiveProperty =
           BindableProperty.Create(
               nameof(IsActive),
               typeof(bool),
               typeof(CustomLoader),
               defaultValue: false,
               defaultBindingMode: BindingMode.OneWay,
               propertyChanged: IsActiveChanged);

    public bool IsActive
    {
        get => (bool)GetValue(IsActiveProperty);
        set => SetValue(IsActiveProperty, value);
    }

    public CustomLoader()
    {
        InitializeComponent();
        IsVisible = false;
    }

    private static void IsActiveChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var me = (CustomLoader)bindable;
        me.SetLoad((bool)newValue);
    }

    void SetLoad(bool value)
    {
        if (IsVisible == value)
            return;

        IsVisible = Wrapper.IsRunning = value;
    }
}