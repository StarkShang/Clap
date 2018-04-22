using System;

namespace Clap
{
    public abstract class CommandBase
    {
		protected abstract Dictionary<string, CommandBase> CommandTable { get; }
        protected abstract Dictionary<string, Action<Queue<string>>> OptionTable { get; }
        protected abstract void DefaultBehavior(Queue<string> args);

        public void Execute(Queue<string> args)
        {
            if (IsCommand(args)) CommandTable[args.Dequeue()].Execute(args);
            if (IsOption(args)) OptionTable[args.Dequeue()](args);
            DefaultBehavior(args);
        }

        public bool IsCommand(Queue<string> args)
        {
            return CommandTable != null
                && args.TryPeek(out var token)
                && CommandTable.ContainsKey(token);
        }

        public bool IsOption(Queue<string> args)
        {
            return OptionTable != null
                && args.TryPeek(out var token)
                && OptionTable.ContainsKey(token);
        }
    }
}
