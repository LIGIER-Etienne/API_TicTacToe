using MAUI_TicTacToe.Enums;
using TicTacToe.Enums;

namespace TicTacToe.Dtos;

public class GameStateDto
{
    public required Symbol[][] Board { get; set; }
    public required GameState CurrentGameState { get; set; }
    public required Symbol CurrentPlayer { get; set; }
}
