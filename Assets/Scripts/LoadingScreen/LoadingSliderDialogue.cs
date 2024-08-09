using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace LoadingScreen
{
    public class LoadingSliderDialogue : MonoBehaviour
    {
        public Slider slider;

        private bool _isCanNext;
        private bool _isReady;
        private Coroutine _coroutine;

        public static event Action OnReadySlider;
        
        public void StartSlider(Action completeAction)
        {
            slider.DOValue(0.75f, 3f).OnComplete(()=>
            {
                StartCoroutine(TimerAfterStartSlider(completeAction));
            });
        }

        private void NextSlider()
        {
            slider.DOValue(1f, 2f).OnComplete(()=>OnReadySlider?.Invoke());
        }

        private void OnMouseEnter()
        {
            if (!_isReady && _isCanNext)
            {
                _coroutine = StartCoroutine(TimerToNextSlider());
            }
        }

        private void OnMouseExit()
        {
            if (!_isReady && _isCanNext)
            {
                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }
            }
        }

        private IEnumerator TimerToNextSlider()
        {
            yield return new WaitForSeconds(1f);
            _isReady = true;
            NextSlider();
        }

        private IEnumerator TimerAfterStartSlider(Action completeAction)
        {
            yield return new WaitForSeconds(3f);
            completeAction?.Invoke();
            _isCanNext = true;
        }
    }
}
