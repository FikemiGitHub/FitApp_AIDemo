using Microsoft.SemanticKernel;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddKernel()
    .AddOpenAIChatCompletion("gpt-4", builder.Configuration["AI:OpenAI:ApiKey"]);

var app = builder.Build();

app.MapGet("/chat", async (string question, Kernel kernel) =>
{
    Console.WriteLine("Reach AI");
    Console.WriteLine(kernel.InvokePromptStreamingAsync<string>(question));
    return kernel.InvokePromptStreamingAsync<string>(question);
});

app.Run();
