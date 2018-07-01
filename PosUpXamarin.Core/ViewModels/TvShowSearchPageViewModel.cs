using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using I18NPortable;
using PosUpXamarin.Core.Helpers;
using PosUpXamarin.Core.Models;
using PosUpXamarin.Core.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace PosUpXamarin.Core.ViewModels
{
    public class TvShowSearchPageViewModel : BaseViewModel
    {
        private int currentPage = 1;
        private int totalPage;

        private string searchTerm;
        public string SearchTerm
        {
            get => searchTerm;
            set
            {
                SetProperty(ref searchTerm, value);
                SearchResults.Clear();
            }
        }

        public ObservableRangeCollection<TvShow> SearchResults { get; set; }

        public DelegateCommand SearchCommand { get; }
        public DelegateCommand<TvShow> ShowTvShowDetailCommand { get; }
        public DelegateCommand<TvShow> ItemAppearingCommand { get; }

        private readonly INavigationService navigationService;
        private readonly IPageDialogService pageDialogService;

        public TvShowSearchPageViewModel
        (
            INavigationService navigationService, 
            IPageDialogService pageDialogService
        )
        {
            Title = "Search TV Shows".Translate();
            this.navigationService = navigationService;
            this.pageDialogService = pageDialogService;
            SearchResults = new ObservableRangeCollection<TvShow>();

            SearchCommand = 
                new DelegateCommand(async () => 
                                    await ExecuteSearchCommand().ConfigureAwait(false));
            ShowTvShowDetailCommand = 
                new DelegateCommand<TvShow>(async tvShow => 
                                            await ExecuteShowTvShowDetailCommand(tvShow).ConfigureAwait(false));
            ItemAppearingCommand = 
                new DelegateCommand<TvShow>(async tvShow => 
                                            await ExecuteItemAppearingCommand(tvShow).ConfigureAwait(false));
        }

        private async Task ExecuteSearchCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                SearchResults.Clear();
                currentPage = 1;
                await LoadAsync(currentPage).ConfigureAwait(true);
            }
            finally
            {
                IsBusy = false;
            }

            if (SearchResults.Count == 0)
            {
                await Dialogs.AlertAsync("No results found.".Translate(), string.Empty, "Ok");
            }
        }

        private async Task ExecuteShowTvShowDetailCommand(TvShow tvShow)
        {
            var parameters = new NavigationParameters { { nameof(tvShow), tvShow } };
            await navigationService
                .NavigateAsync(nameof(TvShowDetailPage), parameters).ConfigureAwait(false);
        }

        private async Task ExecuteItemAppearingCommand(TvShow tvShow)
        {
            var viewCellIndex = SearchResults.IndexOf(tvShow);
            if (SearchResults.Count - 2 <= viewCellIndex)
            {
                await NextPageAsync().ConfigureAwait(false);
            }
        }

        private async Task NextPageAsync()
        {
            currentPage++;
            if (currentPage <= totalPage)
            {
                await LoadAsync(currentPage).ConfigureAwait(false);
            }
        }

        private async Task LoadAsync(int page)
        {
            try
            {
                var continueOnCapturedContext = Device.RuntimePlatform == Device.Windows;

                var searchTvShows = await TheMovieDbService
                    .SearchTvShowsAsync(searchTerm, page).ConfigureAwait(continueOnCapturedContext);

                if (searchTvShows != null)
                {
                    totalPage = searchTvShows.TotalPages;
                    SearchResults.AddRange(searchTvShows.TvShows);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                Dialogs.Toast(new ToastConfig("Oops ... There was an error.".Translate()));
            }
        }
    }
}