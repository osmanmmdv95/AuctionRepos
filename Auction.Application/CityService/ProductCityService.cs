using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auction.Application.CityService.Dtos;
using Auction.Domain.Identity;
using Auction.Domain.Product;
using Auction.EntityFramework.Context;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auction.Application.CityService
{

    public class CityService : ICityService
    {
        private readonly ApplicationUserDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public CityService(ApplicationUserDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<ApplicationResult<CityDto>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationResult<List<CityDto>>> GetAll()
        {
            try
            {
                List<CityDto> cityList = await _context.Cities.Select(city => new CityDto
                {
                    Id = city.Id,
                    CityName = city.CityName

                }).ToListAsync();
                return new ApplicationResult<List<CityDto>>
                {
                    Succeeded = true,
                    Result = cityList
                };

            }
            catch (Exception e)
            {
                return new ApplicationResult<List<CityDto>>
                {
                    ErrorMessage = e.Message,
                    Result = new List<CityDto>(),
                    Succeeded = false
                };
            }
        }
    }
}
