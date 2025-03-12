using cine_api.Services;

class Program
{
    static void Main()
    {
        CinemaService cinemaService = new CinemaService();
        cinemaService.ShowMenu();
    }
}
