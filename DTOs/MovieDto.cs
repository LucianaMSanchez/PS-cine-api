namespace cine_api.DTOs
{
    public class MovieDto
    {
        public required string Name { get; set; }
        public required string Country { get; set; }
        public required DirectorDto Director { get; set; }

    }

}