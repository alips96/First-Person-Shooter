using UnityEngine;
using System.Collections;

namespace S3
{
    public class Item_UI : MonoBehaviour
    {
        private Item_Master itemMaster;
        public GameObject myUI;

        void OnEnable()
        {
            SetInitialRefrences();
            itemMaster.EventObjectPickup += EnableMyUI;
            itemMaster.EventObjectThrow += DisableMyUI;
        }

        void OnDisable()
        {
            itemMaster.EventObjectPickup -= EnableMyUI;
            itemMaster.EventObjectThrow -= DisableMyUI;
        }

        void SetInitialRefrences()
        {
            itemMaster = GetComponent<Item_Master>();
        }

        void EnableMyUI()
        {
            if (myUI != null)
                myUI.SetActive(true);
        }

        void DisableMyUI()
        {
            if (myUI != null)
                myUI.SetActive(false); 
        }
    }
}

