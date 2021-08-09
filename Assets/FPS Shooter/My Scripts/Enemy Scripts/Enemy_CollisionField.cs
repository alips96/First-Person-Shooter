using UnityEngine;
using System.Collections;

namespace S3
{
    public class Enemy_CollisionField : MonoBehaviour
    {
        private Enemy_Master enemyMaster;
        private Rigidbody rigidbodyStrikingMe;
        private int damageToApply;
        public float massRequirement = 50;
        public float speedRequirement = 5;
        private float damageFactor = 0.1f;

        void OnEnable()
        {
            SetInitialRefrences();
            enemyMaster.EventEnemyDie += DisableThis;
        }

        void OnDisable()
        {
            enemyMaster.EventEnemyDie -= DisableThis;
        }

        void OnTriggerEnter(Collider other)
        {
            // Debug.Log("entered trigger");
          
            if (other.GetComponent<Rigidbody>() != null)
            {
                rigidbodyStrikingMe = other.GetComponent<Rigidbody>();

                if(rigidbodyStrikingMe.mass >= massRequirement &&
                    rigidbodyStrikingMe.velocity.sqrMagnitude > speedRequirement * speedRequirement)
                {
                  //  Debug.Log("entered if");
                  //  Debug.Log(other.GetComponent<Rigidbody>().mass.ToString());
                    //Always use sqrMagitute rather than magnitute in this situation because magnitute is lot more expensive
                    //Velocity is vector3 and the sqrmagnitude turns it to be float
                    damageToApply = (int)(damageFactor * rigidbodyStrikingMe.mass * rigidbodyStrikingMe.velocity.magnitude);
                    // i used to use sqemagnitude for this line and i was stocked like a dumpass!
                   // Debug.Log(damageToApply.ToString());
                    enemyMaster.CallEventEnemyDeductHealth(damageToApply);
                }
            }    
        }

        void SetInitialRefrences()
        {
            enemyMaster = transform.root.GetComponent<Enemy_Master>();
        }

        void DisableThis()
        {
            gameObject.SetActive(false); // it turns off the collision field. We could also destroy that
        }
    }
}

