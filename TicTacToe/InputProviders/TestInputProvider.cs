namespace MAUI_TicTacToe.InputProviders {
    public class TestInputProvider :IInputProvider {
        private readonly Queue<string> _inputs = new();

        public TestInputProvider(IEnumerable<string> inputs) {
            foreach(var s in inputs) _inputs.Enqueue(s);
        }

        public string? ReadLine() => _inputs.Dequeue();
        public void WriteLine(string _) { }
    }
}
