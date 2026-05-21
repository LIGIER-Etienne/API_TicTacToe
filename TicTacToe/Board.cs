using MAUI_TicTacToe.Enums;
using TicTacToe.Enums;

namespace MAUI_TicTacToe;

public class Board {
    public Symbol[,] Cells { get; private set; }
    private const short BOARD_SIZE = 3; // Win conditions are only designed for 3*3 Cells

    public Board() {
        Cells = new Symbol[BOARD_SIZE, BOARD_SIZE];
        InitBoard();
    }

    public void DisplayBoard() {
        Console.WriteLine();

        for(int i = 0; i < BOARD_SIZE; i++) {
            for(int j = 0; j < BOARD_SIZE; j++) {
                Console.Write((char) Cells[i, j]);
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    public void InitBoard() {
        for(int i = 0; i < BOARD_SIZE; i++) {
            for(int j = 0; j < BOARD_SIZE; j++) {
                Cells[i, j] = Symbol.Empty;
            }
        }
    }

    private static bool LineMatch(Symbol a, Symbol b, Symbol c) {
        return !a.Equals(Symbol.Empty) && a == b && b == c;
    }

    public GameState GetGameState() {
        // Rows
        for(int i = 0; i < 3; i++) {
            if(LineMatch(Cells[i, 0], Cells[i, 1], Cells[i, 2]))
                return Cells[i, 0].Equals(Symbol.Circle) ? GameState.CircleWins : GameState.CrossWins;
        }

        // Columns
        for(int j = 0; j < 3; j++) {
            if(LineMatch(Cells[0, j], Cells[1, j], Cells[2, j]))
                return Cells[0, j].Equals(Symbol.Circle) ? GameState.CircleWins : GameState.CrossWins;
        }

        // Diagonals
        if(LineMatch(Cells[0, 0], Cells[1, 1], Cells[2, 2]))
            return Cells[0, 0].Equals(Symbol.Circle) ? GameState.CircleWins : GameState.CrossWins;

        if(LineMatch(Cells[0, 2], Cells[1, 1], Cells[2, 0]))
            return Cells[0, 2].Equals(Symbol.Circle) ? GameState.CircleWins : GameState.CrossWins;

        // Draw check
        if(GetEmptyCells().Count == 0) return GameState.Draw;

        return GameState.InProgress;
    }

    public bool PlayMove(short row, short col, Symbol symbol) {
        if(!IsMoveValid(row, col)) return false;

        Cells[row, col] = symbol;
        return true;
    }

    private bool IsMoveValid(short row, short col) {
        if(row < 0 || row > BOARD_SIZE - 1) {
            Console.Error.WriteLine("Row must be >= 0 and < " + BOARD_SIZE);
            return false;
        }

        if(col < 0 || col > BOARD_SIZE - 1) {
            Console.Error.WriteLine("Col must be >= 0 and < " + BOARD_SIZE);
            return false;
        }

        if(!Cells[row, col].Equals(Symbol.Empty)) {
            Console.Error.WriteLine("Slot [" + row + ", " + col + "] is already taken");
            return false;
        }

        return true;
    }

    public List<short[]> GetEmptyCells() {
        List<short[]> EmptyCells = [];

        for(short i = 0; i < BOARD_SIZE; i++) {
            for(short j = 0; j < BOARD_SIZE; j++) {
                if(Cells[i, j] == Symbol.Empty) EmptyCells.Add([i, j]);
            }
        }

        return EmptyCells;
    }

    public Board Clone() {
        Board copy = new();
        for(int i = 0; i < 3; i++)
            for(int j = 0; j < 3; j++)
                copy.Cells[i, j] = this.Cells[i, j];
        return copy;
    }
}
