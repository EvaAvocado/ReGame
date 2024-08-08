using System;
using UnityEngine;

namespace Tag
{
    public class ElementWithGear : MonoBehaviour
    {
        public Animator animator;
        public ParticleSystem particle;
        public bool isCanMove = true;
        public bool isWithGear;

        private bool _isSabotage;
        private int _counter;

        public static event Action OnReadyElement;
        
        public void MouseDown()
        {
            if (!isCanMove)
            {
                if (_counter < 5)
                {
                    animator.Play("twitch");
                    particle.Play();
                    _counter++;

                    if (_counter == 5)
                    {
                        isCanMove = true;
                    }
                }
            }
        }

        public void OnReady()
        {
            if (!_isSabotage)
            {
                OnReadyElement?.Invoke();
                _isSabotage = true;
            }
            
        }
    }
}
