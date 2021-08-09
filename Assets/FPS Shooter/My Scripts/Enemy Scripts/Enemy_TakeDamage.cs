//Using shouldRemoveCollider is a great technique
using UnityEngine;
using System.Collections;

namespace S3
{
    public class Enemy_TakeDamage : MonoBehaviour
    {
        private Enemy_Master enemyMaster;
        public int damageMultiplier = 1;
        public bool shouldRemoveCollider;

        void OnEnable()
        {
            SetInitialRefrences();
            enemyMaster.EventEnemyDie += RemoveThis;
        }

        void OnDisable()
        {
            enemyMaster.EventEnemyDie -= RemoveThis;
        }

        void SetInitialRefrences()
        {
            enemyMaster = transform.root.GetComponent<Enemy_Master>(); //Because we are going to attach it to the head!
        }

        public void ProcessDamage(int damage)
        {
            int damageToApply = damage * damageMultiplier;
            //Multiplier is setting it for a number of objects with diffrent types
            //For example for head it is 5 and body it is 1
            enemyMaster.CallEventEnemyDeductHealth(damageToApply);
        }

        void RemoveThis()
        {
            if (shouldRemoveCollider)
            {
                if (GetComponent<Collider>() != null)
                    Destroy(GetComponent<Collider>());
                if (GetComponent<Rigidbody>() != null)
                    Destroy(GetComponent<Rigidbody>());
            }

            Destroy(this); //Destroy this script
        }
    }
}

