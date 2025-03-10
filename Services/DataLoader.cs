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
            if (File.Exists(MoviesFilePath) && File.Exists(DirectorsFilePath))
            {
                var directors = File.ReadAllLines(DirectorsFilePath).ToList();
                var movieLines = File.ReadAllLines(MoviesFilePath);

                if (movieLines.Length == directors.Count)
                {
                    for (int i = 0; i < movieLines.Length; i++)
                    {
                        var data = movieLines[i].Split(',');
                        if (data.Length == 2)
                        {
                            movies.Add(new MovieDto
                            {
                                Name = data[0],
                                Country = data[1],
                                Director = new DirectorDto { Name = directors[i] }
                            });
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Movies and directors count mismatch!");
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
