namespace Project.Core.PlayerController
{
    public interface IPlayerMovement
    {
        void EnableMove();
        void DisableMove();
        void Jump();
        void SetOnStartPosition();
    }
}
