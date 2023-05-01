using System.Text.Json;
using FunnyGame.Application.Dtos;
using FunnyGame.Application.Exceptions;
using FunnyGame.Data;
using Microsoft.Extensions.Configuration;

namespace FunnyGame.Application.Services;

public class RandomizerService : IRandomizerService
{
    private readonly IConfiguration _configuration;

    public RandomizerService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<int> GetRandomChoice()
    {
        HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(_configuration["RandomizerUrl"]);

        if (!response.IsSuccessStatusCode)
        {
            throw new AppException("Problem communication with external service.");
        }

        string stringResponse = await response.Content.ReadAsStringAsync();
        RandomizerResponse result = JsonSerializer.Deserialize<RandomizerResponse>(stringResponse);

        return result.Random;
    }
}
