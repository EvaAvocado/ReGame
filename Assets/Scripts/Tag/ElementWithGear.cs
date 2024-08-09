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

        public bool isSabotage;
        private int _counter;

        public static event Action OnReadyElement;
        
        public void MouseDown()
        {
            if (!isCanMove && !isSabotage)
            {
                if (_counter < 5)
                {
                    if(animator) animator.Play("twitch");
                    if(particle) particle.Play();
                    else
                    {
                        return;
                    }
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
            if (!isSabotage)
            {
                OnReadyElement?.Invoke();
                isSabotage = true;
            }
            
        }
    }
}
