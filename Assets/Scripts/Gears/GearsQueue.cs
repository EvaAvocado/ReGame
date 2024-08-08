using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Items.Gears
{
    public class GearsQueue : MonoBehaviour
    {
        public List<PlaceToGear> gearList1;
        public List<PlaceToGear> gearList2;
        public List<PlaceToGear> gearList3;
        

        private void OnEnable()
        {
            MyButton.OnButtonUp += StopAll;
            MyButton.OnButtonDown += OnButtonDown;
        }
        
        private void OnDisable()
        {
            MyButton.OnButtonUp -= StopAll;
            MyButton.OnButtonDown -= OnButtonDown;
        }


        private void Awake()
        {
            foreach (var place in gearList1)
            {
                place.GearsQueue = this;
            }
            
            foreach (var place in gearList2)
            {
                place.GearsQueue = this;
            }
            
            foreach (var place in gearList3)
            {
                place.GearsQueue = this;
            }
        }

        public void StopAll()
        {
            foreach (var place in gearList1)
            {
                if (place.GearInPlace != null)
                {
                    place.GearInPlace.StopGear();
                    place.GearInPlace.CheckGear(false);
                }
            }
            
            foreach (var place in gearList2)
            {
                if (place.GearInPlace != null)
                {
                    place.GearInPlace.StopGear();
                    place.GearInPlace.CheckGear(false);
                }
            }
            
            foreach (var place in gearList3)
            {
                if (place.GearInPlace != null)
                {
                    place.GearInPlace.StopGear();
                    place.GearInPlace.CheckGear(false);
                }
            }
        }

        private void OnButtonDown()
        {
            CheckRotation(gearList1);
            CheckRotation(gearList2);
            CheckRotation(gearList3);
        }

        private void CheckRotation(List<PlaceToGear> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].GearInPlace != null && list[i].GearInPlace.bigGear == list[i].bigGear)
                {
                    list[i].GearInPlace.IsRight = i % 2 == 0;
                    list[i].GearInPlace.RotateGear();
                    list[i].GearInPlace.CheckGear(true);
                }
                else
                {
                    return;
                }
            }
        }
    }
}