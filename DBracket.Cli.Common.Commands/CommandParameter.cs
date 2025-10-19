using System.Collections.ObjectModel;

namespace DBracket.Cli.Common.Commands;

public class CommandParameter
{
    #region "----------------------------- Private Fields ------------------------------"
    #endregion



    #region "------------------------------ Constructor --------------------------------"
    /*  Parameter don't always have a value list
     *  - They have a type
     *  - A Description
     *  - Limits
     *  - Maybe Regex
     */

    public CommandParameter(string name, string descriptorKey, Func<string, (bool, AutoCompleteSuggestion?)> valdiationFunction)
    {
        Name = name;
        DescriptorKey = descriptorKey;
        ValdiationFunction = valdiationFunction;
    }
    #endregion



    #region "--------------------------------- Methods ---------------------------------"
    #region "----------------------------- Public Methods ------------------------------"
    //public void UpdateValues(ObservableCollection<string> values)
    //{
    //    _values = values;
    //}

    public ObservableCollection<string> GetAutoComplete(string parameterValue)
    {
        var values = new ObservableCollection<string>();

        return values;
    }
    #endregion

    #region "----------------------------- Private Methods -----------------------------"

    #endregion

    #region "------------------------------ Event Handling -----------------------------"

    #endregion
    #endregion



    #region "--------------------------- Public Propterties ----------------------------"
    #region "------------------------------- Properties --------------------------------"
    public string Name { get { return _name; } set { _name = value; } }
    private string _name = string.Empty;

    public string DescriptorKey { get { return _descriptorKey; } set { _descriptorKey = value; } }
    private string _descriptorKey = string.Empty;

    public Func<string, (bool, AutoCompleteSuggestion?)> ValdiationFunction { get { return _valdiationFunction; } set { _valdiationFunction = value; } }
    private Func<string, (bool, AutoCompleteSuggestion?)> _valdiationFunction;
    #endregion

    #region "--------------------------------- Events ----------------------------------"

    #endregion
    #endregion
}