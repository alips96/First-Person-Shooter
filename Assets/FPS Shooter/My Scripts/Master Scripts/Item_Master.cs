//GameManager_Refrences._player:It is the player object which is accessible in every scripts because it is universal
using UnityEngine;
using System.Collections;

namespace S3
{
    public class Item_Master : MonoBehaviour
    {
        private Player_Master playerMaster;

        public delegate void GeneralEventHandler();
        public event GeneralEventHandler EventObjectThrow;
        public event GeneralEventHandler EventObjectPickup;

        public delegate void PickupActionEventHandler(Transform item);
        public event PickupActionEventHandler EventPickupAction;

        //void OnEnable()
        //{
        //    SetInitialRefrences();
        //}

        void Start()
        {
            SetInitialRefrences(); //Wy start?? because we want it to be called a while after the call process
            // in Item_Ammo script to work well
        }

        public void CallEventObjectThrow()
        {
            if (EventObjectThrow != null)
            {
                EventObjectThrow();
            }
            playerMaster.CallEventHandsEmpty();
            playerMaster.CallEventInventoryChanged();

        }

        public void CallEventObjectPickup()
        {
            if(EventObjectPickup != null)
            {
                EventObjectPickup();

            }
            playerMaster.CallEventInventoryChanged();
        }               

        public void CallEventPickupAction(Transform item)
        {
            if(EventPickupAction != null)
            {
                EventPickupAction(item);
            }
        }

        void SetInitialRefrences()
        {
            if(GameManager_Refrences._player != null) // Through this way we can access the universal parameter
            {
                // This is anotheer way of filling this objects
                playerMaster = GameManager_Refrences._player.GetComponent<Player_Master>();
            }
        }
    }

}

