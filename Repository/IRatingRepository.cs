using Entits;

namespace Repository
{
    public interface IRatingRepository
    {
        Task<Rating> AddRating(Rating rating);
    }

}