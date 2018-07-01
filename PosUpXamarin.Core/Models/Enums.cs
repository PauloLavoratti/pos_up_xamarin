namespace PosUpXamarin.Core.Models
{
    public class Enums
    {
        public enum TVCategory
        {
            Popular,
            TopRated
        }

        public static string PathCategoryTvShow(TVCategory category)
        {
            switch (category)
            {
                case TVCategory.Popular:
                    return "/tv/popular";

                case TVCategory.TopRated:
                    return "/tv/top_rated";

                default:
                    return string.Empty;
            }
        }

        public static string NameCategoryTvShow(TVCategory category)
        {
            switch (category)
            {
                case TVCategory.Popular:
                    return "Popular";

                case TVCategory.TopRated:
                    return "Top Rated";

                default:
                    return string.Empty;
            }
        }
    }
}
