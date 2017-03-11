using System;
using UnityEngine;

namespace UnnyhogTestTask.Scripts
{
    public class PlayerShooting : MonoBehaviour
    {
        private const string ShootingAnimationTriggerName = "Shoot";

        public Animator Animator;
        public Transform GunPoint;
        public GameObject ProjectilePrefab;
        public float TimeBetweenShots = 1f;

        private float _timer;

        private void Update()
        {
            _timer += Time.deltaTime;

            if (Input.GetButton("Fire1") && _timer >= TimeBetweenShots && Math.Abs(Time.timeScale) > float.Epsilon)
            {
                Shoot();
            }
        }


        private void Shoot()
        {
            _timer = 0f;

            Animator.SetTrigger(ShootingAnimationTriggerName);
        }

        /// <summary>
        /// Method is called by animation event
        /// </summary>
        private void CreateProjectile()
        {
            Instantiate(ProjectilePrefab, GunPoint.position, transform.rotation);
        }
    }
}
