using System;

namespace Project.Core
{
    public interface ITransition
    {
        Type To { get; }
        bool CanTranslate(IState fromState);
    }
}
