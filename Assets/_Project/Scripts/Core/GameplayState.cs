using Project.Core.Obstacle;
using Project.Core.PlayerController;
using System;
using System.Threading.Tasks;

namespace Project.Core
{
    public class GameplayState : IDisposable, IEnterState, IExitState
    {
        private readonly IPlayerMovement _playerMovement;
        private readonly IUpdatingController _gameBehavior;
        private readonly ObstacleSpawner _obstacleSpawner;
        private readonly float _spawnAmplitude;

        private bool _isActive = false;

        public GameplayState(
            IPlayerMovement playerMovement,
            IUpdatingController updatingController,
            ObstacleSpawner obstacleSpawner,
            float spawnAmplitude)
        {
            _playerMovement = playerMovement;
            _gameBehavior = updatingController;
            _obstacleSpawner = obstacleSpawner;
            _spawnAmplitude = spawnAmplitude;
        }

        public void Dispose() =>
            Exit();

        public void Enter()
        {
            _playerMovement.EnableMove();
            _gameBehavior.EnableUpdating();
            _isActive = true;
            StartSpawnObstacles();
        }

        public void Exit() =>
            _isActive = false;

        private async void StartSpawnObstacles()
        {
            while (_isActive) 
            {
                _obstacleSpawner.Get();
                await Task.Delay(Convert.ToInt32(_spawnAmplitude * 1000));
            }
        }
    }
}
