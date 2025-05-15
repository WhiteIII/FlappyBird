using System;
using UnityEngine;

namespace Project.Core.PlayerController
{
    public class PlayerInput : MonoBehaviour, IPlayerInput
    {
        public event Action OnClick;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetMouseButton(0))
                OnClick?.Invoke();
        }
    }
}
