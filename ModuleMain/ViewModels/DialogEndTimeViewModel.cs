using AppProjectDev.core.DataAccess;
using AppProjectDev.core.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleMain.ViewModels
{
    public class DialogEndTimeViewModel : BindableBase, IDialogAware
    {
        IDataRepository dataRepository;
        public DelegateCommand<string> Close { get; private set; }
        public DialogEndTimeViewModel(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
            Close = new DelegateCommand<string>(CloseDialog);


        }

        private void CloseDialog(string obj)
        {
            var p = new DialogParameters();
            p.Add("Return", Item);
            ButtonResult result = new ButtonResult();
            if (obj == "Enter")
            {

                result = ButtonResult.OK;
                RequestClose?.Invoke(new DialogResult(result));


            }
            else
            {
                result = ButtonResult.Cancel;
                RequestClose?.Invoke(new DialogResult(result));
            }


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
            Item = parameters.GetValue<TimeModel>("Item");
            Item.Time = DateTime.Now - Item.Start_Date;


        }

        private TimeModel _item;
        public TimeModel Item
        {
            get { return _item; }
            set { SetProperty(ref _item, value); }
        }

        DialogCloseListener IDialogAware.RequestClose => throw new NotImplementedException();
    }
}
