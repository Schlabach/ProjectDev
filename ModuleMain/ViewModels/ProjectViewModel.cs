using AppProjectDev.core.DataAccess;
using AppProjectDev.core.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ModuleMain.ViewModels
{
    public class ProjectViewModel : BindableBase
    {
        public DelegateCommand AddProject { get; set; }
        public DelegateCommand StartTime { get; set; }
        public DelegateCommand<string> EndTime { get; set; }
        public DelegateCommand Delete { get; set; }
        public DelegateCommand CompleteProject { get; set; }

        IDialogService dialogService;
        IDataRepository dataRepository;
        public ProjectViewModel(IDialogService dialogService, IDataRepository dataRepository)
        {
            this.dialogService = dialogService;
            this.dataRepository = dataRepository;
            AddProject = new DelegateCommand(AddProjectClick);
            Delete = new DelegateCommand(DeleteClick);
            StartTime = new DelegateCommand(StartTimeClick);
            CompleteProject = new DelegateCommand(CompleteProjectClick);
            EndTime = new DelegateCommand<string>(EndTimeClick);
            Projects = new ObservableCollection<ProjectModel>();
            Time = new ObservableCollection<TimeModel>();


            SetUsers();
            SetView();
        }
        //End Time Button Click
        private void EndTimeClick(string id)
        {
            var p = new DialogParameters();
            TimeModel item = Time.First(x => x.ID == Int32.Parse(id));
            item.End_Date = DateTime.Now;
            item.Time = item.End_Date - item.Start_Date;
            p.Add("Item", item);
            dialogService.ShowDialog("DialogEndTimeView", p, result =>
            {
                TimeModel ret = result.Parameters.GetValue<TimeModel>("Return");
                if (result.Result != ButtonResult.Cancel && ret != null)
                {
                    dataRepository.EndTime(ret);
                    Time.Remove(Time.First(x => x.ID == Int32.Parse(id)));
                    SetView();
                }
            });
        }
        //Start Time Button Click
        private void StartTimeClick()
        {
            if (SelectedUser != null)
            {
                dataRepository.AddTime(SelectedProject.Id, SelectedUser.Id);
                Time.Clear();
                Time.AddRange(dataRepository.GetTimeInProgress(SelectedUser.Id));
            }

        }

        //Adds the users to the dropdown and sets the logged in user
        private void SetUsers()
        {
            Users = dataRepository.GetUsers();

            //SelectedUser = Users.FirstOrDefault(x => x.UserName == Environment.UserName);

        }
        // Binds to X button to delete a project        
        private void DeleteClick()
        {

            if (MessageBox.Show("Are you sure you want to delete this?", "", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                dataRepository.RemoveProject(SelectedProject.Id);
            }
            else
            {

            }
        }
        private void CompleteProjectClick()
        {

            if (MessageBox.Show("Are you sure you want to complete this?", "", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                dataRepository.Complete(SelectedProject.Id);
            }
        }
        // Initiates the view adds the projects and the times
        private void SetView()
        {
            Projects.Clear();
            Projects.AddRange(dataRepository.GetProjects());
        }
        // Binds to the add project button; adds a new project
        private void AddProjectClick()
        {
            ProjectModel item = new ProjectModel();
            var p = new DialogParameters();
            p.Add("Item", item);
            dialogService.ShowDialog("DialogAddProjectView", p, result =>
            {
                ProjectModel ret = result.Parameters.GetValue<ProjectModel>("Return");
                if (result.Result != ButtonResult.Cancel && ret != null)
                {
                    dataRepository.AddProject(ret);
                    SetView();
                }
            });
        }
        private ObservableCollection<ProjectModel> _projects;
        public ObservableCollection<ProjectModel> Projects
        {
            get { return _projects; }
            set { SetProperty(ref _projects, value); }
        }
        private ProjectModel _selectedProject;
        public ProjectModel SelectedProject
        {
            get { return _selectedProject; }
            set { SetProperty(ref _selectedProject, value); OnSelectedProject(); }
        }

        private void OnSelectedProject()
        {
            if (SelectedProject != null)
            {
                IsSelected = true;

            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }

        private ObservableCollection<TimeModel> _time;
        public ObservableCollection<TimeModel> Time
        {
            get { return _time; }
            set { SetProperty(ref _time, value); }
        }

        private List<UserModel> _users;
        public List<UserModel> Users
        {
            get { return _users; }
            set { SetProperty(ref _users, value); }
        }
        private UserModel _selectedUser;
        public UserModel SelectedUser
        {
            get { return _selectedUser; }
            set { SetProperty(ref _selectedUser, value); OnSelectedUser(); }
        }

        private void OnSelectedUser()
        {
            if (_selectedUser != null)
            {
                Time.Clear();
                Time.AddRange(dataRepository.GetTimeInProgress(SelectedUser.Id));
            }
        }
    }

}
