using cine_api.Services;

namespace cine_api.Endpoints
{
    public class FunctionsEndpoints
    {
        private readonly CinemaService _cinemaService;

        public FunctionsEndpoints()
        {
            _cinemaService = new CinemaService();
        }

        public void ShowMenu()
        {
            while (true)
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
                    case "1":
                        CreateFunction();
                        break;
                    case "2":
                        ModifyFunction();
                        break;
                    case "3":
                        DeleteFunction();
                        break;
                    case "4":
                        ListFunctions();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        private void CreateFunction()
        {
            Console.Write("Enter the movie name: ");
            string? movieName = Console.ReadLine();
            Console.Write("Enter the director's name: ");
            string? directorName = Console.ReadLine();
            Console.Write("Enter the date (YYYY-MM-DD): ");
            DateTime date;
            while (!DateTime.TryParse(Console.ReadLine(), out date))
            {
                Console.Write("Invalid date. Try again (YYYY-MM-DD): ");
            }
            Console.Write("Enter the time (HH:MM): ");
            TimeSpan time;
            while (!TimeSpan.TryParse(Console.ReadLine(), out time))
            {
                Console.Write("Invalid time. Try again (HH:MM): ");
            }
            Console.Write("Enter the price: ");
            decimal price;
            while (!decimal.TryParse(Console.ReadLine(), out price))
            {
                Console.Write("Invalid price. Try again: ");
            }

            var result = _cinemaService.AddFunction(movieName!, directorName!, date, time, price);
            Console.WriteLine(result ? "Function added successfully." : "Could not add function.");
        }

        private void ModifyFunction()
        {
            Console.Write("Enter the function ID to modify: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Invalid ID. Try again: ");
            }
            Console.Write("Enter the new price: ");
            decimal price;
            while (!decimal.TryParse(Console.ReadLine(), out price))
            {
                Console.Write("Invalid price. Try again: ");
            }

            var result = _cinemaService.UpdateFunction(id, price);
            Console.WriteLine(result ? "Function modified successfully." : "Function not found.");
        }

        private void DeleteFunction()
        {
            Console.Write("Enter the function ID to delete: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Invalid ID. Try again: ");
            }

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
                Console.WriteLine($"ID: {function.Id}, Movie: {function.MovieName}, Director: {function.DirectorName}, Date: {function.Date.ToShortDateString()}, Time: {function.Time}, Price: ${function.Price}");
            }
        }
    }
}
