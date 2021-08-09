using UnityEngine;
using System.Collections;

namespace S3
{

	public class Behavior_Pattern : MonoBehaviour 
	{
        private Behavior_Master behaviorMaster;
        private BehaviorState_Attack AttackBehavior;
        private BehaviorState_Pursue PursueBehavior;
        private BehaviorState_Patrol PatrolBehavior;
        //private BehaviorState_Struck StruckBehavior;

        private bool strucked = false;

        private Collider[] playerColliders;
        public float sightRange = 40;
        public LayerMask myEnemyLayers;
        private Vector3 heading;
        private float dotProd;
        public Transform head;
        private Vector3 lookAtPoint;
        public LayerMask sightLayers;
        public string[] myEnemyTags;
        private UnityEngine.AI.NavMeshAgent myNavMeshAgent;
        public bool isOnRoute;
        private float attackRange = 3.5f;

        void Awake()
		{
            SetupUpStateRefrences();
            SetInitialReferences();
		}

		void OnDisable()
		{
			
		}
	
		void Update () 
		{
            if (!strucked)
            {
                if (!ThereIsTarget())
                {
                    StopAllCoroutines();
                    PatrolBehavior.Patrol();
                }
            }
            //else
            //{
            //StruckBehavior.Struck();
            //}
        }

        void SetInitialReferences()
		{
            myNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            behaviorMaster = GetComponent<Behavior_Master>();

            if (behaviorMaster != null)
                behaviorMaster.CallEventIdleAnimation();
		}

        void SetupUpStateRefrences()
        {
            AttackBehavior = GetComponent<BehaviorState_Attack>();
            PursueBehavior = GetComponent<BehaviorState_Pursue>();
            PatrolBehavior = GetComponent<BehaviorState_Patrol>();
        }

        bool ThereIsTarget()
        {
            //Check minimum range
            playerColliders = Physics.OverlapSphere(transform.position, sightRange / 3, myEnemyLayers);

            if (playerColliders.Length > 0)
            {
                GetAlerted(playerColliders[0].transform);
                return true;
            }
 
                //Check Max Range
                playerColliders = Physics.OverlapSphere(transform.position, sightRange, myEnemyLayers);

                foreach (Collider col in playerColliders)
                {
                    RaycastHit hit;
                    VisibilityCalculations(col.transform);

                    if (Physics.Linecast(head.position, lookAtPoint, out hit, sightLayers))
                    {
                        foreach (string tag in myEnemyTags)
                        {
                            if (hit.transform.CompareTag(tag))
                            {
                                if (dotProd > 0)
                                {
                                    GetAlerted(col.transform);
                                    return true;
                                }
                            }
                        }
                    }
                }
            playerColliders = null;
            return false;
        }


        void VisibilityCalculations(Transform potentialTarget)
        {
            lookAtPoint = new Vector3(potentialTarget.position.x, potentialTarget.position.y + 0.4f, potentialTarget.position.z);
            heading = lookAtPoint - transform.position;
            dotProd = Vector3.Dot(heading, transform.forward);
        }

        void GetAlerted(Transform target)
        {
            head.LookAt(target);

            if (Vector3.Distance(transform.position, target.position) < attackRange)
            {               
                AttackBehavior.PrepareToAttack();
            }
            else
                PursueBehavior.Pursue(target);
        }

    }
}


