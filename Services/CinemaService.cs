using cine_api.DTOs;

namespace cine_api.Services
{
    public class CinemaService
    {
        private readonly List<MovieDto> _movies;
        private readonly List<DirectorDto> _directors;
        private readonly List<FunctionDto> _functions;
        private const string FunctionsFilePath = "Data/functions.txt";
        private int _nextFunctionId = 1;

        public CinemaService()
        {
            _movies = DataLoader.LoadMovies();
            _directors = DataLoader.LoadDirectors();
            _functions = LoadFunctions();
            if (_functions.Count() != 0)
                _nextFunctionId = _functions.Max(f => f.Id) + 1;

        }

        public bool AddFunction(string movieName, string directorName, DateTime date, TimeSpan time, decimal price)
        {
            var movie = _movies.FirstOrDefault(m => m.Name.Equals(movieName, StringComparison.OrdinalIgnoreCase));
            var director = _directors.FirstOrDefault(d => d.Name.Equals(directorName, StringComparison.OrdinalIgnoreCase));

            if (movie == null || director == null)
            {
                Console.WriteLine("Movie or director not found.");
                return false;
            }
            if (!_movies.Any(m => m.Name.Equals(movieName, StringComparison.OrdinalIgnoreCase)) ||
                !_directors.Any(d => d.Name.Equals(directorName, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Movie or director not found.");
                return false;
            }
            if (!string.Equals(movie.Director.Name, directorName, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("The entered director did not direct the selected movie.");
                return false;
            }

            int directorFunctionsCount = _functions.Count(f => f.DirectorName == directorName && f.Date == date);
            if (directorFunctionsCount >= 10)
            {
                Console.WriteLine("The director has reached the maximum number of functions allowed for the day.");
                return false;
            }

            int movieFunctionsCount = _functions.Count(f => f.MovieName == movieName);
            if (movie.Country != "Argentina" && movieFunctionsCount >= 8)
            {
                Console.WriteLine("The international movie has reached its function limit.");
                return false;
            }

            var newFunction = new FunctionDto
            {
                Id = _nextFunctionId++,
                MovieName = movieName,
                DirectorName = directorName,
                Date = date,
                Time = time,
                Price = price
            };
            _functions.Add(newFunction);
            SaveFunctions();
            return true;
        }

        public bool UpdateFunction(FunctionDto function)
        {
            var existingFunction = _functions.FirstOrDefault(f => f.Id == function.Id);
            if (existingFunction == null)
            {
                return false;
            }

            var movie = _movies.FirstOrDefault(m => m.Name.Equals(function.MovieName, StringComparison.OrdinalIgnoreCase));
            var director = _directors.FirstOrDefault(d => d.Name.Equals(function.DirectorName, StringComparison.OrdinalIgnoreCase));

            if (movie == null || director == null)
            {
                Console.WriteLine("Movie or director not found.");
                return false;
            }
            if (!_movies.Any(m => m.Name.Equals(function.MovieName, StringComparison.OrdinalIgnoreCase)) ||
                !_directors.Any(d => d.Name.Equals(function.DirectorName, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Movie or director not found.");
                return false;
            }
            if (!string.Equals(movie.Director.Name, function.DirectorName, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("The entered director did not direct the selected movie.");
                return false;
            }

            existingFunction.MovieName = function.MovieName;
            existingFunction.DirectorName = function.DirectorName;
            existingFunction.Date = function.Date;
            existingFunction.Time = function.Time;
            existingFunction.Price = function.Price;

            SaveFunctions();
            return true;
        }

        public bool DeleteFunction(int id)
        {
            var function = _functions.FirstOrDefault(f => f.Id == id);
            if (function == null)
            {
                return false;
            }
            _functions.Remove(function);
            SaveFunctions();
            return true;
        }

        public List<FunctionDto> GetFunctions()
        {
            return _functions;
        }

        public FunctionDto? GetFunctionById(int id)
        {
            return _functions.FirstOrDefault(f => f.Id == id);
        }


        private void SaveFunctions()
        {
            using (StreamWriter writer = new StreamWriter(FunctionsFilePath))
            {
                foreach (var function in _functions)
                {
                    writer.WriteLine($"{function.Id},{function.MovieName},{function.DirectorName},{function.Date:yyyy-MM-dd},{function.Time},{function.Price}");
                }
            }
        }

        private List<FunctionDto> LoadFunctions()
        {
            var functions = new List<FunctionDto>();
            if (File.Exists(FunctionsFilePath))
            {
                foreach (var line in File.ReadAllLines(FunctionsFilePath))
                {
                    var data = line.Split(',');
                    if (data.Length == 6 &&
                        int.TryParse(data[0], out int id) &&
                        DateTime.TryParse(data[3], out DateTime date) &&
                        TimeSpan.TryParse(data[4], out TimeSpan time) &&
                        decimal.TryParse(data[5], out decimal price))
                    {
                        functions.Add(new FunctionDto
                        {
                            Id = id,
                            MovieName = data[1],
                            DirectorName = data[2],
                            Date = date,
                            Time = time,
                            Price = price
                        });
                    }
                }
            }
            return functions;
        }
    }
}
