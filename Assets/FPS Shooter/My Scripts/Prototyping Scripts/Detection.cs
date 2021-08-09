using UnityEngine;
using System.Collections;

namespace Chapter1
{
    public class Detection : MonoBehaviour
    {
        private float checkRate = 0.5f;
        private float nextCheck = 0;
        private Transform myTransform;
        private RaycastHit hit;
        private float range = 5;
        private LayerMask DetectionLayer;


        // Use this for initialization
        void Start()
        {
            SetInitialRefrences();
        }

        // Update is called once per frame
        void Update()
        {
            DetectItems();
        }
        void SetInitialRefrences()
        {
            myTransform = transform;
            DetectionLayer = 1 << 9;
        }

        void DetectItems()
        {
            if(Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;
                if (Physics.Raycast(myTransform.position, myTransform.forward, out hit, range, DetectionLayer))
                Debug.Log(hit.transform.name + "Is an item");
            }
        }
    }
}
