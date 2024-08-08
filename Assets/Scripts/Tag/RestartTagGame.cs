using NULLcode_Studio._15Puzzle.Scripts;
using UnityEngine;

namespace Tag
{
    public class RestartTagGame : MonoBehaviour
    {
        public GameControl tagGame;

        public void Restart()
        {
            tagGame.StartNewGame();
        }
    }
}
