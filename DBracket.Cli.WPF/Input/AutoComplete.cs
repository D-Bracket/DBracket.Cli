using DBracket.Cli.Common;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace DBracket.Cli.WPF.Input;

public class AutoComplete : PropertyChangedBase
{
    #region "----------------------------- Private Fields ------------------------------"
    private CliInputContainer _inputContainer;
    #endregion



    #region "------------------------------ Constructor --------------------------------"
    internal AutoComplete(CliInputContainer inputContainer)
    {
        _inputContainer = inputContainer;
        _inputContainer.InputTextBox.TextChanged += HandleInputChanged;
    }
    #endregion



    #region "--------------------------------- Methods ---------------------------------"
    #region "----------------------------- Public Methods ------------------------------"
    public bool AttemptCompletion()
    {
        if (IsShown == false)
            return true;

        if (_inputContainer.AutoCompleteViewModel.SelectedSuggestion is null)
            return false;

        //_inputContainer.InputTextBox.Text = _inputContainer.AutoCompleteViewModel.SelectedSuggestion.Identiefier;
        //_inputContainer.InputTextBox.CaretIndex = _inputContainer.InputTextBox.Text.Length;

        //var result = _inputContainer.CurrentCommand.CheckIfInputIsComplete();
        //_autoCompletePopup.IsOpen = false;
        return true;
    }

    public void UseSelectedSuggestion(bool executeCommand = false)
    {
        if (_inputContainer.AutoCompleteViewModel.SelectedSuggestion is null)
            return;

        // AutoComplete
        // Command Parameter
        // Parameter kann alles mögliche sein, vielleicht etwas das autocompleted werden kann oder vielleicht etwas mit einem int wert (kein autocomplete)
        // oder beides (mehrere parameter mit int oder vielleicht mit einem enum) -> Live anzeigen, welche Werte das Enum annehmen kann!!!!!!!
    }

    //public bool UpdateSuggestions(ObservableCollection<string> test)
    //{
    //    //_inputContainer.AutoCompleteViewModel.Suggestions = Core.CommandCollection.GetCommandsByFilter(filter);
    //    //if (_inputContainer.AutoCompleteViewModel.Suggestions.Count == 1)
    //    //    _inputContainer.AutoCompleteViewModel.SelectedSuggestion = _inputContainer.AutoCompleteViewModel.Suggestions[0];
    //    //return _inputContainer.AutoCompleteViewModel.Suggestions.Count > 0;
    //}

    public void SelectPrevious()
    {
        if (_inputContainer.AutoCompleteViewModel.Suggestions.Count == 0)
            return;

        if (_inputContainer.AutoCompleteViewModel.SelectedSuggestion is null)
        {
            _inputContainer.AutoCompleteViewModel.SelectedSuggestion = _inputContainer.AutoCompleteViewModel.Suggestions[0];
            return;
        }

        var currentIndex = _inputContainer.AutoCompleteViewModel.Suggestions.IndexOf(_inputContainer.AutoCompleteViewModel.SelectedSuggestion);
        if (currentIndex < 1)
        {
            _inputContainer.AutoCompleteViewModel.SelectedSuggestion = _inputContainer.AutoCompleteViewModel.Suggestions[0];
            return;
        }

        _inputContainer.AutoCompleteViewModel.SelectedSuggestion = _inputContainer.AutoCompleteViewModel.Suggestions[currentIndex - 1];
    }

    public void SelectNext()
    {
        if (_inputContainer.AutoCompleteViewModel.Suggestions.Count == 0)
            return;

        if (_inputContainer.AutoCompleteViewModel.SelectedSuggestion is null)
        {
            _inputContainer.AutoCompleteViewModel.SelectedSuggestion = _inputContainer.AutoCompleteViewModel.Suggestions[0];
            return;
        }

        var currentIndex = _inputContainer.AutoCompleteViewModel.Suggestions.IndexOf(_inputContainer.AutoCompleteViewModel.SelectedSuggestion);
        if (currentIndex >= _inputContainer.AutoCompleteViewModel.Suggestions.Count - 2)
            return;

        _inputContainer.AutoCompleteViewModel.SelectedSuggestion = _inputContainer.AutoCompleteViewModel.Suggestions[currentIndex + 1];
    }
    #endregion

    #region "----------------------------- Private Methods -----------------------------"    
    public void RegisterAutoCompleteSource(AutoCompleteSource autoCompleteSource)
    {
        _autoCompleteSources.Add(autoCompleteSource);
    }

    private List<AutoCompleteSource> _autoCompleteSources = [];

    #endregion

    #region "------------------------------ Event Handling -----------------------------"
    private void HandleInputChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox inputTextBox)
            return;



        // 19.10.2025:
        // TODO Disabled for now, because it is not ready yet and I want to release a version to use in my everyday work flow, to test it.

        //ObservableCollection<AutoCompleteSuggestion> suggestions = new();
        //foreach (var source in _autoCompleteSources)
        //{
        //    var t = source.GetSuggestions(inputTextBox.Text);
        //    //if (t is not null)
        //    //{
        //    //    suggestions.Add(t);
        //    //}

        //    foreach (var suggestion in t)
        //    {
        //        suggestions.Add(suggestion);
        //    }
        //}
        //_inputContainer.AutoCompleteViewModel.Suggestions = suggestions;
        //if (suggestions.Count > 0)
        //    _inputContainer.AutoCompleteViewModel.SelectedSuggestion = suggestions[0];

        //_inputContainer.AutoCompleteViewModel.CurrentError = "";
        //_inputContainer.AutoCompletePopup.IsOpen = true;
    }
    #endregion
    #endregion



    #region "--------------------------- Public Propterties ----------------------------"
    #region "------------------------------- Properties --------------------------------"
    public bool IsShown { get { return _inputContainer.AutoCompletePopup.IsOpen; } }
    #endregion

    #region "--------------------------------- Events ----------------------------------"

    #endregion
    #endregion
}