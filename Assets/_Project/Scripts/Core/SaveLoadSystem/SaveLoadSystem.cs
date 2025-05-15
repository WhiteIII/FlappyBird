using UnityEngine;

namespace Project.Core.SaveLoad
{
    public class SaveLoadSystem : ISaveLoadSystem<int>
    {
        private const string KEY = "PlayerPoints";
        
        public void Save(int points) =>
            PlayerPrefs.SetInt(KEY, points);

        public int Load() =>
            PlayerPrefs.GetInt(KEY);
    }
}
