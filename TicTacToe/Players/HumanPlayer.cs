using MAUI_TicTacToe.Enums;
using MAUI_TicTacToe.InputProviders;

namespace MAUI_TicTacToe.Players {
    public class HumanPlayer :Player {

        private readonly IInputProvider inputProvider;

        public HumanPlayer(Symbol symbol, IInputProvider inputProvider) : base(symbol) {
            this.inputProvider = inputProvider;
        }

        private short ReadShort(string message) {
            while(true) {
                inputProvider.WriteLine(message);
                if(short.TryParse(inputProvider.ReadLine()?.Trim(), out short value))
                    return value;

                inputProvider.WriteLine("Invalid format, integer expected");
            }
        }

        public override Task<short[]> GetNextMoveAsync() {
            Console.WriteLine("It's " + Symbol + "'s turn.");

            short row = ReadShort("Enter a row:");
            short col = ReadShort("Enter a col:");

            return Task.FromResult(new[] { row, col });
        }
    }
}
