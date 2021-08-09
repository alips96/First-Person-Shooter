//Raycast is pinpointing but sphere cast is wider
//time.timescale>0 means game is not paused and whin it i 0 the game would be paused
//itemAvailableForPickup.root.tag != GameManager_Refrences._playerTag : root the camera is the player. so if the 
//player tag is _playertag wich is Player it wont do anything(player tag is a universal so everywhere is usable like here
using UnityEngine;
using System.Collections;


namespace S3
{
    public class Player_DetectItem : MonoBehaviour
    {
        [Tooltip("What layer is being used for items.")]
        public LayerMask layerToDetect;
        [Tooltip("What transform will the ray be fired from.")]
        public Transform rayTransformPivot;
        [Tooltip("The editor input button that will be used for picking up items.")]
        public string buttonPickup;

        private Transform itemAvailableForPickup;
        private RaycastHit hit;
        private float detectRange = 3;
        private float detectRadius = 0.7f;//the radius of the sphere we are casting 
        private bool itemInRange;

        private float labelWidth = 200;
        private float labelHeight = 50;


        // Update is called once per frame
        void Update()
        {
            CastRayForDetectingItems();
            CheckForItemPickupAttempt();
        }

        void CastRayForDetectingItems()
        {
            if(Physics.SphereCast(rayTransformPivot.position,detectRadius,rayTransformPivot.forward,out hit, detectRange, layerToDetect))
            {
                itemAvailableForPickup = hit.transform;
                itemInRange = true;
            }
            else
            {
                itemInRange = false;
            }
        }
        
        void CheckForItemPickupAttempt()
        {
            if(Input.GetButtonDown(buttonPickup) && Time.timeScale > 0 && itemInRange && itemAvailableForPickup.root.tag != GameManager_Refrences._playerTag)
            {
                //Debug.Log("Pickup attempted");
                itemAvailableForPickup.GetComponent<Item_Master>().CallEventPickupAction(rayTransformPivot);
            }
        }

        void OnGUI()//very useful
        {
            if(itemInRange && itemAvailableForPickup != null)
            {
                GUI.Label(new Rect(Screen.width / 2 - labelWidth / 2, Screen.height / 2, labelWidth, labelHeight),itemAvailableForPickup.name);
            }
        }
    }

}
