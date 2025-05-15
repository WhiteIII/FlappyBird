using Project.Core.PlayerController;
using Project.Core.SaveLoad;
using Project.Core.UI;
using System.Threading.Tasks;

namespace Project.Core
{
    public class MenuState : IEnterAsyncState, IExitAsyncState
    {
        private readonly IWindowViewController _menuWindowViewController;
        private readonly IWindowViewController _shadowPopup;
        private readonly IWindowViewController _pointsCounterView;
        private BaseStateController _playerStateController;
        private readonly IPlayerMovement _playerMovement;
        private readonly IPointsView<int> _menuWindowPointsView;
        private readonly ISaveLoadSystem<int> _saveLoadSystem;
        private readonly IPlayerHealth _playerHealth;
        private readonly BirdRotateAnimation _birdRotateAnimation;

        public MenuState(
            IWindowViewController menuWindowViewController, 
            IPlayerMovement playerMovement,
            IWindowViewController shadowPopup,
            BaseStateController playerStateController,
            IPointsView<int> menuWindowPointsView,
            ISaveLoadSystem<int> saveLoadSystem,
            IPlayerHealth playerHealth,
            IWindowViewController pointsCounterView,
            BirdRotateAnimation birdRotateAnimation)
        {
            _menuWindowViewController = menuWindowViewController;
            _playerMovement = playerMovement;
            _shadowPopup = shadowPopup;
            _playerStateController = playerStateController;
            _menuWindowPointsView = menuWindowPointsView;
            _saveLoadSystem = saveLoadSystem;
            _playerHealth = playerHealth;
            _pointsCounterView = pointsCounterView;
            _birdRotateAnimation = birdRotateAnimation;
        }

        public async Task EnterAsync()
        {
            _playerHealth.Revive();
            _playerMovement.SetOnStartPosition();
            _playerStateController.Translate(typeof(CalmState));
            _menuWindowPointsView.SetCurrentPoints(_saveLoadSystem.Load());

            await _shadowPopup.PlayHideAnimationAsync();
            await _menuWindowViewController.PlayShowAnimationAsync();
        }

        public async Task ExitAsync()
        {
            _playerStateController.Translate(typeof(ActiveState));
            _playerMovement.EnableMove();
            _playerMovement.Jump();
            _birdRotateAnimation.Jump();
            _playerMovement.DisableMove();
            await Task.WhenAll(
                _menuWindowViewController.PlayHideAnimationAsync(), 
                _pointsCounterView.PlayShowAnimationAsync());
        }
    }
}
