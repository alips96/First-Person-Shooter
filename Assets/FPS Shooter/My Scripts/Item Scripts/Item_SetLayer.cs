using UnityEngine;
using System.Collections;

namespace S3
{
    public class Item_SetLayer : MonoBehaviour
    {
        private Item_Master itemMaster;
        public string itemThrowLayer;
        public string itemPickupLayer;

        void OnEnable()
        {
            SetInitialRefrences();
            itemMaster.EventObjectPickup += SetItemToPickupLayer;
            itemMaster.EventObjectThrow += SetItemToThrowLayer;
        }

        void OnDisable()
        {
            itemMaster.EventObjectPickup -= SetItemToPickupLayer;
            itemMaster.EventObjectThrow -= SetItemToThrowLayer;
        }

        void Start()
        {
            SetLayerOnEnable(); // because an execution error occured "Tag name is null"
        }

        void SetInitialRefrences()
        {
            itemMaster = GetComponent<Item_Master>();
        }

        void SetItemToThrowLayer()
        {
            SetLayer(transform, itemThrowLayer);
        }

        void SetItemToPickupLayer()
        {
            SetLayer(transform, itemPickupLayer);
        }

        void SetLayerOnEnable()
        {
            if (itemPickupLayer == "")
                itemPickupLayer = "Item";
            if (itemThrowLayer == "")
                itemThrowLayer = "Item";

            if (transform.root.CompareTag(GameManager_Refrences._playerTag))
            {
                SetItemToPickupLayer();
            }
            else
            {
                SetItemToThrowLayer();
            }
        }

        void SetLayer(Transform tForm,string itemLayerName)
        {
            tForm.gameObject.layer = LayerMask.NameToLayer(itemLayerName);
            foreach(Transform child in tForm) // Here we set layer to all of the children of the object
            {
                SetLayer(child, itemLayerName);
            }
        }
    }
}

