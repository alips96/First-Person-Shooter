//This script is for when the barrel is destructed we don't want it to be in the player inventory any more!
//that was a bug actually :)
using UnityEngine;
using System.Collections;

namespace S3
{
    public class Destructible_PlayerInventoryUpdate : MonoBehaviour
    {
        private Destructible_Master destructibleMaster;
        private Player_Master playerMaster;

        void OnEnable()
        {
            SetInitialRefrences();
            destructibleMaster.EventDestroyMe += ForcePlayerInventoryUpdate;
        }

        void OnDisable()
        {
            destructibleMaster.EventDestroyMe -= ForcePlayerInventoryUpdate;
        }

        // Use this for initialization
        void Start()
        {
            SetInitialRefrences();
        }

        void SetInitialRefrences()
        {
            if (GetComponent<Item_Master>() == null)
                Destroy(this); //It is for efficiency. we wanna be accertian if it not an item this script would be destroyed

            if (GameManager_Refrences._player != null)
                playerMaster = GameManager_Refrences._player.GetComponent<Player_Master>();

            destructibleMaster = GetComponent<Destructible_Master>();
        }

        void ForcePlayerInventoryUpdate()
        {
            if (playerMaster != null)
                playerMaster.CallEventInventoryChanged();
        }
    }
}

