using Project.Config;
using Project.Core.PlayerController;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Project.Core.Obstacle
{
    public class ObstacleSpawner
    {
        private readonly IFactory<ObstacleObjectData> _factory;
        private readonly Vector2 _spawnPoint;
        private readonly GameBehavior _gameBehavior;
        private readonly ObjectPool<ObstacleObjectData> _obstacleObjectPool;
        private readonly List<ObstacleObjectData> _obstaclesData = new();
        private readonly LinkedList<ObstacleObjectData> _receivedObjects = new();
        private readonly ObstaclePositionController _obstaclePositionController;
        private readonly PlayerPointsCounter _playerPointsCounter;

        public ObstacleSpawner(
            IFactory<ObstacleObjectData> factory,
            Vector2 spawnPoint,
            GameBehavior gameBehavior,
            ObstacleData obstacleData,
            PlayerPointsCounter playerPointsCounter)
        {
            _factory = factory;
            _spawnPoint = spawnPoint;
            _gameBehavior = gameBehavior;
            _playerPointsCounter = playerPointsCounter;
            _obstaclePositionController = new ObstaclePositionController(obstacleData);
            _obstacleObjectPool = new ObjectPool<ObstacleObjectData>(OnCreate, OnGet, OnRelease, OnDestroy, true, obstacleData.PoolMaxSize);
        }

        private void OnDestroy(ObstacleObjectData obstacleData)
        {
            GameObject.Destroy(obstacleData.ObstacleGameObject);
            _obstaclesData.Remove(obstacleData);
        }
        
        private ObstacleObjectData OnCreate()
        {
            ObstacleObjectData obstacleData = _factory.Create();
            obstacleData.ObstacleReleaseController.Initialize(this);
            obstacleData.ObstaclePointerController.Initialize(_playerPointsCounter);
            _obstaclesData.Add(obstacleData);
            return obstacleData;
        }

        private void OnGet(ObstacleObjectData obstacleData)
        {
            _receivedObjects.AddLast(obstacleData);
            ObstacleMovement obstacleMovement = obstacleData.ObstacleMovement;
            obstacleMovement.SetPosition(_spawnPoint);
            _obstaclePositionController.SetSpace(
                obstacleData.ObstacleObjects.TopObstacle.transform, 
                obstacleData.ObstacleObjects.BottomObstacle.transform);

            obstacleData.ObstacleGameObject.SetActive(true);
            _gameBehavior.AddUpdateableObject(obstacleMovement);
        }

        private void OnRelease(ObstacleObjectData obstacleData)
        {
            _gameBehavior.RemoveUpdateableObject(obstacleData.ObstacleMovement);
            obstacleData.ObstacleGameObject.SetActive(false);
        }

        public GameObject Get() =>
            _obstacleObjectPool.Get().ObstacleGameObject;

        public void Release(GameObject gameObject)
        {
            foreach (var obstacleData in _obstaclesData) 
            { 
                if (gameObject == obstacleData.ObstacleGameObject)
                {
                    _obstacleObjectPool.Release(obstacleData);
                    _receivedObjects.Remove(obstacleData);
                }
            }
        }

        public void ReleaseAll()
        {
            foreach (var obstacleData in _receivedObjects)
                _obstacleObjectPool.Release(obstacleData);

            _receivedObjects.Clear();  
        }
    }
}