using FunnyGame.Data;
using Microsoft.EntityFrameworkCore;

namespace FunnyGame.Application.Services;

public class MockRandomizerService : IRandomizerService
{
    private readonly GameDbContext _dbContext;

    public MockRandomizerService(GameDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> GetRandomChoice()
    {
        Random random = new();
        List<int> ids = await _dbContext.Choices.Select(x => x.Id).ToListAsync();
        return await Task.FromResult(ids[random.Next(ids.Count)]);
    }
}
