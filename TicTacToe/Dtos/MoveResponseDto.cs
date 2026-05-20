namespace TicTacToe.Dtos;

public class MoveResponseDto
{
    public required bool Success { get; set; }
    public string? Error { get; set; }
    public required GameStateDto GameState { get; set; }
}
