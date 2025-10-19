using DBracket.Cli.Common;

namespace DBracket.Cli.WPF.Plugins.ProjectManagement;

public class Note : PropertyChangedBase
{
    #region "----------------------------- Private Fields ------------------------------"

    #endregion



    #region "------------------------------ Constructor --------------------------------"
    public Note(string description)
    {
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
    public string Description { get { return _description; } set { _description = value; OnMySelfChanged(); } }
    private string _description;
    #endregion

    #region "--------------------------------- Events ----------------------------------"

    #endregion
    #endregion
}