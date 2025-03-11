using AppProjectDev.core.DataAccess;
using AppProjectDev.core.Models;
using Prism.Commands;
using Prism.Dialogs;
using Prism.Mvvm;
using System;

namespace ModuleMain.ViewModels
{
    public class DialogAddProjectViewModel : BindableBase, IDialogAware
    {
        private readonly IDataRepository dataRepository;

        public DelegateCommand<string> Close { get; }

        public DialogAddProjectViewModel(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
            Title = "Add Project";
            Close = new DelegateCommand<string>(CloseDialog);
            RequestClose = new DialogCloseListener(); // Initialize the DialogCloseListener
        }

        private void CloseDialog(string obj)
        {
            var parameters = new DialogParameters { { "Return", Item } };
            ButtonResult result = obj == "Enter" && Item?.Name != null ? ButtonResult.OK : ButtonResult.Cancel;

            RequestClose.Invoke(new DialogResult(result)); // Use the new Invoke method
        }

        public string Title { get; }

        public DialogCloseListener RequestClose { get; } // Replace event with DialogCloseListener

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() { }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Item = parameters.GetValue<ProjectModel>("Item");
        }

        private ProjectModel _item;
        public ProjectModel Item
        {
            get => _item;
            set => SetProperty(ref _item, value);
        }
    }
}
