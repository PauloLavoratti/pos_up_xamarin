using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using I18NPortable;
using PosUpXamarin.Core.Models;
using Prism.Navigation;

namespace PosUpXamarin.Core.ViewModels
{
    public class TvShowDetailPageViewModel : BaseViewModel, INavigationAware
    {
        private TvShowDetail tvShowDetail;
        public TvShowDetail TvShowDetail
        {
            get => tvShowDetail;
            set => SetProperty(ref tvShowDetail, value);
        }

        private bool contentLoaded;
        public bool ContentLoaded
        { 
            get => contentLoaded;
            set => SetProperty(ref contentLoaded, value); 
        }

        private async Task LoadTvShowDetailAsync(int tvShowId)
        {
            try
            {
                Dialogs.ShowLoading("Loading...".Translate());

                var result = await TheMovieDbService
                    .GetTvShowDetailAsync(tvShowId).ConfigureAwait(false);
                
                if (result != null)
                {
                    ContentLoaded = true;
                    GenreListToString(result);
                    TvShowDetail = result;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                Dialogs.Toast(new ToastConfig("Oops ... There was an error.".Translate()));
            } finally {
                Dialogs.HideLoading();
            }
        }

        public async void OnNavigatingTo(NavigationParameters parameters)
        {
            var tvShow = parameters.GetValue<TvShow>("tvShow");
            Title = tvShow.Name;

            await LoadTvShowDetailAsync(tvShow.Id).ConfigureAwait(false);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        private void GenreListToString(TvShowDetail tvShow)
        {
            string genresNames = 
                tvShow.Genres != null ? 
                string.Join(", ", tvShow.Genres.Select(x => x.Name)) : "Undefined".Translate();
            
            tvShow.GenresNames = genresNames;
        }
    }
}