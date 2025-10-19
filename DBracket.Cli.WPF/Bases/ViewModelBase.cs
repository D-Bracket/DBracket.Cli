using DBracket.Cli.Common;
using DBracket.Cli.WPF.Input;
using System.Windows.Input;

namespace DBracket.Cli.WPF.Bases;

/// <summary>Baseclass for ViewModels</summary>
public abstract class ViewModelBase : PropertyChangedBase
{
    #region "----------------------------- Private Fields ------------------------------"

    #endregion



    #region "------------------------------ Constructor --------------------------------"

    #endregion



    #region "--------------------------------- Methods ---------------------------------"
    #region "----------------------------- Public Methods ------------------------------"

    #endregion

    #region "----------------------------- Private Methods -----------------------------"

    #endregion

    #region "------------------------------ Event Handling -----------------------------"

    #endregion

    #region "----------------------------- Command Handling ----------------------------"
    /// <summary>Executes the commands from the UI</summary>
    /// <param name="command">Command that has to be executed</param>
    public abstract void ExecuteCommands(string? command);
    #endregion
    #endregion


    #region "--------------------------- Public Propterties ----------------------------"
    #region "------------------------------- Properties --------------------------------"

    #endregion

    #region "--------------------------------- Events ----------------------------------"

    #endregion

    #region "-------------------------------- Commands ---------------------------------"
    /// <summary>Commands from the UI</summary>
    public ICommand Commands => new StringCommand(ExecuteCommands);
    #endregion
    #endregion
}