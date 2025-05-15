namespace Project.Core.UI
{
    public class PointsView : IPointsView<int>
    {
        private readonly TMPro.TMP_Text _text;

        public PointsView(TMPro.TMP_Text text) =>
            _text = text;

        public void SetCurrentPoints(int points) =>
            _text.text = points.ToString();
    }
}
