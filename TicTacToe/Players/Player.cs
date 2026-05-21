using TicTacToe.Enums;

namespace MAUI_TicTacToe.Players {
    public abstract class Player {
        public Symbol Symbol { get; }

        protected Player(Symbol symbol) {
            Symbol = symbol;
        }

        public abstract Task<short[]> GetNextMoveAsync();
    }
}
