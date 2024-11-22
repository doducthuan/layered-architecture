namespace LayeredArchitecture.Application.DTO
{
    public class StudentDTO
    {
        public int id { get; set; }
        public string first_name { get; set; } = null!;
        public string last_name { get; set; } = null!;
        public DateTime? birth_day { get; set; }
        public string? address { get; set; }
    }
}
