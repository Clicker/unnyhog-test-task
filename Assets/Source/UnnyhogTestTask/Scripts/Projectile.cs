using UnityEngine;
using UnnyhogTestTask.Core;

namespace UnnyhogTestTask.Scripts
{
    public class Projectile : MonoBehaviour
    {
        public float Speed;
        public float Damage;
        public float Range;

        public GameObject Geometry;
        public AudioSource AudioSource;
        public AudioClip HitAudioClip;
        
        private Vector3 _startPosition;
        private Collider _collider;
        private bool _isActive;

        private void Start()
        {
            _startPosition = transform.position;
            _collider = GetComponent<Collider>();
            _isActive = true;
        }

        private void FixedUpdate()
        {
            if (!_isActive)
            {
                return;
            }

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
            Geometry.SetActive(false);
            _collider.enabled = false;
            _isActive = false;

            AudioSource.Stop();
            AudioSource.clip = HitAudioClip;
            AudioSource.loop = false;
            AudioSource.Play();
            
            Destroy(gameObject, HitAudioClip.length);
        }
    }
}
