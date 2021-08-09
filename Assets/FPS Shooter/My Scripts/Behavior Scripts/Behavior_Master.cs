using UnityEngine;
using System.Collections;

namespace S3
{

	public class Behavior_Master : MonoBehaviour 
	{
        public delegate void GeneralEventHandler();
        public event GeneralEventHandler EventAttackAnimation;
        public event GeneralEventHandler EventWalkAnimation;
        public event GeneralEventHandler EventIdleAnimation;
        public event GeneralEventHandler EventStruckAnimation;

        public void CallEventAttackAnimation()
        {
            if (EventAttackAnimation != null)
                EventAttackAnimation();
        }

        public void CallEventWalkAnimation()
        {
            if (EventWalkAnimation != null)
                EventWalkAnimation();
        }

        public void CallEventIdleAnimation()
        {
            if (EventIdleAnimation != null)
                EventIdleAnimation();
        }

        public void CallEventStruckAnimation()
        {
            if (EventStruckAnimation != null)
                EventStruckAnimation();
        }
    }
}


