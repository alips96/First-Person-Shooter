using UnityEngine;
using System.Collections;

namespace S3
{
    public class Item_Animator : MonoBehaviour
    {
        private Item_Master itemMaster;
        public Animator myAnimator;
         
        void OnEnable()
        {
            SetInitialRefrences();
            itemMaster.EventObjectThrow += DisableMyAnimator;
            itemMaster.EventObjectPickup += EnableMyAnimator;
        }

        void OnDisable()
        {
            itemMaster.EventObjectThrow -= DisableMyAnimator;
            itemMaster.EventObjectPickup -= EnableMyAnimator;
        }

        void SetInitialRefrences()
        {
            itemMaster = GetComponent<Item_Master>();
        }

        void EnableMyAnimator()
        {
            if (myAnimator != null)
                myAnimator.enabled = true;
        }

        void DisableMyAnimator()
        {
            if (myAnimator != null)
                myAnimator.enabled = false;
        }
    }
}

