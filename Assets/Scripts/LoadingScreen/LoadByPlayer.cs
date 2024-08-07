using System;
using UnityEngine;
using UnityEngine.UI;

namespace LoadingScreen
{
    public class LoadByPlayer : MonoBehaviour
    {
        public Slider slider;
        public bool isReady;
        
        private bool _isFast = true;
        public bool isInteractable;

        public static event Action OnReadySlider;
        
        public void ValueChanged(float value)
        {
            float max = 99f;
            
            if (value >= 75f && value <= max)
            {
                _isFast = false;
            }
            else
            {
                _isFast = true;
            }

            if (value >= slider.maxValue * 0.99f)
            {
                OnReadySlider?.Invoke();
                isReady = true;
            }
        }

        /*private void Update()
        {
            if (!_isFast && _isValueChanged)
            {
                if (_timeLeft > 0)
                {
                    _timeLeft -= Time.deltaTime;
                    slider.maxValue += 0.02f;
                } else
                {
                    _timeLeft = time;
                }
            }

            if (_isValueChanged)
            {
                if (_timeLeft2 > 0)
                {
                    _timeLeft2 -= Time.deltaTime;
                    slider.value += 0.04f;
                } else
                {
                    _timeLeft2 = time;
                }
            }
        }*/

        private void OnMouseUp()
        {
            if(isInteractable) Click();
        }

        private void Click()
        {
            //if (DialogueController.instance.DialogueInstanceQue.Count > 0) DialogueController.instance.EndDialogue();
            if (_isFast)
            {
                slider.value += 6f;
            }

            else if (!_isFast)
            {
                slider.value += 2f;
            }
        }
    }
}
