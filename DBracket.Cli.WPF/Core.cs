using DBracket.Cli.Common;
using DBracket.Cli.WPF.Controls;
using DBracket.Cli.WPF.UI;
using H.NotifyIcon;
using System.Windows;

namespace DBracket.Cli.WPF;

public static class Core
{
    #region "----------------------------- Private Fields ------------------------------"
    private static NotifyIconViewModel? _viewModel;
    private static TaskbarIcon? _taskbarIcon;
    private static Dictionary<string, IDescriptorUI> _descriptors = [];
    #endregion



    #region "------------------------------ Constructor --------------------------------"
    static Core()
    {

    }
    #endregion



    #region "--------------------------------- Methods ---------------------------------"
    #region "----------------------------- Public Methods ------------------------------"
    public static void Dispose()
    {
        _taskbarIcon?.Dispose();
    }

    public static void Initialize(CliWindow window, bool initializeStandardCommands = false)
    {
        if (initializeStandardCommands)
        {
            //CommandCollection.InitStandardCommands();
        }

        _taskbarIcon = Application.Current.FindResource("NotifyIcon") as TaskbarIcon;
        _viewModel = _taskbarIcon?.DataContext as NotifyIconViewModel;
        _viewModel.Init(window);
    }

    public static bool RegisterDescriptor(string DescriptorUIKey, IDescriptorUI iDescriptorUI)
    {
        if (_descriptors.ContainsKey(DescriptorUIKey))
            return false;

        _descriptors.Add(DescriptorUIKey, iDescriptorUI);
        return true;
    }
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