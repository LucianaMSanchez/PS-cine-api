namespace cine_api.DTOs
{
    public class FunctionDto
    {
        public int Id { get; set; }
        public required string MovieName { get; set; }
        public required string DirectorName { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public decimal Price { get; set; }
    }
}