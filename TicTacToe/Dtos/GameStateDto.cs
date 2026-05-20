namespace TicTacToe.Dtos;

public class GameStateDto
{
    public required string[][] Board { get; set; }
    public required string CurrentGameState { get; set; }
    public required string CurrentPlayer { get; set; }
}
