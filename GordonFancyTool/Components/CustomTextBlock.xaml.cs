namespace GordonFancyTool.Components;

public partial class CustomTextBlock : UserControl
{
    public enum ValueType
    {
        Default,
        None,
        Currency,
        Percentage,
    }

    public static readonly DependencyProperty ValueProperty =
       DependencyProperty.Register(
           nameof(Value),
           typeof(double),
           typeof(CustomTextBlock));

    public static readonly DependencyProperty ValueAlignmentProperty =
        DependencyProperty.Register(
            nameof(ValueAlignment),
            typeof(HorizontalAlignment),
            typeof(CustomTextBlock),
            new PropertyMetadata(HorizontalAlignment.Right));

    public static readonly DependencyProperty TypeProperty =
        DependencyProperty.Register(
            nameof(Type),
            typeof(ValueType),
            typeof(CustomTextBlock),
            new PropertyMetadata(
            ValueType.Default,
            OnValueTypeChanged));

    public double Value
    {
        set => SetValue(ValueProperty, value);
        get => (double)GetValue(ValueProperty);
    }

    public HorizontalAlignment ValueAlignment
    {
        set => SetValue(ValueAlignmentProperty, value);
        get => (HorizontalAlignment)GetValue(ValueAlignmentProperty);
    }

    public ValueType Type
    {
        set => SetValue(TypeProperty, value);
        get => (ValueType)GetValue(TypeProperty);
    }

    public CustomTextBlock()
    {
        InitializeComponent();
    }

    private static void OnValueTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (CustomTextBlock)d;
        ValueType type = (ValueType)e.NewValue;

        switch (type)
        {
            case ValueType.None:
                Grid.SetColumnSpan(control.ValueBlock, 2);
                control.TypeBlock.Text = "";
                break;
            case ValueType.Currency:
                control.TypeBlock.Text = "kr.";
                break;
            case ValueType.Percentage:
                control.TypeBlock.Text = "%";
                break;
        }
    }
}
