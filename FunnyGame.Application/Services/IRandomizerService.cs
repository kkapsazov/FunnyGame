namespace FunnyGame.Application.Services;

public interface IRandomizerService
{
    public Task<int> GetRandomChoice();
}
