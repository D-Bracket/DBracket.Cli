namespace DBracket.Cli.Common;

public abstract class AutoCompleteSource
{
    #region "----------------------------- Private Fields ------------------------------"

    #endregion



    #region "------------------------------ Constructor --------------------------------"
    /*   Problem:
     *   - My commands need autocomplete for commands and parameters. The autocompletion is split into to phases.
     *      - Options in the cli, split autocomplete -> Option for separator (Space in my case)
     *      - The CLI knows, if the separator is found, move to the next phase of autocompletion
     *   - I want to return an expressive descriptor for the autocompletion, for example the command and then the parameters
     *      - The Descriptor should be generic
     *          -> Should I pull the descriptor (view) out of the suggestion?
     *              -> Dictionary of [string, object] in Cli.Common? Register Descriptors, in the suggestion is the key of the descriptor that should be used
     */
    #endregion



    #region "--------------------------------- Methods ---------------------------------"
    #region "----------------------------- Public Methods ------------------------------"
    // For my case:
    // level 0: Command
    // level 1: Parameter 1
    // level 2: Parameter 2
    // aso.
    public abstract List<AutoCompleteSuggestion>? GetSuggestions(string text);
    #endregion

    #region "----------------------------- Private Methods -----------------------------"

    #endregion

    #region "------------------------------ Event Handling -----------------------------"

    #endregion
    #endregion



    #region "--------------------------- Public Propterties ----------------------------"
    #region "------------------------------- Properties --------------------------------"
    public string DescriptorKey { get; set; } = string.Empty;
    #endregion

    #region "--------------------------------- Events ----------------------------------"

    #endregion
    #endregion
}