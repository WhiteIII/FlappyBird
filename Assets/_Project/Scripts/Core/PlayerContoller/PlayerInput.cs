using System;
using UnityEngine;

namespace Project.Core.PlayerController
{
    public class PlayerInput : MonoBehaviour, IPlayerInput
    {
        public event Action OnClick;

        private void Update()
        {
            if (Input.GetMouseButton(0))
                OnClick?.Invoke();
        }
    }
}
