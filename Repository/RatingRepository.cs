using Entits;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Repository
{
    public class RatingRepository : IRatingRepository
    {

        AdoNetManageContext _AdoNetManageContext;
        private readonly ILogger<UserRepository> _logger;
        public RatingRepository(AdoNetManageContext manageDbContext, ILogger<UserRepository> logger)
        {
            this._AdoNetManageContext = manageDbContext;
            _logger = logger;
        }

        public async Task<Rating> AddRating(Rating rating)
        {

            await _AdoNetManageContext.Ratings.AddAsync(rating);
            await _AdoNetManageContext.SaveChangesAsync();
            return rating;

        }



    }
}
