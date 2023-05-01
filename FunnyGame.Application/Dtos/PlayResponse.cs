using FunnyGame.Application.Enums;

namespace FunnyGame.Application.Dtos;

public class PlayResponse
{
    public GameResult Result { get; set; }
    public int Player { get; set; }
    public int Bot { get; set; }
}
