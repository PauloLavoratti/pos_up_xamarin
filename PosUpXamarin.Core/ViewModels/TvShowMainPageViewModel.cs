using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using I18NPortable;
using Plugin.Connectivity;
using PosUpXamarin.Core.Helpers;
using PosUpXamarin.Core.Models;
using PosUpXamarin.Core.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace PosUpXamarin.Core.ViewModels
{
    public class TvShowMainPageViewModel : BaseViewModel
    {
        private int currentPage;
        private int totalPage;

        private bool isConnected;
        public bool IsConnected
        {
            get => isConnected;
            set => SetProperty(ref isConnected, value);
        }

        public ObservableRangeCollection<TvShow> TvShows { get; set; }
        public string MessageInternetConnectionFailed =>
            "You need an internet connection. Check your connection and try again.".Translate();

        public DelegateCommand LoadTopRatedTvShowsCommand { get; }
        public DelegateCommand ShowSearchTvShowCommand { get; }
        public DelegateCommand<TvShow> ShowTvShowDetailCommand { get; }
        public DelegateCommand<TvShow> ItemAppearingCommand { get; }

        private readonly INavigationService navigationService;
        public TvShowMainPageViewModel(INavigationService navigationService)
        {
            Title = "TV Shows".Translate();
            this.navigationService = navigationService;
            TvShows = new ObservableRangeCollection<TvShow>();

            LoadTopRatedTvShowsCommand =
                new DelegateCommand(async () => 
                                    await ExecuteLoadTopRatedTvShowsCommand().ConfigureAwait(false));
            ShowSearchTvShowCommand =
                new DelegateCommand(async () => 
                                    await ExecuteShowSearchTvShowsCommand().ConfigureAwait(false));
            ShowTvShowDetailCommand =
                new DelegateCommand<TvShow>(async tvShow => 
                                            await ExecuteShowTvShowDetailCommand(tvShow).ConfigureAwait(false));
            ItemAppearingCommand =
                new DelegateCommand<TvShow>(async tvShow => 
                                            await ExecuteItemAppearingCommand(tvShow).ConfigureAwait(false));

            LoadTopRatedTvShowsCommand.Execute();
        }

        private async Task ExecuteLoadTopRatedTvShowsCommand()
        {
            IsConnected = CrossConnectivity.Current.IsConnected;

            if (IsBusy || !IsConnected)
                return;

            IsBusy = true;

            try
            {
                TvShows.Clear();
                currentPage = 1;
                await LoadTvShowsAsync(currentPage, Enums.TVCategory.TopRated).ConfigureAwait(false);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ExecuteShowSearchTvShowsCommand()
        {
            await navigationService.NavigateAsync(nameof(TvShowSearchPage)).ConfigureAwait(false);
        }

        private async Task ExecuteShowTvShowDetailCommand(TvShow tvShow)
        {
            var parameters = new NavigationParameters { { nameof(tvShow), tvShow } };
            await navigationService.NavigateAsync(nameof(TvShowDetailPage), parameters).ConfigureAwait(false);
        }

        private async Task ExecuteItemAppearingCommand(TvShow tvShow)
        {
            var viewCellIndex = TvShows.IndexOf(tvShow);
            if (TvShows.Count - 2 <= viewCellIndex)
            {
                await NextPageUpcomingTvShowsAsync().ConfigureAwait(false);
            }
        }

        private async Task LoadTvShowsAsync(int page, Enums.TVCategory tvCategory)
        {
            try
            {
                var continueOnCapturedContext = Device.RuntimePlatform == Device.Windows;
                var searchTvShow = await TheMovieDbService
                    .GetTvShowsByCategoryAsync(page, tvCategory).ConfigureAwait(continueOnCapturedContext);

                if (searchTvShow != null)
                {
                    var tvShows = new List<TvShow>();
                    totalPage = searchTvShow.TotalPages;
                    foreach (var tvShow in searchTvShow.TvShows)
                    {
                        tvShows.Add(tvShow);
                    }
                    TvShows.AddRange(tvShows);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                Dialogs.Toast(new ToastConfig("Oops ... There was an error.".Translate()));
            }
        }

        public async Task NextPageUpcomingTvShowsAsync()
        {
            currentPage++;
            if (currentPage <= totalPage)
            {
                await LoadTvShowsAsync(currentPage, Enums.TVCategory.TopRated).ConfigureAwait(false);
            }
        }
    }
}