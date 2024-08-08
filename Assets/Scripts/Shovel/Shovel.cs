using System;
using System.Collections;
using UnityEngine;

namespace Shovel
{
    public class Shovel : MonoBehaviour
    {
        public ParticleSystem particle;
        public bool isCanDigging;

        public static event Action OnClick;

        private void Update()
        {
            if (isCanDigging)
            {
                transform.position =
                    Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            }
        }

        private void OnMouseDown()
        {
            if (isCanDigging)
            {
                ParticleSystem newParticle = Instantiate(particle,
                    new Vector3(particle.transform.position.x, particle.transform.position.y,
                        particle.transform.position.z), Quaternion.identity);
                newParticle.Play();

                OnClick?.Invoke();

                StartCoroutine(Lifetime(newParticle));
            }
        }

        private IEnumerator Lifetime(ParticleSystem particleSystem)
        {
            yield return new WaitForSeconds(2f);
            Destroy(particleSystem.gameObject);
        }
    }
}