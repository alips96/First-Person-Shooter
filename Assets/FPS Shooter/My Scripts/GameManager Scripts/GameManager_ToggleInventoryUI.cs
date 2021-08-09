using UnityEngine;
using System.Collections;


namespace S3
{
    public class GameManager_ToggleInventoryUI : MonoBehaviour
    {
        [Tooltip("Does this game mode have an inventory? Set true if that is the case!")]
        public bool hasInventory;
        public GameObject inventoryUI;
        public string toggleInventoryButton;
        private GameManager_Master GameManagerMaster;

        // Use this for initialization
        void Start()
        {
            SetInitialRefrences();
        }

        // Update is called once per frame
        void Update()
        {
            CheckForInventoryUIToggleRequset();
        }

        void SetInitialRefrences()
        {
            GameManagerMaster = GetComponent<GameManager_Master>();

            if (toggleInventoryButton == "")
            {
                Debug.LogWarning("Please type in the the name og the button used to toggle the inventory in "+
                    "GameManager_ToggleInventoryUI");
                this.enabled = false; //This script would be disabled
            }
            
        }

        void CheckForInventoryUIToggleRequset()
        {
            if (Input.GetButtonUp(toggleInventoryButton) && !GameManagerMaster.isMenuOn && !GameManagerMaster.isGameOver
                && hasInventory)
            {
                ToggleInventoryUI();
            }
        }

        public void ToggleInventoryUI()
        {
            if(inventoryUI != null)
            {
                inventoryUI.SetActive(!inventoryUI.activeSelf);
                GameManagerMaster.isInvetoryUIOn = !GameManagerMaster.isInvetoryUIOn;
                GameManagerMaster.CallEventInventoryUIToggle();
            }
        }
    }
}

