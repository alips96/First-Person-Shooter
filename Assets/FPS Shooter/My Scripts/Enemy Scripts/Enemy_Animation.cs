using UnityEngine;
using System.Collections;

namespace S3
{ 
    public class Enemy_Animation : MonoBehaviour
    {
        private Enemy_Master enemyMaster;
        private Animator myAnimator;

        void OnEnable()
        {
            SetInitialRefrences();
            enemyMaster.EventEnemyDie += DisableAnimator;
            enemyMaster.EventEnemyWalking += SetAnimationToWalk;
            enemyMaster.EventEnemyAttack += SetAnimationToAttack;
            enemyMaster.EventEnemyReachedNavTarget += SetAnimationToIdle;
            enemyMaster.EventEnemyDeductHealth += SetAnimationToStruck;
        }

        void OnDisable()
        {
            enemyMaster.EventEnemyDie -= DisableAnimator;
            enemyMaster.EventEnemyWalking -= SetAnimationToWalk;
            enemyMaster.EventEnemyAttack -= SetAnimationToAttack;
            enemyMaster.EventEnemyReachedNavTarget -= SetAnimationToIdle;
            enemyMaster.EventEnemyDeductHealth -= SetAnimationToStruck;
        }

        void SetInitialRefrences()
        {
            enemyMaster = GetComponent<Enemy_Master>();

            if (GetComponent<Animator>() != null)
                myAnimator = GetComponent<Animator>();
        }

        void SetAnimationToWalk()
        {
            if(myAnimator != null)
            {
                if (myAnimator.enabled)
                {
                    myAnimator.SetBool("isPursuing", true);
                }
            }
        }

        void SetAnimationToIdle()
        {
            if (myAnimator != null)
            {
                if (myAnimator.enabled)
                {
                    myAnimator.SetBool("isPursuing", false);
                }
            }
        }

        void SetAnimationToAttack()
        {
            if (myAnimator != null)
            {
                if (myAnimator.enabled)
                {
                    myAnimator.SetTrigger("Attack");
                }
            }
        }

        void SetAnimationToStruck(int dummy)
        {
            if (myAnimator != null)
            {
                if (myAnimator.enabled)
                {
                    myAnimator.SetTrigger("Struck");
                }
            }
        }

        void DisableAnimator()
        {
            if (myAnimator != null)
                myAnimator.enabled = false;
        }


    }
}

