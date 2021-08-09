using UnityEngine;
using System.Collections;

namespace S3
{
    public class Item_Ammo : MonoBehaviour
    {
        private Item_Master itemMaster;
        private GameObject playerGo;
        public string ammoName;
        public int quantity;
        public bool isTriggerPickup;

        void OnEnable()
        {
            SetInitialRefrences();
            itemMaster.EventObjectPickup += TakeAmmo;
        }

        void OnDisable()
        {
            itemMaster.EventObjectPickup -= TakeAmmo;
        }

        // Use this for initialization
        void Start()
        {
            SetInitialRefrences(); // Why did we call it twice? because in game manager refrences the player 
            // will be filed in onEnable so we should call it in start to take a while for taht to be filled in
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameManager_Refrences._playerTag) && isTriggerPickup)
                TakeAmmo();        
        }

        void SetInitialRefrences()
        {
            itemMaster = GetComponent<Item_Master>();
            playerGo = GameManager_Refrences._player; // Important!

            if (isTriggerPickup)
            {
                if (GetComponent<Collider>() != null)
                    GetComponent<Collider>().isTrigger = true;
                if (GetComponent<Rigidbody>() != null)
                    GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        void TakeAmmo()
        {
            playerGo.GetComponent<Player_Master>().CallEventPickedUpAmmo(ammoName, quantity);
           // GameManager_Refrences._player.GetComponent<Player_Master>().CallEventPickedUpAmmo(ammoName, quantity); // another way use it
            Destroy(gameObject);
        }

    }
}

