using UnityEngine;
using System.Collections;

namespace S3
{

	public class BehaviorState_Patrol : MonoBehaviour 
	{
        private UnityEngine.AI.NavMeshAgent myNavMeshAgent;
        private Behavior_Master behaviorMaster;
        private Behavior_Pattern behaviorPattern;
        private Vector3 placeToPatrol;
        private Vector3 result;

		void Start()
		{
            SetInitialReferences();
		}

		void SetInitialReferences()
		{
            behaviorMaster = GetComponent<Behavior_Master>();
            myNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            behaviorPattern = GetComponent<Behavior_Pattern>();
		}

        public void Patrol()
        {
            //StopWalking();

            if (!behaviorPattern.isOnRoute)
            {
                //Debug.Log("Entered patrol and is not OnRoute");
                if (FoundASuitablePlaceToPatrol())
                {
                    //Debug.Log("FoundASuitablePlaceToPatrol");
                    myNavMeshAgent.SetDestination(result);
                    KeepWalking();

                    if (HaveIReachedDestination())
                    {
                        //Debug.Log("HaveIReachedDestination");
                        StopWalking();
                        return;
                    }
                }
            }
        }

        bool FoundASuitablePlaceToPatrol()
        {
            placeToPatrol = transform.position + Random.insideUnitSphere * 20;
            UnityEngine.AI.NavMeshHit navhit;

            if (UnityEngine.AI.NavMesh.SamplePosition(transform.position, out navhit, 3.0f, UnityEngine.AI.NavMesh.AllAreas))
            {
                result = navhit.position;
                return true;
            }
            else
            {
                result = transform.position;
                return false;
            }
        }

        void KeepWalking()
        {
            myNavMeshAgent.Resume();
            behaviorPattern.isOnRoute = true;
            behaviorMaster.CallEventWalkAnimation();
        }

        void StopWalking()
        {
            myNavMeshAgent.Stop();
            behaviorPattern.isOnRoute = false;
            behaviorMaster.CallEventIdleAnimation();
        }

        bool HaveIReachedDestination()
        {
            if(myNavMeshAgent.remainingDistance < myNavMeshAgent.stoppingDistance)
            {
                return true;
            }
            return false;
        }
	}
}


