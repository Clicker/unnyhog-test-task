using UnityEngine;
using UnnyhogTestTask.Core;

namespace UnnyhogTestTask.Scripts
{
    public class Projectile : MonoBehaviour
    {
        public float Speed;
        public float Damage;
        public float Range;
        
        private Vector3 _startPosition;

        private void Start()
        {
            _startPosition = transform.position;
        }

        private void FixedUpdate()
        {
            if (Vector3.Distance(_startPosition, transform.position) > Range)
            {
                Destroy();
                return;
            }

            var movement = transform.forward * Speed * Time.deltaTime;
            transform.position += movement;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(UnityTags.Obstacle))
            {
                Destroy();
            }
            else if (other.CompareTag(UnityTags.Player))
            {
                Destroy();
            }
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
