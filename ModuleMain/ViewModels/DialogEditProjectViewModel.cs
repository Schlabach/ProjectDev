using AppProjectDev.core.DataAccess;
using AppProjectDev.core.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleMain.ViewModels
{
    public class DialogEditProjectViewModel : BindableBase, IDialogAware
    {
        IDataRepository dataRepository;
        public DelegateCommand<string> Close { get; private set; }
        public DialogEditProjectViewModel(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
            Title = "Add Project";
            Close = new DelegateCommand<string>(CloseDialog);
        }
        // Add a constructor that accepts the ProjectModel as a parameter
        public DialogEditProjectViewModel(IDataRepository dataRepository, ProjectModel project)
            : this(dataRepository)
        {
            Item = project;
        }

        private void CloseDialog(string obj)
        {
            var p = new DialogParameters();
            p.Add("Return", Item);
            ButtonResult result = new ButtonResult();
            if (obj == "Enter")
            {
                if (Item.Name != null)
                {
                    result = ButtonResult.OK;
                    RequestClose?.Invoke(new DialogResult(result, p));
                }

            }
            else
            {
                result = ButtonResult.Cancel;
                RequestClose?.Invoke(new DialogResult(result, p));
            }


        }
        private ProjectModel _selectedProject;
        public ProjectModel SelectedProject
        {
            get { return _selectedProject; }
            set { SetProperty(ref _selectedProject, value); }
        }

        public string Title { get; }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Item = parameters.GetValue<ProjectModel>("Item");
        }
        private ProjectModel _item;
        public ProjectModel Item
        {
            get { return _item; }
            set { SetProperty(ref _item, value); }
        }
    }
}
