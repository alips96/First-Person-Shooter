using UnityEngine;
using System.Collections;

namespace S3
{
    public class Destructible_Sound : MonoBehaviour
    {
        private Destructible_Master destructibleMaster;
        public float explosionVolume = 0.5f;
        public AudioClip explodingSound;

        void OnEnable()
        {
            SetInitialRefrences();
            destructibleMaster.EventDestroyMe += ExplosionSound;
        }

        void OnDisable()
        {
            destructibleMaster.EventDestroyMe -= ExplosionSound;
        }

        void SetInitialRefrences()
        {
            destructibleMaster = GetComponent<Destructible_Master>();
        }

        void ExplosionSound()
        {
            if (explodingSound != null)
                AudioSource.PlayClipAtPoint(explodingSound, transform.position, explosionVolume);
        }
    }
}

