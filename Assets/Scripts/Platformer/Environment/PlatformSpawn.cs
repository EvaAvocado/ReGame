using UnityEngine;

namespace Environment
{
    public class PlatformSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject _platformPrefab;

        private GameObject _spawnedPlatform;

        private void OnMouseDown()
        {
            Spawn();
        }

        private void Spawn()
        {
            if (_spawnedPlatform != null) Destroy(_spawnedPlatform);

            _spawnedPlatform = Instantiate(_platformPrefab,
                new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                    Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0), Quaternion.identity);
        }
    }
}