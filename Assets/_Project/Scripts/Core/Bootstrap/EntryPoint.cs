using Project.Config;
using Project.Core.Obstacle;
using Project.Core.PlayerController;
using Project.Core.SaveLoad;
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

        [Header("WindowData:")]
        [SerializeField] private WindowData _windowData;

        [Header("PopupData:")]
        [SerializeField] private PopupData _popupData;

        [Header("GameOverWindow:")]
        [SerializeField] private GameObject _gameOverWindowGameObject;
        [SerializeField] private Button _restartGameButton;
        [SerializeField] private RectTransform _gameOverWindowRectTransform;
        [SerializeField] private CanvasGroup _gameOverWindowCanvasGroup;
        [SerializeField] private TMP_Text _gameOverWindowPointsText;

        [Header("MenuWindow:")]
        [SerializeField] private GameObject _menuWindowGameObject;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private RectTransform _menuWindowRectTransform;
        [SerializeField] private CanvasGroup _menuWindowCanvasGroup;
        [SerializeField] private TMP_Text _menuWindowPointsText;

        [Header("ShadowPopup")]
        [SerializeField] private GameObject _shadowPopupGameObject;
        [SerializeField] private CanvasGroup _shadowPopupCanvasGroup;

        [Header("PointsCounterView")]
        [SerializeField] private GameObject _pointsViewGameObject;
        [SerializeField] private CanvasGroup _pointsViewCanvasGroup;
        [SerializeField] private TMP_Text _counter;

        private PlayerMovement _playerMovement;
        private MenuWindowController _menuWindowController;
        private GameOverWindowController _gameOverWindowController;
        private BaseStateController _gameStateController;
        private BaseWindowViewController _menuWindowViewController;
        private BaseWindowViewController _gameOverWindowViewController;
        private BaseUIControllerWithAlphaAnimation _shadowPopupController;
        private BaseUIControllerWithAlphaAnimation _pointsViewController;

        private void Awake()
        {
            BaseWindowViewController menuWindowViewController = new(
                new BaseWindowAnimation(_menuWindowRectTransform, _menuWindowCanvasGroup, _windowData.AnimationDuration),
                new Vector2(_menuWindowRectTransform.anchoredPosition.x, _menuWindowRectTransform.anchoredPosition.y - 200f),
                _menuWindowRectTransform.anchoredPosition,
                _menuWindowGameObject);
            BaseWindowViewController gameOverWindowViewController = new(
                new BaseWindowAnimation(_gameOverWindowRectTransform, _gameOverWindowCanvasGroup, _windowData.AnimationDuration),
                new Vector2(_gameOverWindowRectTransform.anchoredPosition.x, _gameOverWindowRectTransform.anchoredPosition.y - 200f),
                _gameOverWindowRectTransform.anchoredPosition,
                _gameOverWindowGameObject);
            BaseUIControllerWithAlphaAnimation shadowPopupController = new(
                new BaseAlphaAnimation(_shadowPopupCanvasGroup, _popupData.AnimationDuration),
                _shadowPopupGameObject);
            BaseUIControllerWithAlphaAnimation pointsViewController = new(
                new BaseAlphaAnimation(_pointsViewCanvasGroup, _popupData.AnimationDuration),
                _pointsViewGameObject);

            _playerMovement = new(
                _playerInput, 
                _playerGameObject.GetComponent<Rigidbody2D>(), 
                _playerData.JumpForce, 
                _playerTransform);
            PointsView menuWindowPointsView = new(_menuWindowPointsText);
            PointsView gameOverWindowPointsView = new(_gameOverWindowPointsText);
            PointsView pointsView = new(_counter);
            PlayerPointsCounter playerPointsCounter = new(pointsView);
            SaveLoadSystem saveLoadSystem = new();

            ObstacleSpawner obstacleSpawner;

            GameObject gameBehaviorGameObject = GameObject.Instantiate(_gameBehaviorPrefab);
            GameBehavior gameBehavior = gameBehaviorGameObject.GetComponent<GameBehavior>();
            
            GameObject.DontDestroyOnLoad(gameBehaviorGameObject);

            PlayerStateControllerFactory playerStateControllerFactory = new(
                _birdRotateAnimation, 
                _playerGameObject.GetComponent<Rigidbody2D>());
            obstacleSpawner = new ObstacleSpawner(
                new ObstacleFactory(_obstaclePrefab, _obstacleData.Speed),
                _obstacleSpawnPoint.position,
                gameBehavior,
                _obstacleData,
                playerPointsCounter);

            BaseStateController playerStateController = playerStateControllerFactory.Create();
            
            GameStateControllerFactory gameStateControllerFactory = new(
                menuWindowViewController, 
                gameOverWindowViewController, 
                gameBehavior,
                _playerMovement,
                obstacleSpawner,
                _obstacleData.SpawnAmplitude,
                shadowPopupController,
                playerStateController,
                saveLoadSystem,
                menuWindowPointsView,
                gameOverWindowPointsView,
                playerPointsCounter,
                _playerHealth,
                pointsViewController,
                pointsView,
                _birdRotateAnimation);

            BaseStateController gameStateController = gameStateControllerFactory.Create();

            _menuWindowController = new MenuWindowController(_startGameButton, gameStateController);
            _gameOverWindowController = new GameOverWindowController(_restartGameButton, gameStateController);
            _gameStateController = gameStateController;

            _menuWindowViewController = menuWindowViewController;
            _gameOverWindowViewController = gameOverWindowViewController;
            _shadowPopupController = shadowPopupController;
            _pointsViewController = pointsViewController;

            _playerMovement.Initialize();
            _menuWindowController.Initialize();
            _gameOverWindowController.Initialize(); 
            gameStateController.Initialize();
            _playerHealth.Initialize(gameStateController);
        }

        private void OnDestroy()
        {
            _playerMovement.Dispose();
            _menuWindowController.Dispose();
            _gameOverWindowController.Dispose();
            _gameStateController.Dispose();
            _menuWindowViewController.Dispose();
            _gameOverWindowViewController.Dispose();
            _shadowPopupController.Dispose();
            _pointsViewController.Dispose();
        }
    }
}