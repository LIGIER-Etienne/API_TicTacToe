namespace MAUI_TicTacToe.InputProviders {
    public class DummyInputProvider : IInputProvider
    {
        public string? ReadLine() => string.Empty;
        public void WriteLine(string message) { }
    }
}
