using DBracket.Cli.Common.Commands;
using DBracket.Cli.Common.Commands.StandardCommands;
using System.Windows;

namespace DBracket.Cli.WPF.Sample;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    #region "----------------------------- Private Fields ------------------------------"
    private MainWindow? _mainWindow;
    #endregion



    #region "------------------------------ Constructor --------------------------------"

    #endregion



    #region "--------------------------------- Methods ---------------------------------"
    #region "----------------------------- Public Methods ------------------------------"

    #endregion

    #region "----------------------------- Private Methods -----------------------------"
    private void InitializeCommands()
    {
        // Register Descriptors
        var commandDescriptor = new CommandDescriptor();
        Core.RegisterDescriptor("CommandDescriptor", commandDescriptor);

        // Register Commands
        var command = new AOpen("CommandDescriptor", "CommandDescriptor");
        CommandCore.RegisterCommand(command);


        _mainWindow?.InputHandler?.AutoComplete.RegisterAutoCompleteSource(command);
    }
    #endregion

    #region "------------------------------ Event Handling -----------------------------"
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        _mainWindow = new MainWindow();
        Core.Initialize(_mainWindow);

        InitializeCommands();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _mainWindow?.Close();
        _mainWindow?.Dispose();
        Core.Dispose();

        base.OnExit(e);
    }
    #endregion
    #endregion



    #region "--------------------------- Public Propterties ----------------------------"
    #region "------------------------------- Properties --------------------------------"

    #endregion

    #region "--------------------------------- Events ----------------------------------"

    #endregion
    #endregion
}