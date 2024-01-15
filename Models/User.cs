namespace WeSyncBackend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string Password { get; set; } = string.Empty;

        public List<Fisier> FisierList { get; set;} = new List<Fisier>();
    }
}
