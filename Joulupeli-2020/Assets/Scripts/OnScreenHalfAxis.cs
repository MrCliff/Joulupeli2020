using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace Assets.Scripts
{
    /// <summary>
    /// On-Screen input that represents one half-axis (positive or negative)
    /// </summary>
    [AddComponentMenu("Input/On-Screen Half Axis")]
    public class OnScreenHalfAxis : OnScreenControl, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private bool isPositive = true;

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("Pointer up! " + eventData.button);
            SendValueToControl(0.0f);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Pointer down! " + eventData.button);
            SendValueToControl(isPositive ? 1.0f : -1.0f);
        }

        [InputControl(layout = "Button")]
        [SerializeField]
        private string m_ControlPath;

        protected override string controlPathInternal
        {
            get => m_ControlPath;
            set => m_ControlPath = value;
        }
    }
}
