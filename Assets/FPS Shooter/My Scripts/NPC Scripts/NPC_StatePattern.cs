using UnityEngine;
using System.Collections;

namespace S3
{
    public class NPC_StatePattern : MonoBehaviour
    {
        //Used in decision making
        private float checkRate = 0.1f;
        private float nextCheck;
        public float sightRange = 40;
        public float detectBehindRange = 5;
        public float meleeAttackRange = 4;
        public float meleeAttackDamage = 10;
        public float rangeAttackRange = 35; //for npc to stop walking toward the player and stop an start shooting
        public float rangeAttackDamage = 5;
        public float rangeAttackSpread = 0.5f; //randomness to the shooting
        public float attackRange = 0.4f;
        public float nextAttack;
        public float fleeRange = 25;
        public float offset = 0.4f;
        public int requiredDetectionCount = 15; //When the enemy sees the player he does not start shooting at the moment and waits in the alert stay

        public bool hasRangeAttack;
        public bool hasMeleeAttack;
        public bool isMeleeAttacking;

        public Transform myFollowTarget;
        [HideInInspector]
        public Transform pursueTarget;
        [HideInInspector]
        public Vector3 locationOfInterest;
        [HideInInspector]
        public Vector3 wanderTarget;
        [HideInInspector]
        public Transform myAttacker;

        //Used for sight
        public LayerMask sightLayer;
        public LayerMask myEnemyLayers;
        public LayerMask myFriendlyLayers;//We can mark so we dont need arrays for layermask
        public string[] myEnemyTags;
        public string[] myFriendlyTags;

        //References
        public Transform[] waypionts;
        public Transform head;
        public MeshRenderer meshRendererFlag;
        public GameObject rangeWeapon; //the weapon that npc carries
        public NPC_Master npcMaster;
        [HideInInspector]
        public UnityEngine.AI.NavMeshAgent myNavMeshAgent;

        //Used for state AI
        public NPCState_Interface currentState;
        public NPCState_Interface capturedState;
        public NPCState_Patrol patrolState;
        public NPCState_Alert alertState;
        public NPCState_Pursue pursueState;
        public NPCState_MeleeAttack meleeAttackState;
        public NPCState_RangeAttack rangeAttackState;
        public NPCState_Flee fleeState;
        public NPCState_Struck struckState;
        public NPCState_InvestigateHarm investigateHarmState;
        public NPCState_Follow followState;

        void Awake()
        {
            SetupUpStateRefrences();
            SetInitialRefrences();
            npcMaster.EventNpcLowHealth += ActivateFleeState;
            npcMaster.EventNpcHealthRecovered += ActivatePatrolState;
            npcMaster.EventNpcDeductHealth += ActivateStruckState;
        }

        void Start()
        {
            SetInitialRefrences(); //For assurance
        }

        void OnDisable()
        {
            npcMaster.EventNpcLowHealth -= ActivateFleeState;
            npcMaster.EventNpcHealthRecovered -= ActivatePatrolState;
            npcMaster.EventNpcDeductHealth -= ActivateStruckState;
            StopAllCoroutines(); //When the enemy dies we stop the possibility of errors from coRoutines
        }

        void Update()
        {
            CarryOutUpdateState();
        }

        void SetupUpStateRefrences()
        {
            patrolState = new NPCState_Patrol(this);
        }

        void SetInitialRefrences()
        {
            myNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            ActivatePatrolState();
        }

        void CarryOutUpdateState()
        {
            if(Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;
                currentState.UpdateState();
            }
        }

        void ActivatePatrolState()
        {
            currentState = patrolState;
        }
        
        void ActivateFleeState()
        {
            if(currentState == struckState) 
            {
                capturedState = fleeState; //when they struck they return to the flee state
                return;
            }

            currentState = fleeState;
        }

        void ActivateStruckState(int dummy)
        {
            StopAllCoroutines(); //it is used for the time when we shoot at the enemy for a couple of times 

            if(currentState != struckState)
            {
                capturedState = currentState; 
            }

            if (rangeWeapon != null) 
                rangeWeapon.SetActive(false);

            if (myNavMeshAgent.enabled)
                myNavMeshAgent.Stop();

            currentState = struckState;
            npcMaster.CallEventNpcStruckAnim();
            StartCoroutine(recoverFromStruckState());
        }  

        IEnumerator recoverFromStruckState()
        {
            yield return new WaitForSeconds(1.5f);
            npcMaster.CallEventNpcRecoveredAnim();

            if (rangeWeapon != null)
                rangeWeapon.SetActive(true);

            if (myNavMeshAgent.enabled)
                myNavMeshAgent.Resume();

            currentState = capturedState; //Return us to whatever state in
        }

        public void OnEnemyAttack() //Called by melee attack animation
        {
            if(pursueTarget != null)
            {
                if(Vector3.Distance(transform.position,pursueTarget.position) <= meleeAttackRange)
                {
                    Vector3 toOther = pursueTarget.position - transform.position;
                    if(Vector3.Dot(toOther,transform.forward) > 0.5f) //if the target is in front of the npc    
                    {
                        pursueTarget.SendMessage("CallEventPlayerHealthDeduction", meleeAttackDamage, SendMessageOptions.DontRequireReceiver); //for player
                        pursueTarget.SendMessage("ProcessDamage", meleeAttackDamage, SendMessageOptions.DontRequireReceiver); //For NPC s
                    }
                }
            }
            isMeleeAttacking = false;
        }

        public void SetMyAttacker(Transform attacker)
        {
            myAttacker = attacker;
        }

        public void Distract(Vector3 distractionPos)
        {
            locationOfInterest = distractionPos;

            if (currentState == patrolState)
                currentState = alertState;
        }
    }
}
