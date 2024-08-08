using System;
using System.Collections.Generic;
using System.Linq;
using Gears;
using Shovel;
using UnityEngine;
using UnityEngine.UI;

namespace LoadingScreen
{
    public class ScreensCounter : MonoBehaviour
    {
        public int neededCount;
        public int count;
        public bool isReady;

        private List<Slider> sliders = new List<Slider>();

        public static event Action OnAllCount;
        
        private void OnEnable()
        {
            LoadByPlayer.OnReadySlider += AddCount;
            SliderGearLoading.OnReadySlider += AddCount;
            LoadByShovel.OnReadySlider += AddCount;
        }
        
        private void OnDisable()
        {
            LoadByPlayer.OnReadySlider -= AddCount;
            SliderGearLoading.OnReadySlider -= AddCount;
            LoadByShovel.OnReadySlider -= AddCount;
        }

        private void AddCount(Slider slider)
        {
            if (!sliders.Contains(slider))
            {
                count++;
                sliders.Add(slider);
            }
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
