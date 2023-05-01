using FunnyGame.Application.Dtos;
using FunnyGame.Application.Enums;
using FunnyGame.Application.Exceptions;
using FunnyGame.Data;
using FunnyGame.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FunnyGame.Application.Services;

public class GameService
{
    private readonly GameDbContext _dbContext;
    private readonly IRandomizerService _randomizerService;

    public GameService(GameDbContext dbContext, IRandomizerService randomizerService)
    {
        _dbContext = dbContext;
        _randomizerService = randomizerService;
    }

    public async Task<List<ChoiceResponse>> GetChoices()
    {
        return await _dbContext.Choices.Select(x => ChoiceResponse.Map(x)).ToListAsync();
    }

    public async Task<ChoiceResponse> GetRandomChoice()
    {
        int selectedId = await _randomizerService.GetRandomChoice();

        Choice choice = await _dbContext.Choices.FirstOrDefaultAsync(x => x.Id == selectedId);
        if (choice == null)
        {
            throw new AppException("Choice not found");
        }

        return ChoiceResponse.Map(choice);
    }

    public async Task<PlayResponse> Play(int playerChoiceId)
    {
        int botChoiceId = await _randomizerService.GetRandomChoice();

        GameResult result = GameResult.Tie;
        if (playerChoiceId != botChoiceId)
        {
            bool ruleFound = await _dbContext.GameRules.AnyAsync(x => x.ChoiceId == playerChoiceId && x.WinAgainstId == botChoiceId);

            if (ruleFound)
            {
                result = GameResult.Win;
            }
            else
            {
                result = GameResult.Lose;
            }
        }

        return new()
        {
            Result = result,
            Player = playerChoiceId,
            Bot = botChoiceId
        };
    }
}
