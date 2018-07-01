using System.Collections.Generic;
using System.Threading.Tasks;
using PosUpXamarin.Core.Models;

namespace PosUpXamarin.Core.DataServices.Interfaces
{
    public interface ITheMovieDbService
    {
        Task<TvShowSearch> SearchTvShowsAsync(string searchTerm, int page);

        Task<TvShowSearch> GetTvShowsByCategoryAsync(int page, Enums.TVCategory sortBy);

        Task<TvShowDetail> GetTvShowDetailAsync(int id);
    }
}