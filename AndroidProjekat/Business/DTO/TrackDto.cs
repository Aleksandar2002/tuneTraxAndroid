using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AndroidProjekat.Business.DTO
{
    public class TrackDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string CoverImage{ get; set; }
        public string AudioFile{ get; set; }
        public int Duration{ get; set; }
        public AlbumDto? Album{ get; set; }
        public ICollection<ArtistDto> Artists { get; set; }
        public ICollection<GenreDto> Genres { get; set; }
        public ICommand? RemoveTrackCommand { get; set; }
    }

    public abstract class NamedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class AlbumDto: NamedEntity
    {
        public DateTime ReleaseDate { get; set; }
    }

    public class ArtistDto: NamedEntity
    {
    }

    public class GenreDto : NamedEntity { 
    }
   

}
