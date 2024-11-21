namespace LayeredArchitecture.Common.ApiResponse
{
    public class DataError
    {
        public int Status { get; set; }
        public string Code { get; set; }
        public string? field { get; set; } = null;
        public string? value { get; set; } = null;
        public object? Data { get; set; } = null;
    }
}
