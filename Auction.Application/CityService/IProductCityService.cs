using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Auction.Application.CityService.Dtos;

namespace Auction.Application.CityService
{
   public interface ICityService
    {
        Task<ApplicationResult<CityDto>> Get(int id);
        Task<ApplicationResult<List<CityDto>>> GetAll();
    }
}
