using System;
using System.Collections.Generic;
using Gears;
using UnityEngine;
using Utils;

namespace Items.Gears
{
    public class PlaceToGear : MonoBehaviour
    {
        [SerializeField] private LayerMask _item;
        [SerializeField] private bool _isNotInteractable;
        [SerializeField] private Gear _gearInPlace;
        public bool bigGear;

        private GearsQueue _gearsQueue;
        private bool _isCanInteract = true;

        public Gear GearInPlace => _gearInPlace;

        public GearsQueue GearsQueue
        {
            get => _gearsQueue;
            set => _gearsQueue = value;
        }

        private void Awake()
        {
            if (_isNotInteractable && _gearInPlace != null)
            {
                _gearInPlace.GetComponent<Rigidbody2D>().angularVelocity = 0;
                _gearInPlace.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                _gearInPlace.GetComponent<CircleCollider2D>().isTrigger = true;
                _gearInPlace.transform.position = transform.position;
                _isCanInteract = false;
            }
        }

        private void OnEnable()
        {
            //Gear.OnStopMoving += CheckPlace;
            Gear.OnStartMoving += DeleteGearOutPlace;
        }

        private void OnDisable()
        {
            //Gear.OnStopMoving -= CheckPlace;
            Gear.OnStartMoving -= DeleteGearOutPlace;
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                CheckPlace();
            }
        }

        private void CheckPlace()
        {
            if (_gearInPlace != null && _isCanInteract && !_isNotInteractable)
            {
                SetGearInPlace(_gearInPlace);
            }
        }

        private void SetGearInPlace(Gear gear)
        {
            gear.rb.bodyType = RigidbodyType2D.Static;
            gear.GetComponent<CircleCollider2D>().isTrigger = true;

            gear.transform.position = transform.position;

            _isCanInteract = false;
        }

        public void DeleteGearOutPlace(Gear gear)
        {
            if (gear == _gearInPlace)
            {
                gear.GetComponent<CircleCollider2D>().isTrigger = false;

                gear.StopGear();

                _gearInPlace = null;
                _isCanInteract = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_item.Contains(other.gameObject.layer))
            {
                if (other.TryGetComponent(out Gear gear))
                {
                    if (!_isNotInteractable && _isCanInteract)
                    {
                        _gearInPlace = gear;
                    }
                }
            }
        }

        /*private void OnTriggerStay2D(Collider2D other)
        {
            if (_item.Contains(other.gameObject.layer))
            {
                print(1);
                if (other.TryGetComponent(out Gear gear))
                {
                    print(2);
                    if (Input.GetMouseButtonUp(0))
                    {
                        print(3);
                        if (!_isNotInteractable && _isCanInteract)
                        {
                            print(4);
                            gear.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                            CheckPlace(gear);

                            _gearInPlace = gear;
                        }
                    }
                }
            }
        }*/

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_item.Contains(other.gameObject.layer))
            {
                if (other.TryGetComponent(out Gear gear))
                {
                    if (!_isNotInteractable && _isCanInteract)
                    {
                        gear.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        _gearInPlace = null;
                    }
                }
            }
        }
    }
}