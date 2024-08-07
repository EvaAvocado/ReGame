using System;
using UnityEngine;

namespace Items.Gears
{
    public class Gear : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private bool _isCanDrag;
        
        private bool _isCanRotate;
        
        private Vector3 _mousePos;
        private Camera _camera;
        private Vector3 _startPos;
        private bool _isBeingHeld;

        private bool _isRight;

        public bool IsRight
        {
            get => _isRight;
            set => _isRight = value;
        }

        public static event Action<Gear> OnStartMoving;
        public static event Action OnStopMoving;
        
        public void RotateGear()
        {
            _isCanRotate = true;
        }

        public void StopGear()
        {
            _isCanRotate = false;
        }

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (_isCanRotate)
            {
                _sprite.gameObject.transform.Rotate(0, 0, (_isRight ? -1 : 1) * _rotateSpeed * Time.deltaTime);
            }
            
            if (_isCanDrag && _isBeingHeld)
            {
                _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
                transform.localPosition = new Vector3(_mousePos.x - _startPos.x, _mousePos.y - _startPos.y,
                    transform.localPosition.z);
            }
        }

        public void OnMouseDown()
        {
            if (Input.GetMouseButtonDown(0) && _isCanDrag)
            {
                gameObject.GetComponent<Rigidbody2D>().simulated = true;
                
                OnStartMoving?.Invoke(this);
                _startPos = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.localPosition;
                _isBeingHeld = true;
            }
        }

        public void OnMouseUp()
        {
            if (Input.GetMouseButtonUp(0) && _isCanDrag)
            {
                OnStopMoving?.Invoke();
                _isBeingHeld = false;
            }
        }
    }
}
