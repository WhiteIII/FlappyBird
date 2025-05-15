using System;

namespace Project.Core.PlayerController
{
    public interface IPlayerInput
    {
        event Action OnClick;
    }
}
