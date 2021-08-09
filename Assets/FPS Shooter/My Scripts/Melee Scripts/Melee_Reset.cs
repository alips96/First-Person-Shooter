using UnityEngine;
using System.Collections;

namespace S3
{
    public class Melee_Reset : MonoBehaviour
    {
        private Melee_Master meleeMaster;
        private Item_Master itemMaster;

        void OnEnable()
        {
            SetInitialRefrences();

            if (itemMaster != null)
                itemMaster.EventObjectThrow += ResetMelee;
        }

        void OnDisable()
        {
            if (itemMaster != null)
                itemMaster.EventObjectThrow -= ResetMelee;
        }

        void SetInitialRefrences()
        {
            meleeMaster = GetComponent<Melee_Master>();

            if (GetComponent<Item_Master>() != null)
                itemMaster = GetComponent<Item_Master>();
        }

        void ResetMelee()
        {
           // Debug.Log("is in use turned off");
            meleeMaster.isInUse = false;
        }
    }
}

