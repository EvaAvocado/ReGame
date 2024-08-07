using TMPro;
using UnityEngine;

namespace UI
{
    public class PointsInPlatformer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        private int _score = 0;
        
        public void AddScore()
        {
            _score++;
            _scoreText.text = _score.ToString();
        }
    }
}