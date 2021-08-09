using UnityEngine;
using System.Collections;

namespace S3
{
    public class Item_Drop : MonoBehaviour
    {
        private Item_Master itemMaster;
        public string dropButtonName;
        private Transform myTransform;

        // Use this for initialization
        void Start()
        {
            SetInitialRefrences();
        }

        // Update is called once per frame
        void Update()
        {
            CheckForDropInput();
        }

        void SetInitialRefrences()
        {
            itemMaster = GetComponent<Item_Master>();
            myTransform = transform;
        }

        void CheckForDropInput()
        {
            if(Input.GetButtonDown(dropButtonName) && Time.timeScale > 0 &&
                myTransform.root.CompareTag(GameManager_Refrences._playerTag))
            {
                myTransform.parent = null; //This line will do all the job!
                itemMaster.CallEventObjectThrow();
            }
        }
    }
}

