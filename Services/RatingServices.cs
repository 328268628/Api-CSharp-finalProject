using Entits;
using Microsoft.Extensions.Logging;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RatingServices : IRatingServices
    {
      
        IRatingRepository ratingRepository;

        public RatingServices(AdoNetManageContext manageDbContext, ILogger<UserRepository> logger, IRatingRepository _ratingRepository)
        {
            ratingRepository = _ratingRepository;
      
        }

        public async Task<Rating> AddRating(Rating rating)
        {
            return await ratingRepository.AddRating(rating);
        }

    }
}
