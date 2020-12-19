using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Controls the camera. :D
    /// 
    /// This was temporary controller until the Cinemachine package was
    /// installed.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D targetToFollow;

        [SerializeField]
        private Vector2 movementBounds;

        [SerializeField]
        [Min(0)]
        private float speed = 100;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, movementBounds);

            Gizmos.color = Color.yellow;
            Rect boundRectangle = new Rect((Vector2)transform.position - (movementBounds / 2), movementBounds);
            Gizmos.DrawWireCube(boundRectangle.center, boundRectangle.size);
        }

        private void Start()
        {
            if (targetToFollow == null)
            {
                Debug.LogWarning("No target set for " + nameof(CameraController));
                return;
            }
        }

        private void FixedUpdate()
        {
            if (targetToFollow == null)
            {
                return;
            }

            Rect boundRectangle = new Rect(targetToFollow.position - (movementBounds / 2), movementBounds);
            Vector2 targetVelocity = targetToFollow.velocity;
            Vector2 unclampedTargetPosition = targetToFollow.position + targetVelocity;
            //Vector2 targetPosition = Rect.NormalizedToPoint(boundRectangle, Rect.PointToNormalized(boundRectangle, unclampedTargetPosition));

            Vector3 newCameraPosition = Vector3.Lerp(transform.position, unclampedTargetPosition, speed * Time.fixedDeltaTime);
            newCameraPosition.x = Mathf.Clamp(newCameraPosition.x, boundRectangle.xMin, boundRectangle.xMax);
            newCameraPosition.y = Mathf.Clamp(newCameraPosition.y, boundRectangle.yMin, boundRectangle.yMax);
            newCameraPosition.z = transform.position.z;
            transform.position = newCameraPosition;
        }
    }
}
