using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gears
{
    public class Gear : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private bool _isCanDrag;
        public bool bigGear;
        public Vector3 startPos;
        public Rigidbody2D rb;
        
        public bool mainGear;
        public List<SliderGearLoading> sliders;
        
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
            startPos = transform.position;
            rb = GetComponent<Rigidbody2D>();
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
                rb.simulated = true;
                rb.freezeRotation = true;
                
                OnStartMoving?.Invoke(this);
                _startPos = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.localPosition;
                _isBeingHeld = true;
            }
        }

        public void OnMouseUp()
        {
            if (Input.GetMouseButtonUp(0) && _isCanDrag)
            {
                rb.freezeRotation = false;
                
                OnStopMoving?.Invoke();
                _isBeingHeld = false;
            }
        }

        public void CheckGear(bool status)
        {
            if (mainGear)
            {
                foreach (var slider in sliders)
                {
                    slider.SetIsCanLoading(status);
                }
            }
        }
    }
}
