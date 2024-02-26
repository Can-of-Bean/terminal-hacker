using Terminal;

namespace Commands
{
    public class MakeDirectoryCommand : ICommand
    {
        public void Execute(string[] args)
        {
            if (args.Length == 0)
            {
                TerminalControl.Instance.WriteLineToConsole("mkdir: missing operand");
            }
            else
            {
                string path = args[0];
                bool created;
                if (path.StartsWith('/'))
                {
                    path = path.Remove(0, 1);
                    created = TerminalControl.Instance.CurrentFileSystem.Root.CreateDirectory(path);
                }
                else
                {
                    created = TerminalControl.Instance.CurrentFileSystem.CurrentDirectory.CreateDirectory(path);
                }

                if (!created)
                {
                    TerminalControl.Instance.WriteLineToConsole("Directory already exists.");
                }
            }

        }

        public string Name { get; } = "mkdir";
        public string Description { get; } = "Creates a new directory.";
    }
}