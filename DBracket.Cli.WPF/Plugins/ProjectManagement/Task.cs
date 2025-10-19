using DBracket.Cli.Common;
using System.Collections.ObjectModel;

namespace DBracket.Cli.WPF.Plugins.ProjectManagement;

public class Task : PropertyChangedBase
{
    #region "----------------------------- Private Fields ------------------------------"

    #endregion



    #region "------------------------------ Constructor --------------------------------"
    public Task(string title, string description)
    {
        _title = title;
        _description = description;
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
    public string Title { get { return _title; } set { _title = value; OnMySelfChanged(); } }
    private string _title;

    public string Description { get { return _description; } set { _description = value; OnMySelfChanged(); } }
    private string _description;

    public ObservableCollection<Note> Notes { get { return _notes; } set { _notes = value; OnMySelfChanged(); } }
    private ObservableCollection<Note> _notes = [];
    #endregion

    #region "--------------------------------- Events ----------------------------------"

    #endregion
    #endregion
}