using Acr.UserDialogs;
using I18NPortable;
using PosUpXamarin.Core.DataServices.Interfaces;
using Prism.Mvvm;
using Xamarin.Forms;

namespace PosUpXamarin.Core.ViewModels
{
    public class BaseViewModel : BindableBase
    {
        public ITheMovieDbService TheMovieDbService => DependencyService.Get<ITheMovieDbService>();
        public IUserDialogs Dialogs => UserDialogs.Instance;
        public string this[string key] => key.Translate();

        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        private string title;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }
    }
}
