using UnityEngine;
using System.Collections;

namespace S3
{
    public class Enemy_RagdollActivation : MonoBehaviour
    {
        private Enemy_Master enemyMaster;
        private Collider myCollider;
        private Rigidbody myRigidbody;
        
        void OnEnable()
        {
            SetInitialRefrences();
            enemyMaster.EventEnemyDie += ActivateRagdoll;
        }

        void OnDisable()
        {
            enemyMaster.EventEnemyDie -= ActivateRagdoll;
        }

        void SetInitialRefrences()
        {
            enemyMaster = transform.root.GetComponent<Enemy_Master>();
            //We are gonna attach this to each part of the ragdoll model

            if (GetComponent<Collider>() != null)
                myCollider = GetComponent<Collider>();
            if (GetComponent<Rigidbody>() != null)
                myRigidbody = GetComponent<Rigidbody>();
        }

        void ActivateRagdoll()
        {
            if(myCollider != null)
            {
                myCollider.isTrigger = false;
                myCollider.enabled = true;
            }

            if(myRigidbody != null)
            {
                myRigidbody.isKinematic = false;
                myRigidbody.useGravity = true;
            }
        }
    }

}

