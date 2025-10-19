namespace DBracket.Cli.Common.Commands.StandardCommands;

public class AOpen : CommandBase
{
    #region "----------------------------- Private Fields ------------------------------"
    private List<string> _parameterValues = [];

    private AutoCompleteSuggestion _commandSuggestion;
    #endregion



    #region "------------------------------ Constructor --------------------------------"
    public AOpen(string commandDescriptor, string paramterDescriptor) : base("AOpen", "aopen", "AOpen")
    {
        CommandCore.InitializeInstalledApplications();

        var commandDescriptorContext = new CommandDescriptor("Test");

        _commandSuggestion = new AutoCompleteSuggestion(commandDescriptor, commandDescriptorContext)
        {
            SuggestedSolution = "AOpen"
        };

        var parameter = new CommandParameter("AppName", paramterDescriptor, ValidateApplicationName)
        {

        };

        Parameters.Add(parameter);
        for (int i = 0; i < Parameters.Count; i++)
        {
            _parameterValues.Add("");
        }
    }
    #endregion

    private (bool successful, AutoCompleteSuggestion?) ValidateApplicationName(string text)
    {
        var app = CommandCore.InstalledApps?.FirstOrDefault(x => x.DisplayedName == text);
        if (app is not null)
        {
            _parameterValues[0] = text;
            return (true, null); // TODO Return Suggestion with App Information
        }

        var suggestedApps = CommandCore.InstalledApps?.Where(x => x.DisplayedName.Contains(text));
        // TODO Return List of Suggestions
        return (false, null);
    }
    //private void InitParameters()
    //{
    //    var parameter = new CommandParameter()
    //    {
    //        Name = "AppName",
    //        DescriptorKey = uiDescriptorKey
    //    };

    //    Parameters.Add(parameter);
    //}

    #region "--------------------------------- Methods ---------------------------------"
    #region "----------------------------- Public Methods ------------------------------"
    public override bool Execute()
    {
        if (_isReadyToExecute == false)
            return false;


        //var appName = parameters["0"];
        //var appInfo = CommandCore.InstalledApps?.FirstOrDefault(x => x.DisplayedName == appName);
        //if (appInfo is null)
        //    return false;


        //var filePath = string.Empty;
        //if (appInfo.DisplayeIcon.EndsWith(".exe"))
        //{
        //    filePath = appInfo.DisplayeIcon;
        //}
        //else
        //{
        //    var index = appInfo.DisplayeIcon.IndexOf(".exe,");
        //    if (index == -1)
        //    {
        //        Debug.WriteLine($"Error, while trying to open {appInfo.DisplayeIcon}");
        //        return false;
        //    }
        //    filePath = appInfo.DisplayeIcon.Substring(0, index + 4);
        //}

        //Process.Start(filePath);
        //_inputTextBox.Text = string.Empty;

        _isReadyToExecute = false;
        return true;
    }

    private bool _isReadyToExecute;

    // Just give the complete string in here and handle everything, if parse is valid enable execution
    // After execution reset parameters
    public override List<AutoCompleteSuggestion>? GetSuggestions(string text)
    {
        if (string.IsNullOrEmpty(text))
            return null;

        var suggestions = new List<AutoCompleteSuggestion>();
        var tokens = text.Split(' ');
        for (int i = 0; i < tokens.Length; i++)
        {
            if (i == 0)
            {
                // Check Command
                if (tokens[0] == Identiefier)
                    continue;

                if (Identiefier.StartsWith(tokens[0].ToLower()))
                {
                    _parameterValues.Clear();
                    return [_commandSuggestion];
                }

                return null;
            }

            // Check Parameters
            if (i > Parameters.Count)
            {
                // TODO ERROR;
                return null;
            }

            if (tokens.Length == 1 || (tokens.Length==2 && tokens[1] ==""))
            {
                foreach (var app in CommandCore.InstalledApps)
                {
                    var parameterContext = new CommandParameterDescriptor();
                    var suggestion = new AutoCompleteSuggestion(Parameters[0].DescriptorKey, parameterContext)
                    {
                        SuggestedSolution = app.DisplayedName
                    };
                    suggestions.Add(suggestion);
                }
            }
        }
        return suggestions;
    }

    //public override bool Parse(string text)
    //{
    //    throw new NotImplementedException();
    //}

    //public override AutoCompleteSuggestion Parse2(string text)
    //{
    //    throw new NotImplementedException();
    //}

    //public override bool ParseParameter(int number, string text)
    //{
    //    throw new NotImplementedException();
    //}
    #endregion

    #region "----------------------------- Private Methods -----------------------------"

    #endregion

    #region "------------------------------ Event Handling -----------------------------"

    #endregion
    #endregion



    #region "--------------------------- Public Propterties ----------------------------"
    #region "------------------------------- Properties --------------------------------"

    #endregion

    #region "--------------------------------- Events ----------------------------------"

    #endregion
    #endregion

}