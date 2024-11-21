namespace LayeredArchitecture.Domain.Models
{
    public class Account
    {
        public int id { get; set; }
        public string user_name { get; set; } = null!;
        public string password { get; set; } = null!;
        public DateTime created_at { get; set; }
        public bool active { get; set; }
    }
}
