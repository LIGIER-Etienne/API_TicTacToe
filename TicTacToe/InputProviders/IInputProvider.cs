namespace MAUI_TicTacToe.InputProviders {
    public interface IInputProvider {
        string? ReadLine();
        void WriteLine(string message);
    }
}
