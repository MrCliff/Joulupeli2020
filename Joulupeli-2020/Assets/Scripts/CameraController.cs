using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Controls the camera :D
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Transform targetToFollow;

        [SerializeField]
        [Min(0)]
        private float speed = 100;

        private void FixedUpdate()
        {
            if (targetToFollow == null)
            {
                return;
            }

            Vector3 newCameraPosition = Vector3.Lerp(transform.position, targetToFollow.position, speed * Time.fixedDeltaTime);
            newCameraPosition.z = transform.position.z;
            transform.position = newCameraPosition;
        }
    }
}
