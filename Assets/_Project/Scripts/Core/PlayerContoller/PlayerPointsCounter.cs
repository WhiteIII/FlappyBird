using Project.Core.UI;

namespace Project.Core.PlayerController
{
    public class PlayerPointsCounter
    {
        private readonly IPointsView<int> _pointsView;

        public PlayerPointsCounter(IPointsView<int> pointsView) =>
            _pointsView = pointsView;

        public int PointsCount { get; private set; }
        
        public void AddPoints()
        {
            PointsCount++;
            _pointsView.SetCurrentPoints(PointsCount);
        }

        public void Reset() =>
            PointsCount = 0;
    }
}
