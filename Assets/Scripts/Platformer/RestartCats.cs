using Platformer.Creatures;
using UnityEngine;

namespace Platformer
{
    public class RestartCats : MonoBehaviour
    {
        public Character[] characters;
        
        public void Restart()
        {
            foreach (var character in characters)
            {
                character.Spawn();
            }
        }
    }
}
