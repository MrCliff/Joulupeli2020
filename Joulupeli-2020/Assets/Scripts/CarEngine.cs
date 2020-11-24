using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class CarEngine : MonoBehaviour
    {
        [SerializeField]
        private WheelJoint2D frontWheel;

        [SerializeField]
        private WheelJoint2D rearWheel;

        [SerializeField]
        [Range(0, 1)]
        private float moveThreshold = 0;

        [SerializeField]
        [Min(0)]
        private float motorSpeed = 1000;

        [SerializeField]
        private float jumpForceMagnitude = 100;

        private void Awake()
        {
            if (frontWheel == null)
            {
                Debug.LogError("No front wheel attached to car engine.");
            }
            if (rearWheel == null)
            {
                Debug.LogError("No rear wheel attached to car engine.");
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            int directionModifier = 0;
            if (direction.x > moveThreshold) directionModifier = -1;
            if (direction.x < -moveThreshold) directionModifier = 1;

            JointMotor2D frontWheelMotor = frontWheel.motor;
            frontWheelMotor.motorSpeed = directionModifier * motorSpeed;
            frontWheel.motor = frontWheelMotor;
            frontWheel.useMotor = directionModifier != 0;
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            Vector2 jumpForce = new Vector2(0, jumpForceMagnitude);

            frontWheel.attachedRigidbody.AddForce(jumpForce, ForceMode2D.Impulse);
            rearWheel.attachedRigidbody.AddForce(jumpForce, ForceMode2D.Impulse);
        }
    }
}
