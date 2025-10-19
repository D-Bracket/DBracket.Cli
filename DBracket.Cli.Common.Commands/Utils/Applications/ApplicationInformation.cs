namespace DBracket.Cli.Common.Commands.Utils.Applications;

public class ApplicationInformation
{
    #region "----------------------------- Private Fields ------------------------------"

    #endregion



    #region "------------------------------ Constructor --------------------------------"
    public ApplicationInformation(string displayedName, string displayeIcon, string installLocation, string uninstallString)
    {
        DisplayedName = displayedName;
        DisplayeIcon = displayeIcon;
        InstallLocation = installLocation;
        UninstallString = uninstallString;
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
    public string DisplayedName { get; set; }
    public string DisplayeIcon { get; set; }
    public string InstallLocation { get; set; }
    public string UninstallString { get; set; }
    #endregion

    #region "--------------------------------- Events ----------------------------------"

    #endregion
    #endregion
}