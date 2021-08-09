// This code is the AI of the enemy!

using UnityEngine;
using System.Collections;


namespace Chapter1
{
    public class Enemy_Chase : MonoBehaviour
    {
        public LayerMask detectionLayer;
        private Transform myTransform;
        private UnityEngine.AI.NavMeshAgent myNavMeshAgent;
        private Collider[] hitColliders;
        private float checkRate;
        private float nextCheck;
        private float detectionRadius = 50f;

        // Use this for initialization
        void Start()
        {
            SetInitialRefrences();
        }

        // Update is called once per frame
        void Update()
        {
            CheckPlayerInRange();
        }

        void SetInitialRefrences()
        {
            myTransform = transform;
            myNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>(); // it is for teh efficientcy
            checkRate = Random.Range(0.8f, 1.2f);
        }

        void CheckPlayerInRange()
        {
            if(Time.time > nextCheck && myNavMeshAgent.enabled == true)
            {
                nextCheck = Time.time + checkRate;
                hitColliders = Physics.OverlapSphere(myTransform.position, detectionRadius, detectionLayer);

                if(hitColliders.Length > 0)
                {
                    myNavMeshAgent.SetDestination(hitColliders[0].transform.position);
                }
            }
        }
    }
}

