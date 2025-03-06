using cine_api.DTOs;

namespace cine_api.Services
{
    public static class DataLoader
    {
        private const string MoviesFilePath = "Data/movies.txt";
        private const string DirectorsFilePath = "Data/directors.txt";

        public static List<MovieDto> LoadMovies()
        {
            var movies = new List<MovieDto>();
            if (File.Exists(MoviesFilePath))
            {
                foreach (var line in File.ReadAllLines(MoviesFilePath))
                {
                    var data = line.Split(',');
                    if (data.Length == 2)
                    {
                        movies.Add(new MovieDto { Name = data[0], Country = data[1] });
                    }
                }
            }
            return movies;
        }

        public static List<DirectorDto> LoadDirectors()
        {
            var directors = new List<DirectorDto>();
            if (File.Exists(DirectorsFilePath))
            {
                foreach (var line in File.ReadAllLines(DirectorsFilePath))
                {
                    directors.Add(new DirectorDto { Name = line });
                }
            }
            return directors;
        }
    }
}
