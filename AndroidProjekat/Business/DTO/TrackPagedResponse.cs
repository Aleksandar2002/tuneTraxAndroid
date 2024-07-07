using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidProjekat.Business.DTO
{
    public class TrackPagedResponse: PagedResponse
    {
        public List<TrackDto> Data { get; set; }
    }
}
