using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    /// <summary>
    /// Handles touch input.
    /// </summary>
    [Obsolete]
    public class TouchInput : MonoBehaviour
    {
        [SerializeField]
        private GameObject forwardButton;

        [SerializeField]
        private GameObject backwardButton;

        [SerializeField]
        private GameObject jumpButton;

        private List<Button> buttons = new List<Button>();


        private void Awake()
        {
            GetComponentsInChildren(buttons);
            foreach (Button button in buttons)
            {
                button.onClick.RemoveListener(ActivateOnScreenInputs); // Make sure there are no duplicates.
                button.onClick.AddListener(ActivateOnScreenInputs);
            }
        }

        private void ActivateOnScreenInputs()
        {

        }
    }
}
