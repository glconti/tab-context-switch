#region using

using System;

#endregion

namespace TabContextSwitch.Core
{
    public interface IEventProvider
    {
        event Action<string, string> SolutionOpened;
        event Action<string, string> SolutionClosed;
    }
}