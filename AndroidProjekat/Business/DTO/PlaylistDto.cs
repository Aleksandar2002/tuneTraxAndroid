using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidProjekat.Business.DTO
{
    public class PlaylistDto: NamedEntity
    {
        public AccessLevel AccessLevel { get; set; }
        public List<int> Tracks { get; set; }
    }

    public enum AccessLevel
    {
        Public,
        Private
    }

    public class PlaylistPagedResponseDto: PagedResponse
    {
        public List<PlaylistDto> Data { get; set; }
    }
}
