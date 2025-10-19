using DBracket.Cli.Common.WinApi;
using DBracket.Cli.WPF.Bases;
using DBracket.Cli.WPF.Controls;
using DBracket.Cli.WPF.Plugins;
using DBracket.Cli.WPF.Plugins.ProjectManagement;
using System.Windows;
using System.Windows.Input;

namespace DBracket.Cli.WPF.UI;

internal class NotifyIconViewModel : ViewModelBase
{
    #region "----------------------------- Private Fields ------------------------------"
    //private QuickAccessView _view;
    private CliWindow? _view;
    private KeyboardHookControl _hook;
    #endregion



    #region "------------------------------ Constructor --------------------------------"
    public NotifyIconViewModel()
    {
        //ModifierKeys mods = Keyboard.Modifiers;
        //if ((mods & ModifierKeys.Control) == ModifierKeys.Control)
        //{
        //    Debug.WriteLine("Control is pressed.");
        //}
        //_view = new();
        //Keyboard.AddPreviewKeyDownHandler(_view, OnPreviewKeyDown);
        _hook = new KeyboardHookControl();
        KeyboardHookControl.KeyPressReport += HandleKeyPressed;
        _pluginWindow.Initialize();
    }


    #endregion



    #region "--------------------------------- Methods ---------------------------------"
    #region "----------------------------- Public Methods ------------------------------"
    internal void Init(CliWindow window)
    {
        _view = window;

        // TODO, Check if window is initialized
    }
    #endregion

    #region "----------------------------- Private Methods -----------------------------"
    private PluginWindow? _pluginWindow = new();

    private ProjectManagementView? _projectManagementView = new();
    #endregion

    #region "------------------------------ Event Handling -----------------------------"
    private (bool handled, int t) HandleKeyPressed(Common.WinApi.Key key)
    {
        if (_view is null ||
            _view.InputHandler is null)
            return(false, 0);

        ModifierKeys mods = Keyboard.Modifiers;
        //if ((mods & ModifierKeys.Control) == ModifierKeys.Control)
        //{
        //    Debug.WriteLine("Control is pressed.");
        //}
        if (key == Common.WinApi.Key.VK_E)
        {
            var modifiers = _hook.GetKeyModifiers();
            if (modifiers.Control && modifiers.Windows)
            {
                _view.ShowAtCenter(topOffset: -100.0, leftOffset: 0.0);
                return (handled: true, t: 9);
            }
        }
        else if (key == Common.WinApi.Key.VK_Escape)
        {
            if (_view.IsShown)
            {
                _view.Hide();
                return (true, 9);
            }
            else if (_pluginWindow.IsShown)
            {
                _pluginWindow.Hide();
                return (true, 9);
            }
        }
        else if (key == Common.WinApi.Key.VK_Up &&
            _view.IsShown &&
            _view.InputHandler.AutoComplete.IsShown)
        {
            _view.InputHandler.AutoComplete.SelectPrevious();
            return (true, 9);
        }
        else if (key == Common.WinApi.Key.VK_Down &&
            _view.IsShown &&
            _view.InputHandler.AutoComplete.IsShown)
        {
            _view.InputHandler.AutoComplete.SelectNext();
            return (true, 9);
        }
        else if (key == Common.WinApi.Key.VK_Tab &&
            _view.IsShown &&
            _view.InputHandler.AutoComplete.IsShown)
        {
            _view.InputHandler.AutoComplete.AttemptCompletion();
            return (true, 9);
        }
        else if (key == Common.WinApi.Key.VK_Return
            && _view.IsShown)
        {
            if (_view.InputHandler._inputContainer.InputTextBox.Text.Contains("prjm"))
            {
                //_view.viewmod
                _view.FadeOutAndClose();
                _pluginWindow.PluginContentPresenter.Content = _projectManagementView;
                _pluginWindow.FadeInAndShow();
            }
            //var result = _view.AttemptAutoCompletion();
            //if (result == false)
            //    return (true, 9);

            //_view.ExecuteApp(_view.AutoComplete.SelectedSuggestion.DisplayedName);
            ////////////if (_view.IsAutoCompleteShown)
            ////////////{
            ////////////    _view.AttemptAutoCompletion();
            ////////////}
            ////////////else
            ////////////{
            ////////////    var result = _view.AttemptAutoCompletion();
            ////////////    if (result == false)
            ////////////        return (true, 9);

            ////////////    _view.ExecuteApp(_view.AutoComplete.SelectedSuggestion.DisplayedName);
            ////////////}
            return (true, 9);
        }
        if (key == Common.WinApi.Key.VK_F)
        {

        }

        return (false, 9);
        //// Example: Block the Escape key
        //if (e.Key == Key.Escape)
        //{
        //    MessageBox.Show("Escape was pressed.");
        //    if ((mods & ModifierKeys.Control) == ModifierKeys.Control)
        //    {
        //        _view.Show();
        //        e.Handled = true;
        //    }
        //}
    }
    private void OnPreviewKeyDown(object sender, KeyEventArgs e)
    {
        //ModifierKeys mods = Keyboard.Modifiers;
        //if ((mods & ModifierKeys.Control) == ModifierKeys.Control)
        //{
        //    Console.WriteLine("Control is pressed.");
        //}

        //// Example: Block the Escape key
        //if (e.Key == Key.Escape)
        //{
        //    MessageBox.Show("Escape was pressed.");
        //    if ((mods & ModifierKeys.Control) == ModifierKeys.Control)
        //    {
        //        _view.Show();
        //        e.Handled = true;
        //    }
        //}
    }
    #endregion

    #region "----------------------------- Command Handling ----------------------------"
    public override void ExecuteCommands(string? command)
    {
        switch (command)
        {
            case "ShowWindow":
                //Application.Current.MainWindow ??= new QuickAccessView();
                //Application.Current.MainWindow.Show();// disableEfficiencyMode: true);
                _view.Show();
                break;

            case "HideWindow":
                _view.Hide();//enableEfficiencyMode: true);
                break;

            case "ExitApplication":
                Application.Current.Shutdown();
                break;

            default:
                break;
        }
    }
    #endregion
    #endregion



    #region "--------------------------- Public Propterties ----------------------------"
    #region "------------------------------- Properties --------------------------------"

    #endregion

    #region "--------------------------------- Events ----------------------------------"

    #endregion

    #region "-------------------------------- Commands ---------------------------------"

    #endregion
    #endregion
}