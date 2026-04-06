using Microsoft.Extensions.Configuration;
using PatientsSeed.Helpers;
using System.Net.Http.Json;

namespace PatientsSeed
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var baseUrl = Environment.GetEnvironmentVariable("ApiSettings__BaseUrl")
             ?? "http://localhost:5000";

            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                Console.WriteLine("BaseUrl не задан");
                return;
            }

            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };

            Console.WriteLine($"Ждём API по адресу: {baseUrl}");

            
            var isApiReady = false;

            for (int i = 0; i < 20; i++)
            {
                try
                {
                    var response = await httpClient.GetAsync("/health");

                    if (response.IsSuccessStatusCode)
                    {
                        isApiReady = true;
                        Console.WriteLine("API готов!");
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при проверке API: {ex.Message}");
                }

                Console.WriteLine("Ожидание API...");
                await Task.Delay(2000);
            }

            if (!isApiReady)
            {
                Console.WriteLine("API не запустился. Завершаем работу.");
                return;
            }
                        
            var patients = PatientGenerator.Generate(100);

            Console.WriteLine($"Создание {patients.Count} пациентов...");

            foreach (var patient in patients)
            {
                try
                {
                    var response = await httpClient.PostAsJsonAsync("/api/patients", patient);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Создан пациент");
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Ошибка: {response.StatusCode} | {error}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при отправке: {ex.Message}");
                }
            }

            Console.WriteLine("Готово!");
        }
    }
}