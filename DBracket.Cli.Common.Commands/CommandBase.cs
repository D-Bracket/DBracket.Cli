using System.Collections.ObjectModel;

namespace DBracket.Cli.Common.Commands;

public abstract class CommandBase : AutoCompleteSource
{
    #region "----------------------------- Private Fields ------------------------------"

    #endregion



    #region "------------------------------ Constructor --------------------------------"
    public CommandBase(string name, string identiefier, string shortIdentiefier)
    {
        _name = name;
        _identiefier = identiefier;
        _shortIdentiefier = shortIdentiefier;
    }
    #endregion



    #region "--------------------------------- Methods ---------------------------------"
    #region "----------------------------- Public Methods ------------------------------"
    //public abstract bool Parse(string text);
    //public abstract AutoCompleteSuggestion Parse2(string text);

    //public abstract bool ParseParameter(int number, string text);

    public abstract bool Execute();
    #endregion

    #region "----------------------------- Private Methods -----------------------------"

    #endregion

    #region "------------------------------ Event Handling -----------------------------"

    #endregion
    #endregion



    #region "--------------------------- Public Propterties ----------------------------"
    #region "------------------------------- Properties --------------------------------"
    public string Name { get { return _name; } set { _name = value; } }
    private string _name;


    public string Identiefier { get { return _identiefier; } set { _identiefier = value; } }
    private string _identiefier;

    public string ShortIdentiefier { get { return _shortIdentiefier; } set { _shortIdentiefier = value; } }
    private string _shortIdentiefier;

    public string CommandStructure { get { return _commandStructure; } set { _commandStructure = value; } }
    private string _commandStructure = string.Empty;

    public ObservableCollection<CommandParameter> Parameters { get { return _parameter; } set { _parameter = value; } }
    private ObservableCollection<CommandParameter> _parameter = [];
    #endregion

    #region "--------------------------------- Events ----------------------------------"

    #endregion
    #endregion
}