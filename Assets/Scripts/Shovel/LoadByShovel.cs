using System;
using UnityEngine;
using UnityEngine.UI;

namespace Shovel
{
    public class LoadByShovel : MonoBehaviour
    {
        public Slider slider;
        
        private bool _isFast = true;

        public static event Action<Slider> OnReadySlider;

        private void OnEnable()
        {
            Shovel.OnClick += Click;
        }
        
        private void OnDisable()
        {
            Shovel.OnClick -= Click;
        }

        public void Awake()
        {
            slider = GetComponent<Slider>();
        }

        public void ValueChanged(float value)
        {
            float max = 100f;
            
            /*if (value >= 75f && value <= max)
            {
                _isFast = false;
            }
            else
            {
                _isFast = true;
            }*/

            if (value >= 10f)
            {
                OnReadySlider?.Invoke(slider);
            }
        }

        private void Click()
        {
            slider.value += 0.5f;
            
            /*if (_isFast)
            {
                slider.value += 6/2.5f;
            }

            else if (!_isFast)
            {
                slider.value += 2/2.5f;
            }*/
        }
    }
}
