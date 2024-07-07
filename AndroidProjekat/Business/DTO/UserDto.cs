using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidProjekat.Business.DTO
{
    public class UserDto : TokenDto
    {
        public string Username { get; set; }
        public int Id { get; set; }
    }
    public class RegisterUserDto {

        public string Username { get; set; }
        public string Password{ get; set; }
        public string Email { get; set; }
        public int CountryId{ get; set; }
        public int Gender{ get; set; }
        public DateTime DateOfBirth{ get; set; }
    }
}
