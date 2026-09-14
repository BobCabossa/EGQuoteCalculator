using System.Text.RegularExpressions;

namespace GordonFancyTool.Components;

public partial class DoubleTextBox : UserControl
{
    public double? Value { get
        {
            if (double.TryParse(textBox.Text, out var value)) {
                return value;
            }
            return null;
        }
    }

    public event EventHandler<string>? ValueChanged;

    public DoubleTextBox()
    {
        InitializeComponent();
    }

    private static readonly Regex DoubleRegex = new(@"^-?\d*(,\d*)?$");

    private void DoubleTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        var textBox = (TextBox)sender;

        string newText =
            textBox.Text.Remove(textBox.SelectionStart, textBox.SelectionLength)
                      .Insert(textBox.SelectionStart, e.Text);

        e.Handled = !DoubleRegex.IsMatch(newText);
    }

    private void DoubleTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
    {
        if (!e.DataObject.GetDataPresent(DataFormats.Text))
        {
            e.CancelCommand();
            return;
        }

        var pastedText = (string)e.DataObject.GetData(DataFormats.Text);

        if (!DoubleRegex.IsMatch(pastedText))
        {
            e.CancelCommand();
            return;
        }
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        ValueChanged?.Invoke(this, ((TextBox)sender).Text);
    }
}
