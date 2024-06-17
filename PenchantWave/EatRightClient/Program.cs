using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        using HttpClient client = new();
        client.BaseAddress = new Uri("http://localhost:7022");

        while (true)
        {
            try
            {
                Console.Write("Enter your BMI: ");
                var bmi = Console.ReadLine();

                Console.Write("Enter your country: ");
                var country = Console.ReadLine();

                var question = $"What food suggestions can you provide for someone with a BMI of {bmi} in {country}?";
                var encodedQuestion = Uri.EscapeDataString(question);

                await foreach (var msg in client.GetFromJsonAsAsyncEnumerable<string>($"/chat?question={encodedQuestion}"))
                {
                    Console.Write(msg);
                }

                Console.WriteLine();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }
    }
}
