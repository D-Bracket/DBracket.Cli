using System.Windows.Controls;

namespace DBracket.Cli.WPF.Plugins.ProjectManagement;

/// <summary>
/// Interaction logic for ProjectManagementView.xaml
/// </summary>
public partial class ProjectManagementView : UserControl
{
    #region "----------------------------- Private Fields ------------------------------"

    #endregion



    #region "------------------------------ Constructor --------------------------------"
    public ProjectManagementView()
    {
        InitializeComponent();
        DataContext = ViewModel;
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
    internal ProjectManagementViewModel ViewModel { get; } = new ProjectManagementViewModel();
    #endregion

    #region "--------------------------------- Events ----------------------------------"

    #endregion
    #endregion
}