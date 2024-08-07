using System;
using Items.Gears;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class MyButton : Button
    {
        public static event Action OnButtonDown;
        public static event Action OnButtonUp;
        
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            OnButtonDown?.Invoke();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            OnButtonUp?.Invoke();
        }
    }
}