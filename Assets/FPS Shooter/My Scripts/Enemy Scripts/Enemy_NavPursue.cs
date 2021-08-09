using UnityEngine;
using System.Collections;

namespace S3
{
    public class Enemy_NavPursue : MonoBehaviour
    {
        private Enemy_Master enemyMaster;
        private UnityEngine.AI.NavMeshAgent myNavmeshAgent;
        private float checkRate;
        private float nextCheck;

        void OnEnable()
        {
            SetInitalRefrences();
            enemyMaster.EventEnemyDie += DisableThis;
        }

        void OnDisable()
        {
            enemyMaster.EventEnemyDie -= DisableThis;
        }

        // Update is called once per frame
        void Update()
        {
            if(Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;
                TryToChaseTarget();
            }
        }

        void SetInitalRefrences()
        {
            enemyMaster = GetComponent<Enemy_Master>();

            if(GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
            {
                myNavmeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            }

            checkRate = Random.Range(0.1f, 0.2f);
        }

        void TryToChaseTarget()
        {
            if(enemyMaster.myTarget != null && myNavmeshAgent != null && !enemyMaster.isNavPaused)
            {
                // isNavPaused occures when enemy is injured and will not chase player for a while
                myNavmeshAgent.SetDestination(enemyMaster.myTarget.position);

                if(myNavmeshAgent.remainingDistance > myNavmeshAgent.stoppingDistance)
                {
                    // If enemy if far away from the player
                    enemyMaster.CallEventEnemyWalking();
                    enemyMaster.isOnRoute = true; // The enemy is going somewhere! 
                }

            }
        }

        void DisableThis()
        {
            if (myNavmeshAgent != null)
                myNavmeshAgent.enabled = false;

            this.enabled = false;
        }
    }
}

