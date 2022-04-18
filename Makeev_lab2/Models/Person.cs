namespace Makeev_lab2.Models
{
    public class Person
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool IsAdmin => Role == "admin";
    }
}
