using Project.Config;
using Project.Core.Obstacle;
using Project.Core.PlayerController;
using Project.Core.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.Bootstrap
{
    public class EntryPoint : MonoBehaviour
    {
        [Header("Player:")]
        [SerializeField] private GameObject _playerGameObject;
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private BirdRotateAnimation _birdRotateAnimation;

        [Header("Obstacle:")]
        [SerializeField] private ObstacleData _obstacleData;
        [SerializeField] private GameObject _obstaclePrefab;
        [SerializeField] private Transform _obstacleSpawnPoint;

        [Header("GameBehavior")]
        [SerializeField] private GameObject _gameBehaviorPrefab;
        [SerializeField] private GameCycle _gameCycle;

        [Header("GameOverWindow:")]
        [SerializeField] private GameObject _gameOverWindowGameObject;
        [SerializeField] private Button _restartGameButton;
        [SerializeField] private TMP_Text _gameOverWindowPointsText;

        [Header("MenuWindow:")]
        [SerializeField] private GameObject _menuWindowGameObject;
        [SerializeField] private Button _startGameButton;

        [Header("PointsCounterView")]
        [SerializeField] private GameObject _pointsViewGameObject;
        [SerializeField] private TMP_Text _counter;

        private PlayerMovement _playerMovement;
        private MenuWindowController _menuWindowController;
        private GameOverWindowController _gameOverWindowController;

        private void Awake()
        {
            BaseWindowViewController menuWindowViewController = new(_menuWindowGameObject);
            BaseWindowViewController gameOverWindowViewController = new(_gameOverWindowGameObject);
            BaseWindowViewController pointsViewController = new(_pointsViewGameObject);

            _playerMovement = new(
                _playerInput, 
                _playerGameObject.GetComponent<Rigidbody2D>(), 
                _playerData.JumpForce);

            PointsView gameOverWindowPointsView = new(_gameOverWindowPointsText);
            PointsView pointsView = new(_counter);
            PlayerPointsCounter playerPointsCounter = new(pointsView);

            GameObject gameBehaviorGameObject = GameObject.Instantiate(_gameBehaviorPrefab);
            GameBehavior gameBehavior = gameBehaviorGameObject.GetComponent<GameBehavior>();
            ObstacleSpawner obstacleSpawner = new ObstacleSpawner(
                new ObstacleFactory(_obstaclePrefab, _obstacleData.Speed),
                _obstacleSpawnPoint.position,
                gameBehavior,
                _obstacleData,
                playerPointsCounter);

            GameObject.DontDestroyOnLoad(gameBehaviorGameObject);

            _menuWindowController = new MenuWindowController(_startGameButton, _gameCycle);
            _gameOverWindowController = new GameOverWindowController(_restartGameButton, _gameCycle);

            _gameCycle.Initialize(
                obstacleSpawner, 
                gameBehavior, 
                _obstacleData.SpawnAmplitude,
                menuWindowViewController,
                gameOverWindowViewController,
                pointsViewController,
                _playerMovement,
                gameOverWindowPointsView,
                playerPointsCounter,
                _birdRotateAnimation);
            _playerMovement.Initialize();
            _menuWindowController.Initialize();
            _gameOverWindowController.Initialize(); 
            _playerHealth.Initialize(_gameCycle);
        }

        private void OnDestroy()
        {
            _playerMovement.Dispose();
            _menuWindowController.Dispose();
            _gameOverWindowController.Dispose();
        }
    }
}