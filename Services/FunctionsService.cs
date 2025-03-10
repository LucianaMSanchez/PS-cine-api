namespace cine_api.Services
{
    public class FunctionsService
    {
        private readonly CinemaService _cinemaService;

        public FunctionsService()
        {
            _cinemaService = new CinemaService();
        }

        public void ShowMenu()
        {
            Console.WriteLine("\nCinema Functions Menu");
            Console.WriteLine("1. Create function");
            Console.WriteLine("2. Modify function");
            Console.WriteLine("3. Delete function");
            Console.WriteLine("4. List functions");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option: ");

            var option = Console.ReadLine();
            switch (option)
            {
                case "1": CreateFunction(); break;
                case "2": ModifyFunction(); break;
                case "3": DeleteFunction(); break;
                case "4": ListFunctions(); break;
                case "5": return;
                default: Console.WriteLine("Invalid option. Try again."); break;
            }

            if (option != "5") ShowMenu();
        }

        private void CreateFunction()
        {
            var movieName = Prompt("Enter the movie name: ");
            var directorName = Prompt("Enter the director's name: ");
            DateTime date = GetValidDate("Enter the date (YYYY-MM-DD): ");
            TimeSpan time = GetValidTime(date);
            decimal price = GetValidPrice();

            var result = _cinemaService.AddFunction(movieName!, directorName!, date, time, price);
            Console.WriteLine(result ? "Function added successfully." : "Could not add function.");
        }

        private string Prompt(string message)
        {
            Console.Write(message);
            var response = Console.ReadLine();
            return response!;
        }

        private DateTime GetValidDate(string prompt)
        {
            return ValidateInput(prompt,
                input => DateTime.Parse(input),
                date => date >= DateTime.Today,
                "Invalid date. Must be today or later.");
        }

        private TimeSpan GetValidTime(DateTime date)
        {
            int hour = -1;
            int minutes = -1;
            TimeSpan time;
            DateTime now = DateTime.Now;

            Console.Write("Enter the hour (0-23): ");
            if (!int.TryParse(Console.ReadLine(), out hour) || hour < 0 || hour > 23)
            {
                Console.Write("Invalid hour. Please enter a valid hour (0-23): ");
                return GetValidTime(date);
            }

            Console.Write("Enter the minutes (0-59): ");
            if (!int.TryParse(Console.ReadLine(), out minutes) || minutes < 0 || minutes > 59)
            {
                Console.Write("Invalid minutes. Please enter valid minutes (0-59): ");
                return GetValidTime(date);
            }

            time = new TimeSpan(hour, minutes, 0);

            if (date == DateTime.Today && time <= now.TimeOfDay)
            {
                Console.Write("Invalid time. Please enter a future time: ");
                return GetValidTime(date);
            }

            return time;
        }

        private decimal GetValidPrice()
        {
            return ValidateInput("Enter the price: ",
                input => decimal.Parse(input),
                price => price > 0,
                "Invalid price. Must be greater than 0.");
        }

        private int GetValidId()
        {
            return ValidateInput("Enter the function ID: ",
                input => int.Parse(input),
                id => id >= 0,
                "Invalid ID. Must be a non-negative number.");
        }

        private T ValidateInput<T>(string prompt, Func<string, T> converter, Func<T, bool> validator, string errorMessage)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            try
            {
                T value = converter(input!);
                if (validator(value)) return value;
            }
            catch { }

            Console.WriteLine(errorMessage);
            return ValidateInput(prompt, converter, validator, errorMessage); // Recursión en caso de error
        }

        private void ModifyFunction()
        {
            int id = GetValidId();
            var function = _cinemaService.GetFunctionById(id);
            if (function == null)
            {
                Console.WriteLine("Function not found.");
                return;
            }

            Console.WriteLine($"Current details: Movie: {function.MovieName}, Director: {function.DirectorName}, Date: {function.Date:yyyy-MM-dd}, Time: {function.Time}, Price: {function.Price}");
            function.MovieName = Prompt($"Enter the new movie name (current: {function.MovieName}): ");
            function.DirectorName = Prompt($"Enter the new director name (current: {function.DirectorName}): ");
            function.Date = GetValidDateUpdate(function.Date);
            function.Time = GetValidTimeUpdate(function.Date, function.Time);
            function.Price = GetValidPriceUpdate(function.Price);

            var result = _cinemaService.UpdateFunction(function);
            Console.WriteLine(result ? "Function modified successfully." : "Failed to modify function.");
        }

        private DateTime GetValidDateUpdate(DateTime currentDate)
        {
            var newDateInput = Prompt($"Enter the new date (current: {currentDate:yyyy-MM-dd}): ");
            if (string.IsNullOrEmpty(newDateInput)) return currentDate;
            return GetValidDate("Enter the new date (YYYY-MM-DD): ");
        }

        private TimeSpan GetValidTimeUpdate(DateTime date, TimeSpan currentTime)
        {
            var timeInput = Prompt($"Enter the new time (current: {currentTime}): ");
            return string.IsNullOrEmpty(timeInput) ? currentTime : GetValidTime(date);
        }

        private decimal GetValidPriceUpdate(decimal currentPrice)
        {
            var priceInput = Prompt($"Enter the new price (current: {currentPrice}): ");
            return string.IsNullOrEmpty(priceInput) ? currentPrice : decimal.Parse(priceInput);
        }

        private void DeleteFunction()
        {
            var functions = _cinemaService.GetFunctions();
            if (functions == null || functions.Count == 0)
            {
                Console.WriteLine("No functions available to delete.");
                return;
            }

            foreach (var function in functions)
            {
                Console.WriteLine($"ID: {function.Id}, Movie: {function.MovieName}, Director: {function.DirectorName}, Date: {function.Date:yyyy-MM-dd}, Time: {function.Time}, Price: {function.Price}");
            }

            int id = GetValidId();
            var result = _cinemaService.DeleteFunction(id);
            Console.WriteLine(result ? "Function deleted." : "Function not found.");
        }

        private void ListFunctions()
        {
            var functions = _cinemaService.GetFunctions();
            if (functions.Count == 0)
            {
                Console.WriteLine("No registered functions.");
                return;
            }

            foreach (var function in functions)
            {
                Console.WriteLine($"ID: {function.Id}, Movie: {function.MovieName}, Director: {function.DirectorName}, Date: {function.Date:yyyy-MM-dd}, Time: {function.Time}, Price: {function.Price}");
            }
        }
    }
}
