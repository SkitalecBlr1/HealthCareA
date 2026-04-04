using PatientsSeed.Helpers;
using System.Net.Http.Json;

namespace PatientsSeed
{
    internal class Program
    {
        async static Task Main(string[] args)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5270") 
            };

            var patients = PatientGenerator.Generate(100);

            foreach (var patient in patients)
            {
                var response = await httpClient.PostAsJsonAsync("/api/patients", patient);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Ошибка: {response.StatusCode}");
                }
                else
                {
                    Console.WriteLine("Создан пациент");
                }
            }

            Console.WriteLine("Готово!");
        }
    }
}
