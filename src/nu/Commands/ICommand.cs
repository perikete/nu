namespace nu.Commands
{
    using System.Collections.Generic;
    using Utility;

    public interface ICommand
    {
        void Execute(IEnumerator<IArgument> arguments);
    }
}