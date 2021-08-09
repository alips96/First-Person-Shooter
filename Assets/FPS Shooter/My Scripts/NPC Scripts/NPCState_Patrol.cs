using UnityEngine;
using System.Collections;

namespace S3
{
    public class NPCState_Patrol : NPCState_Interface
    {
        private readonly NPC_StatePattern npc;
        private int nextWayPoint;
        private Collider[] colliders;
        private Vector3 lookAtPoint;
        private Vector3 heading;
        private float dotProd;

        public NPCState_Patrol(NPC_StatePattern npcStatePattern)
        {
            npc = npcStatePattern; //Now we can control it through the state pattern
        }

        public void UpdateState()
        {
            Look();
            Patrol();
        }
        public void ToPatrolState() { }
        public void ToAlertState()
        {
            npc.currentState = npc.alertState;
        }
        public void ToPursueState() { }
        public void ToMeleeAttackState() { }
        public void ToRangeAttackState() { }

        void Look()
        {
            //Check medium range
            colliders = Physics.OverlapSphere(npc.transform.position, npc.sightRange / 3, npc.myEnemyLayers);

            if(colliders.Length > 0)
            {
                VisibilityCalculations(colliders[0].transform);

                if(dotProd > 0)
                {
                    AlertStateActions(colliders[0].transform);
                    return;
                }
            }

            //Check max range
            colliders = Physics.OverlapSphere(npc.transform.position, npc.sightRange, npc.myEnemyLayers);

            foreach(Collider col in colliders)
            {
                RaycastHit hit;
                VisibilityCalculations(col.transform);

                if(Physics.Linecast(npc.head.position,lookAtPoint,out hit, npc.sightLayer))
                {
                    foreach(string tags in npc.myEnemyTags)
                    {
                        if (hit.transform.CompareTag(tags))
                        {
                            if(dotProd > 0)
                            {
                                AlertStateActions(col.transform);
                                return;
                            }   
                        }
                    }
                }
            }
        }

        void Patrol()
        {
            npc.meshRendererFlag.material.color = Color.green;

            if (npc.myFollowTarget != null)
                npc.currentState = npc.followState;

            if (!npc.myNavMeshAgent.enabled)
                return;

            if(npc.waypionts.Length > 0)
            {
                MoveTo(npc.waypionts[nextWayPoint].position);

                if (HaveIReachedDestination())
                {
                    nextWayPoint = (nextWayPoint + 1) % npc.waypionts.Length;
                }
            }

            else //Wander about if there are no waypoints
            {
                if (HaveIReachedDestination())
                {
                    StopWalking();

                    if(RandomWanderTarget(npc.transform.position,npc.sightRange,out npc.wanderTarget))
                    {
                        MoveTo(npc.wanderTarget);
                    }
                }
            }
        }

        void AlertStateActions(Transform target)
        {
            npc.locationOfInterest = target.position; //For the check state
            ToAlertState();
        }

        void VisibilityCalculations(Transform target)
        {
            lookAtPoint = new Vector3(target.position.x, target.position.y + npc.offset, target.position.z); //For the line cast
            heading = lookAtPoint - npc.transform.position;
            dotProd = Vector3.Dot(heading, npc.transform.forward); //if the target is in front of the npc
        }

        bool RandomWanderTarget(Vector3 center,float range,out Vector3 result)
        {
            UnityEngine.AI.NavMeshHit navHit;

            Vector3 randomPoint = center + Random.insideUnitSphere * npc.sightRange;

            if(UnityEngine.AI.NavMesh.SamplePosition(randomPoint,out navHit, 3.0f, UnityEngine.AI.NavMesh.AllAreas)) //if it is in the navmesh area
            {
                result = navHit.position;
                return true;
            }
            else
            {
                result = center;
                return false;     
            }
        }

        bool HaveIReachedDestination()
        {
            if (npc.myNavMeshAgent.remainingDistance <= npc.myNavMeshAgent.stoppingDistance && !npc.myNavMeshAgent.pathPending)
            {
                StopWalking();
                return true;    
            }
            else
            {
                KeepWalking();
                return false;
            }
        }

        void MoveTo(Vector3 targetPos)
        {
            if(Vector3.Distance(npc.transform.position,targetPos) > npc.myNavMeshAgent.stoppingDistance + 1)
            {
                npc.myNavMeshAgent.SetDestination(targetPos);
                KeepWalking();
            }
        }

        void KeepWalking()
        {
            npc.myNavMeshAgent.isStopped = false;
            npc.npcMaster.CallEventNpcWalkAnim();
        }

        void StopWalking()
        {
            npc.myNavMeshAgent.isStopped = true;
            npc.npcMaster.CallEventNpcIdleAnim();
        }
    }
}

