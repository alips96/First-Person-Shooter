using UnityEngine;
using System.Collections;

namespace S3
{
    public class Gun_Animator : MonoBehaviour
    {
        private Gun_Master gunMaster;
        private Animator myAnimator;

        void OnEnable()
        {
            SetInitialRefrences();
            gunMaster.EventPlayerInput += PlayShootAnimation;
        }

        void OnDisable()
        {
            gunMaster.EventPlayerInput -= PlayShootAnimation;
        }

        void SetInitialRefrences()
        {
            gunMaster = GetComponent<Gun_Master>();

            if (GetComponent<Animator>() != null)
                myAnimator = GetComponent<Animator>();
        }

        void PlayShootAnimation()
        {
            if(myAnimator != null)
            {
                myAnimator.SetTrigger("Shoot");
            }
        }
    }
}

