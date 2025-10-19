using DBracket.Cli.WPF.Bases;
using System.Collections.ObjectModel;

namespace DBracket.Cli.WPF.Plugins.ProjectManagement;

internal class ProjectManagementViewModel : ViewModelBase
{
    #region "----------------------------- Private Fields ------------------------------"

    #endregion



    #region "------------------------------ Constructor --------------------------------"
    public ProjectManagementViewModel()
    {
        var project = new Project("Project 1", "Description of Project 1");
        var task = new Task("Task 1 for Project 1", "Description of Task 1");
        task.Notes.Add(new Note("Note 1"));
        task.Notes.Add(new Note("Note 2"));
        task.Notes.Add(new Note("Note 3"));
        project.Tasks.Add(task);

        task = new Task("Task 2 for Project 1", "Description of Task 2");
        task.Notes.Add(new Note("Note 1"));
        task.Notes.Add(new Note("Note 2"));
        task.Notes.Add(new Note("Note 3"));
        project.Tasks.Add(task);

        task = new Task("Task 3 for Project 1", "Description of Task 3");
        task.Notes.Add(new Note("Note 1"));
        task.Notes.Add(new Note("Note 2"));
        task.Notes.Add(new Note("Note 3"));
        project.Tasks.Add(task);
        Projects.Add(project);



        project = new Project("Project 2", "Description of Project 2");
        task = new Task("Task 1 for Project 2", "Description of Task 1");
        task.Notes.Add(new Note("Note 1"));
        task.Notes.Add(new Note("Note 2"));
        task.Notes.Add(new Note("Note 3"));
        project.Tasks.Add(task);

        task = new Task("Task 2 for Project 2", "Description of Task 2");
        task.Notes.Add(new Note("Note 1"));
        task.Notes.Add(new Note("Note 2"));
        task.Notes.Add(new Note("Note 3"));
        project.Tasks.Add(task);
        
        task = new Task("Task 3 for Project 2", "Description of Task 3");
        task.Notes.Add(new Note("Note 1"));
        task.Notes.Add(new Note("Note 2"));
        task.Notes.Add(new Note("Note 3"));
        project.Tasks.Add(task);
        Projects.Add(project);

        project = new Project("Project 3", "Description of Project 3");
        task = new Task("Task 1 for Project 3", "Description of Task 1");
        task.Notes.Add(new Note("Note 1"));
        task.Notes.Add(new Note("Note 2"));
        task.Notes.Add(new Note("Note 3"));
        project.Tasks.Add(task);
        
        task = new Task("Task 2 for Project 3", "Description of Task 2");
        task.Notes.Add(new Note("Note 1"));
        task.Notes.Add(new Note("Note 2"));
        task.Notes.Add(new Note("Note 3"));
        project.Tasks.Add(task);
        
        task = new Task("Task 3 for Project 3", "Description of Task 3");
        task.Notes.Add(new Note("Note 1"));
        task.Notes.Add(new Note("Note 2"));
        task.Notes.Add(new Note("Note 3"));
        project.Tasks.Add(task);
        Projects.Add(project);
    }
    #endregion



    #region "--------------------------------- Methods ---------------------------------"
    #region "----------------------------- Public Methods ------------------------------"

    #endregion

    #region "----------------------------- Private Methods -----------------------------"

    #endregion

    #region "------------------------------ Event Handling -----------------------------"

    #endregion

    #region "----------------------------- Command Handling ----------------------------"
    public override void ExecuteCommands(string? command)
    {

    }
    #endregion
    #endregion



    #region "--------------------------- Public Propterties ----------------------------"
    #region "------------------------------- Properties --------------------------------"

    public ObservableCollection<Project> Projects { get { return _projects; } set { _projects = value; OnMySelfChanged(); } }
    private ObservableCollection<Project> _projects = [];

    public Project? SelectedProject { get { return _selectedProject; } set { _selectedProject = value; OnMySelfChanged(); } }
    private Project? _selectedProject;

    public Task? SelectedTask { get { return _selectedTask; } set { _selectedTask = value; OnMySelfChanged(); } }
    private Task? _selectedTask;
    #endregion

    #region "--------------------------------- Events ----------------------------------"

    #endregion

    #region "-------------------------------- Commands ---------------------------------"

    #endregion
    #endregion
}