using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Components
{
    public class OnTriggerExit : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerToTrigger;
        [SerializeField] private UnityEvent _actionIfEnter;

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_layerToTrigger.Contains(other.gameObject.layer))
            {
                _actionIfEnter?.Invoke();
            }
        }
    }
}
