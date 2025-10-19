using DBracket.Cli.WPF.Controls;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace DBracket.Cli.WPF.Sample;

public partial class MainWindow : CliWindow
{
    public MainWindow()
    {
        InitializeComponent();

        var autoCompletePopup = Application.Current.FindResource("AutoCompletePopup") as Popup;
        if (autoCompletePopup is null)
            return;

        Initialize(CommandInput, autoCompletePopup);
    }
}