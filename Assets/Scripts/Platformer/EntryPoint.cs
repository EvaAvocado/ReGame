using Platformer.Creatures;
using UnityEngine;

namespace Platformer
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private Canvas _spawnPlatformCanvas;

        private void Awake()
        {
            //if (_character != null) _character.Init();
            if (_spawnPlatformCanvas != null) _spawnPlatformCanvas.gameObject.SetActive(true);
        }
    }
}