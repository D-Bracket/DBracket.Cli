namespace DBracket.Cli.Common;

public class AutoCompleteSuggestion : PropertyChangedBase
{
    #region "----------------------------- Private Fields ------------------------------"

    #endregion



    #region "------------------------------ Constructor --------------------------------"
    public AutoCompleteSuggestion(string descriptorKey, IDescriptorDataContext descriptorDataContext)
    {
        _descriptorKey = descriptorKey;
        _descriptorDataContext = descriptorDataContext;
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

    #endregion
    #endregion



    #region "--------------------------- Public Propterties ----------------------------"
    #region "------------------------------- Properties --------------------------------"
    public string SuggestedSolution { get { return _suggestedSolution; } set { _suggestedSolution = value; OnMySelfChanged(); } }
    private string _suggestedSolution = string.Empty;

    public string DescriptorKey { get { return _descriptorKey; } set { _descriptorKey = value; OnMySelfChanged(); } }
    private string _descriptorKey;

    public IDescriptorDataContext DescriptorDataContext { get { return _descriptorDataContext; } set { _descriptorDataContext = value; OnMySelfChanged(); } }
    private IDescriptorDataContext _descriptorDataContext;
    
    #endregion

    #region "--------------------------------- Events ----------------------------------"

    #endregion

    #region "-------------------------------- Commands ---------------------------------"

    #endregion
    #endregion
}