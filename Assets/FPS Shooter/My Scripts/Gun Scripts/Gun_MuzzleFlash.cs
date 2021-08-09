using UnityEngine;
using System.Collections;

namespace S3
{
    public class Gun_MuzzleFlash : MonoBehaviour
    {
        public ParticleSystem muzzleFlash;
        private Gun_Master gunMaster;

        void OnEnable()
        {
            SetInitialRefrences();
            gunMaster.EventPlayerInput += PlayMuzzleFlash;
        }

        void OnDisable()
        {
            gunMaster.EventPlayerInput -= PlayMuzzleFlash;
        }

        void SetInitialRefrences()
        {
            gunMaster = GetComponent<Gun_Master>();
        }

        void PlayMuzzleFlash()
        {
            if (muzzleFlash != null)
                muzzleFlash.Play();
        }
    }
}

