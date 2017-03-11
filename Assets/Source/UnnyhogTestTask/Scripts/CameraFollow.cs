using UnityEngine;

namespace UnnyhogTestTask.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform Target;
        public float Smoothing = 15f;

        private Vector3 _offset;

        private void Start()
        {
            _offset = transform.position - Target.position;
        }

        private void LateUpdate()
        {
            Vector3 desiredPosition = Target.position + _offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Smoothing * Time.deltaTime);
        }
    }
}
