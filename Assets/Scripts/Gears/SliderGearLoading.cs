using System;
using LoadingScreen;
using UnityEngine;
using UnityEngine.UI;

namespace Gears
{
    public class SliderGearLoading : MonoBehaviour
    {
        public bool isCanLoading;
        private float time = 0.005f;

        private Slider _slider;
        private float _timeLeft;
        private float _timeLeft2;

        private bool _isReady;
        private bool _isNeedToComplete;
        private bool _stop;

        private bool _isReadyTrue;

        public static event Action<Slider> OnReadySlider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }
        
        private void OnEnable()
        {
            ScreensCounter.OnAllCount += GearsReady;
        }
        
        private void OnDisable()
        {
            ScreensCounter.OnAllCount -= GearsReady;
        }

        private void FixedUpdate()
        {
            if (isCanLoading && !_stop)
            {
                _isNeedToComplete = true;
                if (_timeLeft > 0)
                {
                    _timeLeft -= Time.fixedDeltaTime;
                    _slider.value += 1f;
                }
                else
                {
                    _timeLeft = time;
                }
            }
            
            if (!isCanLoading && _isNeedToComplete && !_stop && !_isReadyTrue)
            {
                if (_timeLeft2 > 0)
                {
                    _timeLeft2 -= Time.fixedDeltaTime;
                    _slider.value -= 2f;
                }
                else
                {
                    _timeLeft2 = time;
                }

                if (_slider.value <= 0)
                {
                    _isNeedToComplete = false;
                }
            }

            if (_slider.value >= 99f && !_isReady)
            {
                OnReadySlider?.Invoke(_slider);
                _isReady = true;
                _isReadyTrue = true;
            }
        }

        public void SetIsCanLoading(bool status)
        {
            isCanLoading = status;
            if(!status) _isReady = false; 
        }

        private void GearsReady()
        {
            _stop = true;
            _slider.value = _slider.maxValue;
        }
    }
}