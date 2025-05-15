using Project.Config;
using UnityEngine;

namespace Project.Core.Obstacle
{
    public class ObstaclePositionController
    {
        private readonly ObstacleData _obstacleData;

        public ObstaclePositionController(ObstacleData obstacleData) =>
            _obstacleData = obstacleData;

        public void SetSpace(Transform top, Transform bottom) 
        {
            float gapSize = Random.Range(_obstacleData.MinGapSize, _obstacleData.MaxGapSize);
            float gapCenterY = Random.Range(_obstacleData.MinVerticalPosition, _obstacleData.MaxVerticalPosition);

            float topPipeY = gapCenterY + gapSize / 2;
            float bottomPipeY = gapCenterY - gapSize / 2;

            top.position = new Vector3(top.position.x, topPipeY, top.position.z);
            bottom.position = new Vector3(bottom.position.x, bottomPipeY, bottom.position.z);
        }
    }
}