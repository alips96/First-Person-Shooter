using UnityEngine;
using System.Collections;

namespace S3
{
    public class Destructible_CollisionDetection : MonoBehaviour
    {
        private Destructible_Master destructibleMaster;
        private Collider[] hitColliders;
        private Rigidbody myRigidbody;
        public float thresholdMass = 50;
        public float thresholdSpeed = 6;

        // Use this for initialization
        void Start()
        {
            SetInitalRefrences();
        }

        void OnCollisionEnter(Collision col)
        {
            if(col.contacts.Length > 0)
            {
                if(col.contacts[0].otherCollider.GetComponent<Rigidbody>() != null)
                {
                    CollisionCheck(col.contacts[0].otherCollider.GetComponent<Rigidbody>());
                }
                else
                {
                    SelfSpeedCheck();
                }
            }
        }

        void SetInitalRefrences()
        {
            destructibleMaster = GetComponent<Destructible_Master>();

            if (GetComponent<Rigidbody>() != null)
                myRigidbody = GetComponent<Rigidbody>();    
        }

        void CollisionCheck(Rigidbody otherRigidbody)
        {
            if(otherRigidbody.mass > thresholdMass &&
                otherRigidbody.velocity.sqrMagnitude > (thresholdSpeed * thresholdSpeed))
            {
                int damage = (int) otherRigidbody.mass;
                destructibleMaster.CallEventDeductHealth(damage);
            }
            else
            {
                SelfSpeedCheck();
            }
        }

        void SelfSpeedCheck()
        {
            if (myRigidbody.velocity.sqrMagnitude > (thresholdSpeed * thresholdSpeed))
            {
                int damage = (int)myRigidbody.mass;
                destructibleMaster.CallEventDeductHealth(damage);
            }
              
        }
    }
}

