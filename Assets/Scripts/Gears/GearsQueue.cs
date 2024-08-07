using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Items.Gears
{
    public class GearsQueue : MonoBehaviour
    {
        [SerializeField] private List<PlaceToGear> _placeToGears;

        private void OnEnable()
        {
            MyButton.OnButtonUp += StopAll;
            MyButton.OnButtonDown += CheckRotation;
        }
        
        private void OnDisable()
        {
            MyButton.OnButtonUp -= StopAll;
            MyButton.OnButtonDown -= CheckRotation;
        }


        private void Awake()
        {
            foreach (var place in _placeToGears)
            {
                place.GearsQueue = this;
            }
        }

        public void StopAll()
        {
            foreach (var place in _placeToGears)
            {
                if (place.GearInPlace != null)
                {
                    place.GearInPlace.StopGear();
                }
            }
        }

        public void CheckRotation()
        {
            for (int i = 0; i < _placeToGears.Count; i++)
            {
                if (_placeToGears[i].GearInPlace != null)
                {
                    _placeToGears[i].GearInPlace.IsRight = i % 2 == 0;

                    _placeToGears[i].GearInPlace.RotateGear();
                }
                else
                {
                    return;
                }
            }
        }
    }
}