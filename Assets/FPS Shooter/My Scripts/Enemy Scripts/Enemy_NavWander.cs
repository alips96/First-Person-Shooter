using UnityEngine;
using System.Collections;

namespace S3
{
    public class Enemy_NavWander : MonoBehaviour
    {
        private Enemy_Master enemyMaster;
        private UnityEngine.AI.NavMeshAgent myNavmeshAgent;
        private float checkRate;
        private float nextCheck;
        private Transform myTransform;
        private float wanderRange = 10;
        private UnityEngine.AI.NavMeshHit navHit;
        private Vector3 wanderTarget;

        void OnEnable()
        {
            SetInitialRefrences();
            enemyMaster.EventEnemyDie += DisableThis;
        }

        void OnDisable()
        {
            enemyMaster.EventEnemyDie -= DisableThis;
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;
                CheckIfIShouldWander();
            }
        }

        void SetInitialRefrences()
        {
            enemyMaster = GetComponent<Enemy_Master>();

            if (GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
            {
                myNavmeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            }

            checkRate = Random.Range(0.3f, 0.4f);
            myTransform = transform;
        }

        void CheckIfIShouldWander()
        {
            if(enemyMaster.myTarget == null && !enemyMaster.isOnRoute && !enemyMaster.isNavPaused)
            {
                if(RandomWanderTarget(myTransform.position,wanderRange,out wanderTarget))
                {
                    myNavmeshAgent.SetDestination(wanderTarget);
                    enemyMaster.isOnRoute = true;
                    enemyMaster.CallEventEnemyWalking();
                }
            }
        }

        bool RandomWanderTarget(Vector3 center,float range,out Vector3 result)
        {
            //This method returns a bool and an output. if result was not output then we couldnt use it
            //Navmesh.sampleposition also does this
            Vector3 randomPoint = center + Random.insideUnitSphere * wanderRange;
            if(UnityEngine.AI.NavMesh.SamplePosition(randomPoint,out navHit, 1.0f, UnityEngine.AI.NavMesh.AllAreas))
            {
                //Why did we use that? because we want to find out a place close to randomPosition to be in the navmesh territory 
                // to be the destination 
                result = navHit.position;
                return true;
            }
            else
            {
                result = center;
                return false;
            }
        }

        void DisableThis()
        {
            this.enabled = false;
        }
    }
}

