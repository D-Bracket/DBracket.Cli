using DBracket.Cli.Common;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace DBracket.Cli.WPF.Input;

public class CliInputHandler : PropertyChangedBase, IDisposable
{
    #region "----------------------------- Private Fields ------------------------------"
    internal CliInputContainer _inputContainer;
    #endregion



    #region "------------------------------ Constructor --------------------------------"
    public CliInputHandler(TextBox inputTextBox, Popup autoCompletePopup)
    {
        _inputContainer = new CliInputContainer(inputTextBox, autoCompletePopup);
        _inputContainer.InputTextBox.TextChanged += HandleInputChanged;

        AutoComplete = new AutoComplete(_inputContainer);

        _inputContainer.AutoCompletePopup = autoCompletePopup;
        _inputContainer.AutoCompletePopup.DataContext = _inputContainer.AutoCompleteViewModel;
        _inputContainer.AutoCompletePopup.PlacementTarget = _inputContainer.InputTextBox;
        _inputContainer.AutoCompletePopup.Placement = PlacementMode.Bottom;
    }
    #endregion



    #region "--------------------------------- Methods ---------------------------------"
    #region "----------------------------- Public Methods ------------------------------"
    public void Dispose()
    {
        _inputContainer.InputTextBox.TextChanged -= HandleInputChanged;
        _inputContainer.InputTextBox = null;
    }

    public void Focus()
    {
        _inputContainer.InputTextBox.Focus();
    }

    public void CloseAutoCompletePopup()
    {
        _inputContainer.AutoCompletePopup.IsOpen = false;
    }


    #endregion

    #region "----------------------------- Private Methods -----------------------------"

    #endregion

    #region "------------------------------ Event Handling -----------------------------"
    private void HandleInputChanged(object sender, TextChangedEventArgs e)
    {

    }
    #endregion
    #endregion



    #region "--------------------------- Public Propterties ----------------------------"
    #region "------------------------------- Properties --------------------------------"
    public AutoComplete AutoComplete { get; private set; }
    #endregion

    #region "--------------------------------- Events ----------------------------------"

    #endregion
    #endregion
}