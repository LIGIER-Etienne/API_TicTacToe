using MAUI_TicTacToe;
using MAUI_TicTacToe.Enums;
using MAUI_TicTacToe.Players;
using MAUI_TicTacToe.InputProviders;
using TicTacToe.Dtos;

namespace TicTacToe.Services;

public class GameService
{
    private Board _board;
    private Player _humanPlayer;
    private Player _botPlayer;
    private Player _currentPlayer;
    private GameState _gameState;

    public GameService()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        _board = new Board();
        // HumanPlayer needs an IInputProvider, but for API usage we'll use a dummy provider
        var dummyProvider = new DummyInputProvider();
        _humanPlayer = new HumanPlayer(Symbol.Cross, dummyProvider);
        _botPlayer = new BotPlayer(_board, Symbol.Circle);
        _currentPlayer = _humanPlayer;
        _gameState = GameState.InProgress;
    }

    public void Reset()
    {
        InitializeGame();
    }

    public GameStateDto GetCurrentState()
    {
        return new GameStateDto
        {
            Board = ConvertBoardToArray(),
            CurrentGameState = _gameState.ToString(),
            CurrentPlayer = _currentPlayer.Symbol.ToString()
        };
    }

    public MoveResponseDto MakeHumanMove(int row, int column)
    {
        if (_gameState != GameState.InProgress)
        {
            return new MoveResponseDto
            {
                Success = false,
                Error = $"Le jeu est terminé. État: {_gameState}",
                GameState = GetCurrentState()
            };
        }

        if (_currentPlayer != _humanPlayer)
        {
            return new MoveResponseDto
            {
                Success = false,
                Error = "Ce n'est pas le tour du joueur humain",
                GameState = GetCurrentState()
            };
        }

        if (row < 0 || row >= 3 || column < 0 || column >= 3)
        {
            return new MoveResponseDto
            {
                Success = false,
                Error = "Coordonnées invalides (doivent être entre 0 et 2)",
                GameState = GetCurrentState()
            };
        }

        if (_board.Cells[row, column] != Symbol.Empty)
        {
            return new MoveResponseDto
            {
                Success = false,
                Error = "Cette case est déjà occupée",
                GameState = GetCurrentState()
            };
        }

        // Appliquer le coup du joueur
        _board.Cells[row, column] = _humanPlayer.Symbol;
        _gameState = _board.GetGameState();

        // Si le jeu n'est pas terminé, c'est au tour du bot
        if (_gameState == GameState.InProgress)
        {
            ExecuteBotMove();
        }

        return new MoveResponseDto
        {
            Success = true,
            GameState = GetCurrentState()
        };
    }

    private void ExecuteBotMove()
    {
        if (_gameState != GameState.InProgress)
            return;

        _currentPlayer = _botPlayer;
        var botMove = _botPlayer.GetNextMoveAsync().Result;
        _board.Cells[botMove[0], botMove[1]] = _botPlayer.Symbol;
        _gameState = _board.GetGameState();

        if (_gameState == GameState.InProgress)
        {
            _currentPlayer = _humanPlayer;
        }
    }

    private string[][] ConvertBoardToArray()
    {
        var result = new string[3][];
        for (int i = 0; i < 3; i++)
        {
            result[i] = new string[3];
            for (int j = 0; j < 3; j++)
            {
                result[i][j] = _board.Cells[i, j].ToString();
            }
        }
        return result;
    }
}
