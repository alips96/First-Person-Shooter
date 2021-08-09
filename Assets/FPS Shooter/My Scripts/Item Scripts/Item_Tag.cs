using UnityEngine;
using System.Collections;


namespace S3 {
    public class Item_Tag : MonoBehaviour
    {
        public string itemTag;

        void OnEnable()
        {
            SetTag();
        }

        void SetTag()
        {
            if (itemTag == "")
                itemTag = "UnTagged";
            transform.tag = itemTag;  
        }

       
    }
}


