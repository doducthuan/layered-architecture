namespace LayeredArchitecture.Application.Form
{
    public class StudentForm
    {
        public int id { get; set; }
        public string first_name { get; set; } = null!;
        public string last_name { get; set; } = null!;
        public DateTime? birth_day { get; set; }
        public string? address { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
