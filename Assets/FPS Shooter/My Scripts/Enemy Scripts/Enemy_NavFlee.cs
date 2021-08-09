using UnityEngine;
using System.Collections;

namespace S3
{
    public class Enemy_NavFlee : MonoBehaviour
    {
        public bool isFleeing;
        public Transform fleeTarget;
        public float fleeRange = 25;
        private Enemy_Master enemyMaster;
        private UnityEngine.AI.NavMeshAgent myNavMeshAgent;
        private UnityEngine.AI.NavMeshHit navHit;
        private Transform myTransform;
        private Vector3 runPosition;
        private Vector3 directionToPlayer;
        private float checkRate;
        private float nextCheck;

        void OnEnable()
        {
            SetInitialRefrences();
            enemyMaster.EventEnemyDie += DisableThis;
            enemyMaster.EventEnemySetNavTarget += SetFleeTarget;
            enemyMaster.EventEnemyHealthLow += IShouldFlee;
            enemyMaster.EventEnemyHealthRecovered += IShouldStopFleeing;
        }

        void OnDisable()
        {
            enemyMaster.EventEnemyDie -= DisableThis;
            enemyMaster.EventEnemySetNavTarget -= SetFleeTarget;
            enemyMaster.EventEnemyHealthLow -= IShouldFlee;
            enemyMaster.EventEnemyHealthRecovered -= IShouldStopFleeing;
        }

        // Update is called once per frame
        void Update()
        {
            if(Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;
                CheckIfIShouldFlee();
            }
        }

        void SetInitialRefrences()
        {
            enemyMaster = GetComponent<Enemy_Master>();
            myTransform = transform;
            if (GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
                myNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            checkRate = Random.Range(0.3f, 0.4f);
        }

        void SetFleeTarget(Transform target)
        {
            fleeTarget = target;
        }

        void IShouldFlee()
        {
            isFleeing = true;

            if (GetComponent<Enemy_NavPursue>() != null)
                GetComponent<Enemy_NavPursue>().enabled = false;
        }

        void IShouldStopFleeing()
        {
            isFleeing = false;

            if (GetComponent<Enemy_NavPursue>() != null)
                GetComponent<Enemy_NavPursue>().enabled = true;
        }

        void CheckIfIShouldFlee()
        {
            if (isFleeing)
            {
                if(fleeTarget != null && !enemyMaster.isOnRoute && !enemyMaster.isNavPaused)
                {
                    if(FleeTarget(out runPosition) && Vector3.Distance(myTransform.position,fleeTarget.position) < fleeRange)
                    {
                        myNavMeshAgent.SetDestination(runPosition);
                        enemyMaster.CallEventEnemyWalking();
                        enemyMaster.isOnRoute = true; //The enemy is going somewhere
                    }
                }
            }
        }

        bool FleeTarget(out Vector3 result)
        {
            //This method finds a far place on the navmesh and returns it
            directionToPlayer = myTransform.position - fleeTarget.position;
            Vector3 checkPos = myTransform.position + directionToPlayer;
            //Checkpos is both away from the player an away from the enemy

            if(UnityEngine.AI.NavMesh.SamplePosition(checkPos,out navHit,1.0f, UnityEngine.AI.NavMesh.AllAreas))
            {
                // out navhit means the place i can navigate there
                result = navHit.position;
                return true;
            }
            else
            {
                result = myTransform.position;
                return false;
            }
        }

        void DisableThis()
        {
            this.enabled = false;
        }
    }
}

