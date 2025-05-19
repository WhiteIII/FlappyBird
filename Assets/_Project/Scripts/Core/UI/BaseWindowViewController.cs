using UnityEngine;

namespace Project.Core.UI
{
    public class BaseWindowViewController : IWindow
    {
        private readonly GameObject _windowGameObject;

        public BaseWindowViewController(
            GameObject windowGameObject)
        {
            _windowGameObject = windowGameObject;
        }

        public void Hide()
        {
            _windowGameObject.SetActive(false);
        }

        public void Show()
        {
            _windowGameObject.SetActive(true);
        }
    }
}
