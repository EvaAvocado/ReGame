using System;
using UnityEngine;

namespace Tag
{
    public class ElementWithGear : MonoBehaviour
    {
        public Animator animator;
        public ParticleSystem particle;
        public bool isCanMove;

        private int _counter;

        public static event Action OnReadyElement;
        
        private void OnMouseDown()
        {
            if (_counter < 5)
            {
                animator.Play("twitch");
                particle.Play();
                _counter++;
                isCanMove = true;

                if (_counter == 5)
                {
                    OnReadyElement?.Invoke();
                }
            }
        }
    }
}
