using UnityEngine;
using System.Collections;


namespace S3
{
    public class Item_Name : MonoBehaviour
    {
        public string itemName;

        // Use this for initialization
        void Start() //In this case there is no difference wheter to use OnEnable or Start method
        {
            SetItemName();
        }

        void SetItemName()
        {
            transform.name = itemName;
        }
    }

}

