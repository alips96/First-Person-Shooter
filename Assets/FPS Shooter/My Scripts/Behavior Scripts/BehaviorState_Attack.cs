using UnityEngine;
using System.Collections;

namespace S3
{

	public class BehaviorState_Attack : MonoBehaviour 
	{
        private Player_Master playerMaster;
        private Vector3 lookAtPoint;
        public Transform playerTransform;
        private UnityEngine.AI.NavMeshAgent myNavMeshAgent;
        private Behavior_Master behaviorMaster;
        private Behavior_Pattern behaviorPattern;
        private float attackRate = 0.3f;
        private float nextAttack;

        void OnEnable()
        {
            SetInitialRefrences();
        }

        void SetInitialRefrences()
        {
            behaviorMaster = GetComponent<Behavior_Master>();
            playerMaster = GameObject.Find("Player").GetComponent<Player_Master>();
            myNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            behaviorPattern = GetComponent<Behavior_Pattern>();
        }

        public void AttackTarget() //Called by animation
        {
            if (IfThePlayerIsInfront())
            {
                //Debug.Log("yes");
                if(playerMaster != null)    
                playerMaster.CallEventPlayerHealthDeduction(10);
            }

          //      behaviorMaster.CallEventIdleAnimation();
            //Debug.Log("hello");         
        }

        bool IfThePlayerIsInfront()
        {
            if(playerTransform != null)
            {
                if(Time.time > nextAttack)
                {
                    // Debug.Log("heyy");
                    nextAttack = Time.time + attackRate;
                    Vector3 heading = playerTransform.position - transform.position;

                    if (Vector3.Dot(heading, transform.forward) > 0.5f && Vector3.Dot(heading, transform.forward) < 3)
                    {
                        // Debug.Log(Vector3.Dot(heading, transform.forward).ToString());
                        return true;
                    }
                }
            }


            return false;
        }

        public void PrepareToAttack()
        {
            behaviorPattern.isOnRoute = false;
            behaviorMaster.CallEventAttackAnimation();
            //myNavMeshAgent.Stop();
        }


    }
}


