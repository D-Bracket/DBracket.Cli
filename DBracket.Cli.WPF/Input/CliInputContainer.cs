using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace DBracket.Cli.WPF.Input;

internal class CliInputContainer
{
    #region "----------------------------- Private Fields ------------------------------"

    #endregion



    #region "------------------------------ Constructor --------------------------------"
    public CliInputContainer(TextBox inputTextBox, Popup autoCompletePopup)
    {
        InputTextBox = inputTextBox;
        AutoCompletePopup = autoCompletePopup;
    }
    #endregion



    #region "--------------------------------- Methods ---------------------------------"
    #region "----------------------------- Public Methods ------------------------------"

    #endregion

    #region "----------------------------- Private Methods -----------------------------"

    #endregion

    #region "------------------------------ Event Handling -----------------------------"

    #endregion
    #endregion



    #region "--------------------------- Public Propterties ----------------------------"
    #region "------------------------------- Properties --------------------------------"
    internal TextBox InputTextBox { get; set; }
    internal Popup AutoCompletePopup { get; set; }
    internal AutoCompleteViewModel AutoCompleteViewModel { get; set; } = new AutoCompleteViewModel();

    //internal CommandExecuteData CurrentCommand { get; set; } = new CommandExecuteData();
    #endregion

    #region "--------------------------------- Events ----------------------------------"

    #endregion
    #endregion

}