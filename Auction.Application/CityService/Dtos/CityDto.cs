using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Auction.Application.CityService.Dtos
{
    public class CityDto
    {
        public int Id { get; set; }

        [DisplayName("Şehir")]
        public string CityName { get; set; }
    }
}
