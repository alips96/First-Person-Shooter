using UnityEngine;
using System.Collections;

namespace S3
{
    public class Item_Pickup : MonoBehaviour
    {
        private Item_Master itemMaster;
        //private Transform myTransform;

        void OnEnable()
        {
            SetInitialRefrences();
            itemMaster.EventPickupAction += CarryOutPickupActions; 
        }

        void OnDisable()
        {
            itemMaster.EventPickupAction -= CarryOutPickupActions;
        }

        void SetInitialRefrences()
        {
            itemMaster = GetComponent<Item_Master>();
            //myTransform = transform;
        }

        void CarryOutPickupActions(Transform tParent) // tParent is goning to bee the parent of the object
        {
            transform.SetParent(tParent);
            itemMaster.CallEventObjectPickup();
            transform.gameObject.SetActive(false);

        }
    }

}

