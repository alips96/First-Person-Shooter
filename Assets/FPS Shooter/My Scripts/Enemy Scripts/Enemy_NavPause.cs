using UnityEngine;
using System.Collections;

namespace S3
{
    public class Enemy_NavPause : MonoBehaviour
    {
        private Enemy_Master enemyMaster;
        private UnityEngine.AI.NavMeshAgent myNavmeshAgent;
        private float pauseTime = 1;

        void OnEnable()
        {
            SetInitialRefrences();
            enemyMaster.EventEnemyDie += DisableThis;
            enemyMaster.EventEnemyDeductHealth += PauseNavMeshAgent;
        }

        void OnDisable()
        {
            enemyMaster.EventEnemyDie -= DisableThis;
            enemyMaster.EventEnemyDeductHealth -= PauseNavMeshAgent;
        }

        void SetInitialRefrences()
        {
            enemyMaster = GetComponent<Enemy_Master>();

            if (GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
            {
                myNavmeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            }
        }

        void PauseNavMeshAgent(int dummy) // I just pass it because ther should be something passed in enemy_master
        {
            if(myNavmeshAgent != null)
            {
                if (myNavmeshAgent.enabled)
                {
                    myNavmeshAgent.ResetPath();
                    //When the path is cleared, the agent will not start looking for a new path until SetDestination is called.
                    enemyMaster.isNavPaused = true;
                    StartCoroutine(RestartNavMeshAgent());
                }
            }
        }

        IEnumerator RestartNavMeshAgent()
        {
            yield return new WaitForSeconds(pauseTime);
            enemyMaster.isNavPaused = false;
        }

        void DisableThis()
        {
            StopAllCoroutines();
        }
    }
}

