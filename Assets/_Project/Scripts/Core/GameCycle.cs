using Project.Core.Obstacle;
using Project.Core.PlayerController;
using Project.Core.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Core
{
    public class GameCycle : MonoBehaviour, IGameCycle
    {
        private const string SCENE_NAME = "GameScene";
        
        private ObstacleSpawner _obstacleSpawner;
        private IUpdatingController _updatingController;
        private IWindow _menuWindowController;
        private IWindow _gameOverWindowController;
        private IWindow _pointsController;
        private float _obstacleSpawnAmplitude;
        private IPlayerMovement _playerMovement;
        private IPointsView<int> _gameOverWindowPointsView;
        private PlayerPointsCounter _playerPointsCounter;
        private BirdRotateAnimation _birdRotateAnimation;

        private bool _gameIsActive = false; 
        
        public void Initialize(
            ObstacleSpawner obstacleSpawner,
            IUpdatingController updatingController,
            float obstacleSpawnAmplitude,
            IWindow menuWindowController,
            IWindow gameOverWindowController,
            IWindow pointsViewController,
            IPlayerMovement playerMovement,
            IPointsView<int> gameOverWindowPointsView,
            PlayerPointsCounter playerPointsCounter,
            BirdRotateAnimation birdRotateAnimation)
        {
            _obstacleSpawner = obstacleSpawner;
            _updatingController = updatingController;
            _obstacleSpawnAmplitude = obstacleSpawnAmplitude;
            _menuWindowController = menuWindowController;
            _gameOverWindowController = gameOverWindowController;
            _pointsController = pointsViewController;
            _playerMovement = playerMovement;
            _gameOverWindowPointsView = gameOverWindowPointsView;
            _playerPointsCounter = playerPointsCounter;
            _birdRotateAnimation = birdRotateAnimation;
            _playerMovement.DisableGravitation();
            _playerMovement.DisableInput();
        }
        
        public void StartGame()
        {
            _gameIsActive = true;
            _updatingController.EnableUpdating();
            _menuWindowController.Hide();
            _pointsController.Show();
            _playerMovement.EnableGravitation();
            _playerMovement.EnableInput();
            _birdRotateAnimation.EnableAnimation();
            _playerMovement.Jump();
            _birdRotateAnimation.Jump();

            StartCoroutine(StartSpawnObstacles());
        }

        public void StopGame()
        {
            _gameIsActive = false;
            _gameOverWindowPointsView.SetCurrentPoints(_playerPointsCounter.PointsCount);
            _gameOverWindowController.Show();
            _pointsController.Hide();
            _updatingController.DisableUpdating();
            _playerMovement.DisableInput();
            _birdRotateAnimation.DisableAnimation();
        }
        
        public void RestartScene() =>
            SceneManager.LoadScene(SCENE_NAME);

        private IEnumerator StartSpawnObstacles()
        {
            WaitForSeconds waitForSeconds = new(_obstacleSpawnAmplitude);
            
            while (_gameIsActive)
            {
                _obstacleSpawner.Get();
                yield return waitForSeconds;
            }
        }
    }
}
