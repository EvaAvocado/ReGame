using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Platformer
{
    public class PointsInPlatformer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        public int neededPoints = 7;
        private int _score = 0;

        private List<Star> _stars = new List<Star>();

        public static event Action OnAllStarsCollected;

        private void Awake()
        {
            _scoreText.text = "0/" + neededPoints;
        }

        public void AddScore(Star star)
        {
            if (!_stars.Contains(star))
            {
                _score++;
                _scoreText.text = _score.ToString() + "/" + neededPoints;
                _stars.Add(star);

                if (_score == neededPoints)
                {
                    OnAllStarsCollected?.Invoke();
                }
            }
        }
    }
}