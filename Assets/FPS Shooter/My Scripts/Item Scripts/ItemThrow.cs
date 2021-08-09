using UnityEngine;
using System.Collections;

namespace S3
{
    public class ItemThrow : MonoBehaviour
    {
        private Item_Master itemMaster;
        private Transform myTransform;
        private Rigidbody myRigidBody;
        private Vector3 throwDirection;

        public bool canBeThrown;
        public string throwButtonName;
        public float throwForce;

        // Use this for initialization
        void Start()
        {
            SetInitialRefrences();
        }

        // Update is called once per frame
        void Update()
        {
            CheckForThrowInput();
        }

        void SetInitialRefrences()
        {
            itemMaster = GetComponent<Item_Master>();
            myTransform = transform;
            myRigidBody = GetComponent<Rigidbody>();
        }

        void CheckForThrowInput()
        {
            if(throwButtonName != null)
            {
                if(Input.GetButtonDown(throwButtonName) && Time.timeScale > 0 && canBeThrown && myTransform.root.CompareTag(GameManager_Refrences._playerTag))
                {
                    // myTransform.root.CompareTag(GameManager_Refrences._playerTag): if the tag of the parent was player then...
                    CarryOutThrowActions();
                }
            }
        }

        void CarryOutThrowActions()
        {
            throwDirection = myTransform.parent.forward; // root means the player but parent means the fps character
            //forward is a vector3
            myTransform.parent = null; // It sepreates the item from the fps character
            itemMaster.CallEventObjectThrow();
            HurlItem();
        }

        void HurlItem()
        {
            myRigidBody.AddForce(throwDirection * throwForce, ForceMode.Impulse);
        }
    }
}

