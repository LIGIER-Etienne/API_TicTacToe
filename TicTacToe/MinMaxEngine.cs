using MAUI_TicTacToe.Enums;

namespace MAUI_TicTacToe;

public static class MinMaxEngine {
    public static short[] BestMove(Board board, Symbol botSymbol) {
        Symbol opponentSymbol = botSymbol == Symbol.Cross ? Symbol.Circle : Symbol.Cross;

        int bestScore = int.MinValue;
        short[] bestMove = null!;

        foreach(var move in board.GetEmptyCells()) {
            Board simulatedBoard = board.Clone();
            simulatedBoard.PlayMove(move[0], move[1], botSymbol);

            int score = MinMax(simulatedBoard, false, botSymbol, opponentSymbol);

            if(score > bestScore) {
                bestScore = score;
                bestMove = move;
            }
        }

        return bestMove;
    }

    private static int MinMax(Board board, bool isMaxTurn, Symbol aiSymbol, Symbol opponent) {
        GameState state = board.GetGameState();

        if(state == GameState.CrossWins)
            return aiSymbol == Symbol.Cross ? 10 : -10;

        if(state == GameState.CircleWins)
            return aiSymbol == Symbol.Circle ? 10 : -10;

        if(state == GameState.Draw)
            return 0;

        if(isMaxTurn) {
            int bestScore = int.MinValue;

            foreach(var move in board.GetEmptyCells()) {
                Board simulated = board.Clone();
                simulated.PlayMove(move[0], move[1], aiSymbol);

                int score = MinMax(simulated, false, aiSymbol, opponent);
                bestScore = Math.Max(bestScore, score);
            }

            return bestScore;
        } else
          {
            int bestScore = int.MaxValue;

            foreach(var move in board.GetEmptyCells()) {
                Board simulated = board.Clone();
                simulated.PlayMove(move[0], move[1], opponent);

                int score = MinMax(simulated, true, aiSymbol, opponent);
                bestScore = Math.Min(bestScore, score);
            }

            return bestScore;
        }
    }
}
