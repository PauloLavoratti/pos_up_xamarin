using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PosUpXamarin.Core.DataServices.Interfaces;
using PosUpXamarin.Core.Models;
using PosUpXamarin.Core.DataServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(TheMovieDbService))]
namespace PosUpXamarin.Core.DataServices
{
    public class TheMovieDbService : ITheMovieDbService
    {
        private const string apiKey = "ad332258020257fb88e2cc468225dcb0";
        private const string baseUrl = "https://api.themoviedb.org/3";
        private const string tvShowPath = "/tv";
        private const string searchTvShowPath = "/search/tv";
        private const string genreListPath = "/genre/list";

        private readonly string language;
        private readonly HttpClient httpClient;

        public TheMovieDbService()
        {
            language = CultureInfo.CurrentCulture.Name;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        ~TheMovieDbService()
        {
            httpClient.Dispose();
        }

        public async Task<TvShowSearch> SearchTvShowsAsync(string searchTerm, int page)
        {
            var restUrl = $"{baseUrl}{searchTvShowPath}?api_key={apiKey}&query={searchTerm}&page={page}&language={language}";

            try
            {
                using (var response = await httpClient.GetAsync(restUrl).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        {
                            return JsonConvert.DeserializeObject<TvShowSearch>(
                                await new StreamReader(responseStream).ReadToEndAsync().ConfigureAwait(false));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ReportError(ex);
            }

            return null;
        }

        public async Task<TvShowSearch> GetTvShowsByCategoryAsync(int page, Enums.TVCategory category)
        {
            var restUrl = $"{baseUrl}{Enums.PathCategoryTvShow(category)}?api_key={apiKey}&page={page}&language={language}";
            try
            {
                using (var response = await httpClient.GetAsync(restUrl).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        {
                            return JsonConvert.DeserializeObject<TvShowSearch>(
                                await new StreamReader(responseStream).ReadToEndAsync().ConfigureAwait(false));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ReportError(ex);
            }

            return null;
        }

        public async Task<TvShowDetail> GetTvShowDetailAsync(int id)
        {
            var restUrl = $"{baseUrl}{tvShowPath}/{id}?api_key={apiKey}&language={language}";
            try
            {
                using (var response = await httpClient.GetAsync(restUrl).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        {
                            return JsonConvert.DeserializeObject<TvShowDetail>(
                                await new StreamReader(responseStream).ReadToEndAsync().ConfigureAwait(false));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ReportError(ex);
            }

            return null;
        }

        private void ReportError(Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}