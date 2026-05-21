using TicTacToe.Enums;

namespace MAUI_TicTacToe.Players;

public class BotPlayer(Board Board, Symbol symbol) :Player(symbol) {
    private readonly Board board = Board;
    private static async Task Think() => await Task.Delay(1000);

    public override async Task<short[]> GetNextMoveAsync() {
        Console.WriteLine("Bot is playing");
        await Think();
        return MinMaxEngine.BestMove(board, Symbol);
    }
}