using System;
using UI;
using UnityEngine;
using Utils;

namespace Platformer
{
    public class Star : MonoBehaviour
    {
        [SerializeField] private LayerMask _characterLayer;
        [SerializeField] private PointsInPlatformer _points;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_characterLayer.Contains(other.gameObject.layer))
            {
                DeleteThis();
                _points.AddScore(this);
            }
        }

        public void DeleteThis()
        {
            Destroy(gameObject);
        }
    }
}