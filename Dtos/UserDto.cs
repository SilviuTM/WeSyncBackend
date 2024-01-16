using WeSyncBackend.Models;

namespace WeSyncBackend.Dtos
{
    public class UserDto 
    {
        public string Email { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string? LastName { get; set; }
        public string Password { get; set; } = "";  

        public UserDto()
        {
            
        }

        public UserDto(User x)
        {
            Email = x.Email;
            FirstName = x.FirstName;
            LastName = x.LastName;
            Password = x.Password;
        }
    }
}
