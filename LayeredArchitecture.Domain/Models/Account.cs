namespace LayeredArchitecture.Domain.Models
{
    public class Account : BaseCommonModel
    {
        public string user_name { get; set; } = null!;
        public string password { get; set; } = null!;       
    }
}
