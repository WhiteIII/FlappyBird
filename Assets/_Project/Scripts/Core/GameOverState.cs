using Project.Core.Obstacle;
using Project.Core.PlayerController;
using Project.Core.SaveLoad;
using Project.Core.UI;
using System.Threading.Tasks;

namespace Project.Core
{
    public class GameOverState : IEnterAsyncState, IExitAsyncState
    {
        private readonly IWindowViewController _gameOverWindowViewController;
        private readonly IWindowViewController _shadowPopup;
        private readonly IPlayerMovement _playerMovement;
        private readonly IUpdatingController _gameBehavior;
        private readonly ObstacleSpawner _obstacleSpawner;
        private readonly PlayerPointsCounter _playerPointsCounter;
        private readonly ISaveLoadSystem<int> _saveLoadSystem;
        private readonly IPointsView<int> _gameOverWindowPointsView;
        private readonly IWindowViewController _pointsCounterView;
        private readonly IPointsView<int> _pointsView;
        private readonly BirdRotateAnimation _birdRotateAnimation;

        public GameOverState(
            IWindowViewController gameOverWindowViewController,
            IPlayerMovement playerMovement,
            IUpdatingController gameBehavior,
            ObstacleSpawner obstacleSpawner,
            PlayerPointsCounter playerPointsCounter,
            ISaveLoadSystem<int> saveLoadSystem,
            IWindowViewController shadowPopup,
            IPointsView<int> gameOverWindowPointsView,
            IWindowViewController pointsCounterView,
            IPointsView<int> pointsView,
            BirdRotateAnimation birdRotateAnimation)
        {
            _gameOverWindowViewController = gameOverWindowViewController;
            _playerMovement = playerMovement;
            _gameBehavior = gameBehavior;
            _obstacleSpawner = obstacleSpawner;
            _playerPointsCounter = playerPointsCounter;
            _saveLoadSystem = saveLoadSystem;
            _shadowPopup = shadowPopup;
            _gameOverWindowPointsView = gameOverWindowPointsView;
            _pointsCounterView = pointsCounterView;
            _pointsView = pointsView;
            _birdRotateAnimation = birdRotateAnimation;
        }

        public async Task EnterAsync()
        {
            if (_saveLoadSystem.Load() < _playerPointsCounter.PointsCount)
                _saveLoadSystem.Save(_playerPointsCounter.PointsCount);

            _birdRotateAnimation.DisableAnimation();
            _gameOverWindowPointsView.SetCurrentPoints(_playerPointsCounter.PointsCount);
            _playerPointsCounter.Reset();
            _playerMovement.DisableMove();
            _gameBehavior.DisableUpdating();
            await Task.WhenAll(
                _gameOverWindowViewController.PlayShowAnimationAsync(),
                _pointsCounterView.PlayHideAnimationAsync());
            _pointsView.SetCurrentPoints(0);
        }

        public async Task ExitAsync()
        {
            await _gameOverWindowViewController.PlayHideAnimationAsync();
            await _shadowPopup.PlayShowAnimationAsync(); 
            _obstacleSpawner.ReleaseAll();
        }
    }
}
