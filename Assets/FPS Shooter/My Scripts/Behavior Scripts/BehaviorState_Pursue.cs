using UnityEngine;
using System.Collections;

namespace S3
{

	public class BehaviorState_Pursue : MonoBehaviour 
	{
        private UnityEngine.AI.NavMeshAgent myNavMeshAgent;
        private Behavior_Master behaviorMaster;
        private Behavior_Pattern behaviorPattern;

        void OnEnable()
        {
            SetInitialRefrences();
        }

        void OnDisable()
        {

        }

        void SetInitialRefrences()
        {
            behaviorMaster = GetComponent<Behavior_Master>();
            myNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            behaviorPattern = GetComponent<Behavior_Pattern>();
        }

        public void Pursue(Transform target)
        {
            myNavMeshAgent.SetDestination(target.position);
            myNavMeshAgent.Resume();
            behaviorMaster.CallEventWalkAnimation();
            behaviorPattern.isOnRoute = true;
        }
	}
}


