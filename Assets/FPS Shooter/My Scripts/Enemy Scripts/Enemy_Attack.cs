using UnityEngine;
using System.Collections;


namespace S3
{
    public class Enemy_Attack : MonoBehaviour
    {
        private Enemy_Master enemyMaster;
        private Transform attackTarget;
        private Transform myTransform;
        private float nextAttack;
        public float attackRate = 1;
        public float attackRange = 3.5f;
        public int attackDamage = 10;

        void OnEnable()
        {
            SetInitialRefrences();
            enemyMaster.EventEnemyDie += DisableThis;
            enemyMaster.EventEnemySetNavTarget += SetAttackTarget;
        }

        void OnDisable()
        {
            enemyMaster.EventEnemyDie -= DisableThis;
            enemyMaster.EventEnemySetNavTarget -= SetAttackTarget;
        }

        void Update()
        {
            TryToAttack();
        }

        void SetInitialRefrences()
        {
            enemyMaster = GetComponent<Enemy_Master>();
            myTransform = transform; 
        }

        void SetAttackTarget(Transform targetTransform)
        {
            attackTarget = targetTransform;
        }

        void TryToAttack()
        {
            if(attackTarget != null)
            {
                if(Time.time > nextAttack)
                {
                    nextAttack = Time.time + attackRate;
                    if(Vector3.Distance(myTransform.position,attackTarget.position) <= attackRange)
                    {
                        Vector3 lookAtVector = new Vector3(attackTarget.position.x, myTransform.position.y, attackTarget.position.z);
                        myTransform.LookAt(lookAtVector); // Now the enemy looks at the target!
                        enemyMaster.CallEventEnemyAttack();
                        enemyMaster.isOnRoute = false;
                    }
                }
            }
        }

        public void OnEnemyAttack() //Called by hPunch animation
        {
            if(attackTarget != null)
            {
                if(Vector3.Distance(attackTarget.position,myTransform.position) <= attackRange &&
                    attackTarget.GetComponent<Player_Master>() != null)
                {
                    Vector3 toOther = attackTarget.position - myTransform.position; //This is the direction vector
                    // We should be sure that the enemy is infront of the target!
                    //Debug.Log(Vector3.Dot(toOther, myTransform.forward).ToString());

                    if(Vector3.Dot(toOther,myTransform.forward) > 0.5f)
                    {
                        attackTarget.GetComponent<Player_Master>().CallEventPlayerHealthDeduction(attackDamage);
                        //This would cause the player to get hurt
                    }
                }
            }
        }

        void DisableThis()
        {
            this.enabled = false;
        }

    }

}

