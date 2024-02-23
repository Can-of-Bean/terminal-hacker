namespace Terminal
{
    public class LoopbackControlTarget : TerminalControlTarget
    {
        public override void HandleUserInput(string message)
        {
            TerminalControl.Instance.WriteLineToConsole(TerminalControl.Instance.InputHeader + "\n" + message);
        }
    }
}