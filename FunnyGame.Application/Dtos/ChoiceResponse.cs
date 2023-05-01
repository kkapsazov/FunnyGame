using FunnyGame.Data.Models;

namespace FunnyGame.Application.Dtos;

public class ChoiceResponse
{
    public int Id { get; set; }
    public string Name { get; set; }

    public static ChoiceResponse Map(Choice choice)
    {
        return new()
        {
            Id = choice.Id,
            Name = choice.Name
        };
    }
}
