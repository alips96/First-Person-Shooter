using UnityEngine;
using System.Collections;

namespace S3
{
    public class Item_SetRotation : MonoBehaviour
    {
        private Item_Master itemMaster;
        public Vector3 itemLocalRotation;

        void OnEnable()
        {
            SetInitialRefrences();
            itemMaster.EventObjectPickup += SetRotationOnPlayer;
        }

        void OnDisable()
        {
            itemMaster.EventObjectPickup -= SetRotationOnPlayer;
        }

        void Start()
        {
            SetRotationOnPlayer();//we put it on start because game manager refrences take some time to get set up  
        }

        void SetInitialRefrences()
        {
            itemMaster = GetComponent<Item_Master>();
        }

        void SetRotationOnPlayer()
        {
            if (transform.root.CompareTag(GameManager_Refrences._playerTag))
            {
                transform.localEulerAngles = itemLocalRotation; //the same as transform.localPosition but in rotation
            }
        }
    }

}
