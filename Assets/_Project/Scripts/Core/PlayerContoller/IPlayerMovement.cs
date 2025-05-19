namespace Project.Core.PlayerController
{
    public interface IPlayerMovement
    {
        void EnableInput();
        void DisableInput();
        void EnableGravitation();
        void DisableGravitation();
        void Jump();
    }
}
