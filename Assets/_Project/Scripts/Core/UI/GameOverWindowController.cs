using System;
using Unity.VisualScripting;

namespace Project.Core.UI
{
    public class GameOverWindowController : IInitializable, IDisposable
    {
        private readonly UnityEngine.UI.Button _button;
        private readonly IGameCycle _gameCycle;

        public GameOverWindowController(
            UnityEngine.UI.Button button,
            IGameCycle gameCycle)
        {
            _button = button;
            _gameCycle = gameCycle;
        }

        public void Initialize() =>
            _button.onClick.AddListener(GoToMenu);

        public void Dispose() =>
            _button.onClick.RemoveListener(GoToMenu);

        private void GoToMenu() =>
            _gameCycle.RestartScene();
    }
}
