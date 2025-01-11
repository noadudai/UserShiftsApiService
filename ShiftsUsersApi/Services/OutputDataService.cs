using System;

namespace ShiftsUsersApi.Services
{
    public class OutputDataService : IOutputDataService
    {
        public void OutputData(string message)
        {
            Console.WriteLine($"Data recived: {message}");
        }
    }
}
