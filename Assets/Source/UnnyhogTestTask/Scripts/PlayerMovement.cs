using System;
using UnityEngine;
using UnnyhogTestTask.Core;

namespace UnnyhogTestTask.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        private const float CamRayLength = 100f;
        private const string RunningAnimationName = "IsRunning";

        public Animator Animator;
        public float Speed = 10;

        private Vector3 _movement;
        private Rigidbody _rigidbody;
        private int _groundMask;

        private void Awake()
        {
            _groundMask = LayerMask.GetMask(UnityLayers.Ground);
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Move(h, v);
            Turning();
            Animating(h, v);
        }

        private void Move(float h, float v)
        {
            _movement.Set(h, 0f, v);
            //_movement = transform.rotation * _movement;
            _movement = _movement.normalized * Speed * Time.deltaTime;
            _rigidbody.MovePosition(transform.position + _movement);
        }

        private void Turning()
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;

            if (Physics.Raycast(camRay, out floorHit, CamRayLength, _groundMask))
            {
                Vector3 playerToMouse = floorHit.point - transform.position;
                playerToMouse.y = 0f;

                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
                _rigidbody.MoveRotation(newRotation);
            }
        }

        private void Animating(float h, float v)
        {
            bool walking = Math.Abs(h) > float.Epsilon || Math.Abs(v) > float.Epsilon;
            Animator.SetBool(RunningAnimationName, walking);
        }
    }
}
