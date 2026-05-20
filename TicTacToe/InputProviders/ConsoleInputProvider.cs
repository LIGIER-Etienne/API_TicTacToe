namespace MAUI_TicTacToe.InputProviders {
    public class ConsoleInputProvider :IInputProvider {
        public string? ReadLine() => Console.ReadLine();
        public void WriteLine(string message) => Console.WriteLine(message);
    }
}
