using DBracket.Cli.Common.Commands.StandardCommands;
using DBracket.Cli.Common.Commands.Utils.Applications;

namespace DBracket.Cli.Common.Commands;

public static class CommandCore
{
    #region "----------------------------- Private Fields ------------------------------"
    private static bool _installedApplicationsInitialized;
    #endregion



    #region "------------------------------ Constructor --------------------------------"

    #endregion



    #region "--------------------------------- Methods ---------------------------------"
    #region "----------------------------- Public Methods ------------------------------"
    public static void RegisterCommand(CommandBase command)
    {
        
    }

    public static void InitializeInstalledApplications()
    {
        if (_installedApplicationsInitialized)
            return;

        _installedApplicationsInitialized = true;
        InstalledApps = InstalledApplications.GetInstalledApps();
    }
    #endregion

    #region "----------------------------- Private Methods -----------------------------"

    #endregion

    #region "------------------------------ Event Handling -----------------------------"

    #endregion
    #endregion



    #region "--------------------------- Public Propterties ----------------------------"
    #region "------------------------------- Properties --------------------------------"
    public static List<CommandBase> RegisteredCommands { get; set; } = [];
    
    public static InstalledApplications? InstalledApps { get; private set; }

    
    #endregion

    #region "--------------------------------- Events ----------------------------------"

    #endregion
    #endregion
}