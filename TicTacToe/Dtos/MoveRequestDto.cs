namespace TicTacToe.Dtos;

public class MoveRequestDto
{
    public required int Row { get; set; }
    public required int Column { get; set; }
}
