namespace Terminal
{
    public class LoopbackControlTarget : ITerminalControlTarget
    {
        public void HandleUserInput(string message)
        {
            TerminalControl.Instance.WriteLineToConsole(TerminalControl.Instance.InputHeader + "\n" + message);
        }
    }
}