using UnityEngine;
using System.Collections;

namespace S3
{

	public class Behavior_Animations : MonoBehaviour 
	{
        private Behavior_Master behaviorMaster;
        private Animator myAnimator;

		void OnEnable()
		{
            SetInitialReferences();
            behaviorMaster.EventAttackAnimation += EnableAttackAnimation;
            behaviorMaster.EventIdleAnimation += EnableIdleAnimation;
            behaviorMaster.EventStruckAnimation += EnableStruckAnimation;
            behaviorMaster.EventWalkAnimation += EnableWalkAnimation;
		}

		void OnDisable()
		{
            behaviorMaster.EventAttackAnimation -= EnableAttackAnimation;
            behaviorMaster.EventIdleAnimation -= EnableIdleAnimation;
            behaviorMaster.EventStruckAnimation -= EnableStruckAnimation;
            behaviorMaster.EventWalkAnimation -= EnableWalkAnimation;
        }

		void SetInitialReferences()
		{
            behaviorMaster = GetComponent<Behavior_Master>();
            myAnimator = GetComponent<Animator>();
		}

        void EnableAttackAnimation()
        {
            myAnimator.SetTrigger("Attack");
            //Debug.Log("Attack animation called");
        }

        void EnableIdleAnimation()
        {
            myAnimator.SetBool("isPursuing", false);
        }

        void EnableStruckAnimation()
        {
            myAnimator.SetTrigger("Struck");
        }

        void EnableWalkAnimation()
        {
            myAnimator.SetBool("isPursuing", true);
        }
	}
}


