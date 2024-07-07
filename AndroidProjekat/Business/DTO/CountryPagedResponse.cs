using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidProjekat.Business.DTO
{
    class CountryPagedResponse: PagedResponse
    {
        public  IEnumerable<SelectDto> Data { get; set; }
    }
}
