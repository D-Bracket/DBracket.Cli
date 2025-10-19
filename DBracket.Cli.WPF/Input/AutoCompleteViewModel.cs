using DBracket.Cli.Common;
using DBracket.Cli.WPF.Bases;
using System.Collections.ObjectModel;

namespace DBracket.Cli.WPF.Input;

public class AutoCompleteViewModel : ViewModelBase
{
    #region "----------------------------- Private Fields ------------------------------"

    #endregion



    #region "------------------------------ Constructor --------------------------------"
    public AutoCompleteViewModel()
    {

    }
    #endregion



    #region "--------------------------------- Methods ---------------------------------"
    #region "----------------------------- Public Methods ------------------------------"

    #endregion

    #region "----------------------------- Private Methods -----------------------------"

    #endregion

    #region "------------------------------ Event Handling -----------------------------"

    #endregion

    #region "----------------------------- Command Handling ----------------------------"
    public override void ExecuteCommands(string? command)
    {

    }
    #endregion
    #endregion



    #region "--------------------------- Public Propterties ----------------------------"
    #region "------------------------------- Properties --------------------------------"
    public ObservableCollection<AutoCompleteSuggestion> Suggestions { get { return _suggestions; } set { _suggestions = value; OnMySelfChanged(); } }
    private ObservableCollection<AutoCompleteSuggestion> _suggestions = [];
    public AutoCompleteSuggestion? SelectedSuggestion { get { return _selectedSuggestion; } set { _selectedSuggestion = value; OnMySelfChanged(); } }
    private AutoCompleteSuggestion? _selectedSuggestion;

    public string CurrentError { get { return _currentError; } set { _currentError = value; OnMySelfChanged(); } }
    private string _currentError = "";
    #endregion

    #region "--------------------------------- Events ----------------------------------"

    #endregion

    #region "-------------------------------- Commands ---------------------------------"

    #endregion
    #endregion
}