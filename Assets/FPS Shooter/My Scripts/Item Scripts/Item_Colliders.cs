using UnityEngine;
using System.Collections;

namespace S3
{
    public class Item_Colliders : MonoBehaviour
    {
        private Item_Master itemMaster;
        public Collider[] colliders;
        public PhysicMaterial myPhysicMaterial;

        void OnEnable()
        {
            SetInitialRefrences();
            itemMaster.EventObjectPickup += DisableColliders;
            itemMaster.EventObjectThrow += EnableColliders;
        }

        void OnDisable()
        {
            itemMaster.EventObjectPickup -= DisableColliders;
            itemMaster.EventObjectThrow -= EnableColliders;
        }

        void Start()
        {
            CheckIfStartsInInventory(); // because an execution error occured "Tag name is null"
        }

        void SetInitialRefrences()
        {
            itemMaster = GetComponent<Item_Master>();
            //colliders = GetComponentsInChildren<Collider>(); // if we didn't want to manually assign the array
        }

        void CheckIfStartsInInventory()
        {
            if (transform.root.CompareTag(GameManager_Refrences._playerTag))
            {
                DisableColliders();
            }
        }

        void EnableColliders()
        {
            if(colliders.Length > 0)
            {
                foreach (Collider col in colliders)
                {
                    col.enabled = true;
                    if (myPhysicMaterial != null) // it makes the collider more bouncy
                        col.material = myPhysicMaterial;
                }
            }
        }

        void DisableColliders()
        {
            if (colliders.Length > 0)
            {
                foreach (Collider col in colliders)
                {
                    col.enabled = false;
                }
            }
        }   
    }
}

