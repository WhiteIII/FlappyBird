using Project.Core.Obstacle;
using Project.Core.PlayerController;
using Project.Core.SaveLoad;
using Project.Core.UI;

namespace Project.Core
{
    public class GameStateControllerFactory : IFactory<BaseStateController>
    {
        private readonly IWindowViewController _menuWindowViewController;
        private readonly IWindowViewController _gameOverWindowViewController;
        private readonly IPointsView<int> _menuWindowPointsView;
        private readonly IPointsView<int> _gameOverWindowPointView;
        private readonly ISaveLoadSystem<int> _saveLoadSystem;
        private readonly IWindowViewController _shadowPopup;
        private readonly IUpdatingController _updatingController;
        private readonly IPlayerMovement _playerMovement;
        private readonly PlayerPointsCounter _playerPointsCounter;
        private readonly ObstacleSpawner _obstacleSpawner;
        private readonly BaseStateController _playerStateController;
        private readonly float _obstacleSpawnAmplitude;
        private readonly IPlayerHealth _playerHealth;
        private readonly IWindowViewController _pointsCounterView;
        private readonly IPointsView<int> _pointsView;
        private readonly BirdRotateAnimation _birdRotateAnimation;

        public GameStateControllerFactory(
            IWindowViewController menuWindowViewController,
            IWindowViewController gameOverWindowViewController,
            IUpdatingController updatingController,
            IPlayerMovement playerMovement,
            ObstacleSpawner obstacleSpawner,
            float obstacleSpawnAmplitude,
            IWindowViewController shadowPopup,
            BaseStateController playerStateController,
            ISaveLoadSystem<int> saveLoadSystem,
            IPointsView<int> menuWindowPointsView,
            IPointsView<int> gameOverWindowPointsView,
            PlayerPointsCounter playerPointsCounter,
            IPlayerHealth playerHealth,
            IWindowViewController pointsCounterView,
            IPointsView<int> pointsView,
            BirdRotateAnimation birdRotateAnimation)
        {
            _menuWindowViewController = menuWindowViewController;
            _gameOverWindowViewController = gameOverWindowViewController;
            _updatingController = updatingController;
            _playerMovement = playerMovement;
            _obstacleSpawner = obstacleSpawner;
            _obstacleSpawnAmplitude = obstacleSpawnAmplitude;
            _shadowPopup = shadowPopup;
            _playerStateController = playerStateController;
            _saveLoadSystem = saveLoadSystem;
            _menuWindowPointsView = menuWindowPointsView;
            _gameOverWindowPointView = gameOverWindowPointsView;
            _playerPointsCounter = playerPointsCounter;
            _playerHealth = playerHealth;
            _pointsCounterView = pointsCounterView;
            _pointsView = pointsView;
            _birdRotateAnimation = birdRotateAnimation;
        }

        public BaseStateController Create() =>
            new(
                new IState[]
                {
                    new MenuState(
                        _menuWindowViewController, 
                        _playerMovement, 
                        _shadowPopup, 
                        _playerStateController,
                        _menuWindowPointsView,
                        _saveLoadSystem,
                        _playerHealth,
                        _pointsCounterView,
                        _birdRotateAnimation),
                    new GameplayState(
                        _playerMovement, 
                        _updatingController, 
                        _obstacleSpawner, 
                        _obstacleSpawnAmplitude),
                    new GameOverState(
                        _gameOverWindowViewController, 
                        _playerMovement, 
                        _updatingController, 
                        _obstacleSpawner, 
                        _playerPointsCounter, 
                        _saveLoadSystem, 
                        _shadowPopup,
                        _gameOverWindowPointView,
                        _pointsCounterView,
                        _pointsView,
                        _birdRotateAnimation)
                },
                new ITransition[]
                {
                    new Transition<MenuState, GameplayState>(),
                    new Transition<GameplayState, GameOverState>(),
                    new Transition<GameOverState, MenuState>()
                },
                typeof(MenuState));
    }
}
