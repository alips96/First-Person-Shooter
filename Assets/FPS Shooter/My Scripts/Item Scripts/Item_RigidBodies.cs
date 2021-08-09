using UnityEngine;
using System.Collections;

namespace S3 {
    public class Item_RigidBodies : MonoBehaviour
    {
        private Item_Master itemMaster;
        public Rigidbody[] rigidBodies;

        void OnEnable()
        {
            SetInitialRefrences();
            itemMaster.EventObjectThrow += SetIsKinematicToFalse;
            itemMaster.EventObjectPickup += SetIsKinematicToTrue;
        }

        void OnDisable()
        {
            itemMaster.EventObjectThrow -= SetIsKinematicToFalse;
            itemMaster.EventObjectPickup -= SetIsKinematicToTrue;
        }

        void Start()
        {
            CheckIfStartsInInventory(); // because an execution error occured "Tag name is null"
        }

        void SetInitialRefrences()
        {
            itemMaster = GetComponent<Item_Master>();
        }

        void CheckIfStartsInInventory()
        {
            if (transform.root.CompareTag(GameManager_Refrences._playerTag))
            {
                SetIsKinematicToTrue();
            }
        }

        void SetIsKinematicToTrue()
        {
            if(rigidBodies.Length > 0)
            {
                foreach (Rigidbody rBody in rigidBodies)
                {
                    rBody.isKinematic = true;
                }
            }
        }

        void SetIsKinematicToFalse()
        {
            if (rigidBodies.Length > 0)
            {
                foreach (Rigidbody rBody in rigidBodies)
                {
                    rBody.isKinematic = false;
                }
            }
        }
    }
}


