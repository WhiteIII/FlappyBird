using System.Collections.Generic;
using UnityEngine;

namespace Project.Core
{
    public class GameBehavior : MonoBehaviour, IUpdatingController
    {
        private readonly List<IUpdateable> _updateableObjects = new();

        private bool _isUpdating = false;

        public void EnableUpdating() =>
            _isUpdating = true;

        public void DisableUpdating() =>
            _isUpdating = false;

        public void AddUpdateableObject(IUpdateable updateableObject) =>
            _updateableObjects.Add(updateableObject);

        public void RemoveUpdateableObject(IUpdateable updateableObject) =>
            _updateableObjects.Remove(updateableObject);

        private void Update()
        {
            if (_isUpdating == false) 
                return;
            
            foreach (var updateableObject in _updateableObjects)
                updateableObject.Update();
        }
    }
}
