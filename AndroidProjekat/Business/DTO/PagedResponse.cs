using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidProjekat.Business.DTO
{
    public class PagedResponse
    {
        //public IEnumerable<object> Data { get; set; }
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public int TotalAmount { get; set; }
    }
}
