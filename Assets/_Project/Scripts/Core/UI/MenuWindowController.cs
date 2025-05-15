using System;
using Unity.VisualScripting;

namespace Project.Core.UI
{
    public class MenuWindowController : IInitializable, IDisposable
    { 
        private readonly UnityEngine.UI.Button _button;
        private readonly BaseStateController _gameStateController;

        public MenuWindowController(
            UnityEngine.UI.Button button,
            BaseStateController gameStateController)
        {
            _button = button;
            _gameStateController = gameStateController;
        }
        
        public void Initialize() =>
            _button.onClick.AddListener(StartGameplay);

        public void Dispose() =>
            _button.onClick.RemoveListener(StartGameplay);

        private async void StartGameplay() =>
            await _gameStateController.Translate(typeof(GameplayState));
    }
}
