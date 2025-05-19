using System;
using Unity.VisualScripting;

namespace Project.Core.UI
{
    public class MenuWindowController : IInitializable, IDisposable
    { 
        private readonly UnityEngine.UI.Button _button;
        private readonly IGameCycle _gameCycle;

        public MenuWindowController(
            UnityEngine.UI.Button button,
            IGameCycle gameCycle)
        {
            _button = button;
            _gameCycle = gameCycle;
        }
        
        public void Initialize() =>
            _button.onClick.AddListener(StartGameplay);

        public void Dispose() =>
            _button.onClick.RemoveListener(StartGameplay);

        private void StartGameplay() =>
            _gameCycle.StartGame();
    }
}
