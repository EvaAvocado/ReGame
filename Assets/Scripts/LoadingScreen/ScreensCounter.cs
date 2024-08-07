using System;
using UnityEngine;

namespace LoadingScreen
{
    public class ScreensCounter : MonoBehaviour
    {
        public int neededCount;
        public int count;
        public bool isReady;

        public static event Action OnAllCount;
        
        private void OnEnable()
        {
            LoadByPlayer.OnReadySlider += AddCount;
        }
        
        private void OnDisable()
        {
            LoadByPlayer.OnReadySlider -= AddCount;
        }

        private void AddCount()
        {
            count++;
        }

        private void Update()
        {
            if (count == neededCount && !isReady)
            {
                isReady = true;
                OnAllCount?.Invoke();
            }
        }
    }
}
