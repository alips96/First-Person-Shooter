using UnityEngine;
using System.Collections;

namespace S3
{
    public class Item_SetPosition : MonoBehaviour
    {
        private Item_Master itemMaster;
        public Vector3 itemLocalPosition;

        void OnEnable()
        {
            SetInitialRefrences(); 
            itemMaster.EventObjectPickup += SetPositionOnPlayer;
        }

        void OnDisable()
        {
            itemMaster.EventObjectPickup -= SetPositionOnPlayer;
        }

        void Start()
        {
            SetPositionOnPlayer(); // because an execution error occured "Tag name is null"
        }

        void SetInitialRefrences()
        {
            itemMaster = GetComponent<Item_Master>();
        }

        void SetPositionOnPlayer()
        {
            if (transform.root.CompareTag(GameManager_Refrences._playerTag)) // Just for safetiness!
            {
                transform.localPosition = itemLocalPosition; // Pay attention!
            }
        }
    }
}

