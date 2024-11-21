namespace LayeredArchitecture.Application.Form
{
    public class AccountForm
    {
        public int id { get; set; }
        public string user_name { get; set; } = null!;
        public string password { get; set; } = null!;
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
