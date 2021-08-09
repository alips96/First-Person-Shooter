using UnityEngine;
using System.Collections;

namespace S3
{
    public class Melee_Swing : MonoBehaviour
    {
        private Melee_Master meleeMaster;
        public Collider myCollider;
        public Rigidbody myRigidbody;
        public Animator myAnimator;

        void OnEnable()
        {
            SetInitialRefrences();
            meleeMaster.EventPlayerInput += MeleeAttackActions;
        }

        void OnDisable()
        {
            meleeMaster.EventPlayerInput -= MeleeAttackActions;
        }

        void SetInitialRefrences()
        {
            meleeMaster = GetComponent<Melee_Master>();
        }

        void MeleeAttackActions() 
        {
            myCollider.enabled = true;
            myRigidbody.isKinematic = false;
            myAnimator.SetTrigger("Attack");
        }

        void MeleeAttackComplete() //Called by animation
        {
            myCollider.enabled = false;
            myRigidbody.isKinematic = true;
            meleeMaster.isInUse = false;
        }
    }
}

