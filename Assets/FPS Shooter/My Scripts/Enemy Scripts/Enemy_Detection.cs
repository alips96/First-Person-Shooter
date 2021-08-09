//This script is very fun! I can just say that
using UnityEngine;
using System.Collections;

namespace S3
{
    public class Enemy_Detection : MonoBehaviour
    {
        private Enemy_Master enemyMaster;
        private Transform myTransform;
        private float CheckRate;
        private float nextCheck;
        private float detectRadius = 80;
        private RaycastHit hit;
        public Transform head;
        public LayerMask playerLayer;
        public LayerMask sightLayer;
        


        void OnEnable()
        {
            SetInitalRefrences();
            enemyMaster.EventEnemyDie += DisableThis; // We disable the script after enemy dies
        }

        void OnDisable()
        {
            enemyMaster.EventEnemyDie -= DisableThis;
        }

        // Update is called once per frame
        void Update()
        {
            CarryOutDetection();
        }

        void SetInitalRefrences()
        {
            enemyMaster = GetComponent<Enemy_Master>();
            myTransform = transform;

            if (head == null)
                head = myTransform;

            CheckRate = Random.Range(0.8f, 1.2f); //Remember overlapshere is expensive!
        }

        void CarryOutDetection()
        {
            if(Time.time > nextCheck)
            {
                nextCheck = Time.time + CheckRate;

                Collider[] colliders = Physics.OverlapSphere(myTransform.position, detectRadius, playerLayer);
                if(colliders.Length > 0)
                {
                    foreach (Collider potentialTargetCollider in colliders)
                    {
                        if (potentialTargetCollider.CompareTag(GameManager_Refrences._playerTag))
                        {
                            if (CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                            {
                                break;
                            }
                        }
                    }
                }
                else
                {
                    enemyMaster.CallEventEnemyLostTarget();
                }
        
            }
        }

        bool CanPotentialTargetBeSeen(Transform potentialTarget)
        {
            if(Physics.Linecast(head.position,potentialTarget.position,out hit, sightLayer))
            {
                if(hit.transform == potentialTarget)
                {
                    enemyMaster.CallEnemySetNavTarget(potentialTarget);
                    return true;
                }
                else
                {
                    enemyMaster.CallEventEnemyLostTarget();
                    return false;
                }
            }
            else
            {
                enemyMaster.CallEventEnemyLostTarget();
                return false;
            }
        }

        void DisableThis()
        {
            this.enabled = false; //It disables the script
        }
    }
}

