using cine_api.Services;

class Program
{
    static void Main()
    {
        FunctionsService functionsService = new FunctionsService();
        functionsService.ShowMenu();
    }
}
