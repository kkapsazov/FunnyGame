using FunnyGame.Application.Dtos;
using FunnyGame.Application.Services;
using FunnyGame.Data;
using Microsoft.AspNetCore.Mvc;

namespace FunnyGame.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class GameController : ControllerBase
{
    private readonly GameService _gameService;

    public GameController(GameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet]
    public async Task<List<ChoiceResponse>> Choices()
    {
        return await _gameService.GetChoices();
    }

    [HttpGet]
    public async Task<ChoiceResponse> Choice()
    {
        return await _gameService.GetRandomChoice();
    }

    [HttpPost]
    public async Task<PlayResponse> Play(PlayRequest request)
    {
        return await _gameService.Play(request.Player);
    }
}
