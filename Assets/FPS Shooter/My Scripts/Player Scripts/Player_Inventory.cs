using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace S3
{
    public class Player_Inventory : MonoBehaviour
    {
        public Transform inventoryPlayerParent;
        public Transform inventoryUIParent;
        public GameObject UIbutton;

        private Player_Master playerMaster;
        private GameManager_ToggleInventoryUI inventoryUIScript;
        private float timeToPlaceInHands = 0.1f;
        private Transform currentlyHeldItem;
        private int counter;
        private string buttonText;
        private List<Transform> listInventory = new List<Transform>();

        void OnEnable()
        {
            SetInitialRefrences();
            DeactivateAllInventoryItems();
            UpdateInventoryListAndUI();
            CheckIfHandsEmpty();

            playerMaster.EventInventoryChanged += UpdateInventoryListAndUI;	
            playerMaster.EventInventoryChanged += CheckIfHandsEmpty;
            playerMaster.EventHandsEmpty += ClearHands;
        }

        void OnDisable()
        {
            playerMaster.EventInventoryChanged -= UpdateInventoryListAndUI;
            playerMaster.EventInventoryChanged -= CheckIfHandsEmpty;
            playerMaster.EventHandsEmpty -= ClearHands;
        }

        void SetInitialRefrences()
        {
            inventoryUIScript = GameObject.Find("GameManager").GetComponent<GameManager_ToggleInventoryUI>();
            playerMaster = GetComponent<Player_Master>();
        }

        void UpdateInventoryListAndUI()
        {
            counter = 0;
            listInventory.Clear();
            listInventory.TrimExcess(); //it will remove the empty entries  

            ClearInventoryUI();

            foreach (Transform child in inventoryPlayerParent)
            {
                if (child.CompareTag("Item"))
                {
                    listInventory.Add(child);
                    GameObject go = Instantiate(UIbutton) as GameObject;// Here we instantiated a button!
                    buttonText = child.name;
                    go.GetComponentInChildren<Text>().text = buttonText; // getcomponent from the children of go
                    int index = counter;
                    go.GetComponent<Button>().onClick.AddListener(delegate { ActivateInventoryItem(index); }); //this method activates inventory item! When we press the button it will be called.
                    // delegate is foق passing the parameter
                    go.GetComponent<Button>().onClick.AddListener(inventoryUIScript.ToggleInventoryUI);// When we press the go button it calls the method toggleInventoryScript
                    go.transform.SetParent(inventoryUIParent,false); //if it was true, it would have been positioned as before(the world space position an rotation)
                    counter++;
                }
            }
        }

        void CheckIfHandsEmpty()
        {
            if(currentlyHeldItem == null && listInventory.Count > 0)
            {
                StartCoroutine(PlaceItemsInHands(listInventory[listInventory.Count - 1])); // the last index. why? 
                // because of the throwing object issue. this will place the last item in the list in the hands
            }
        }

        void ClearHands()
        {
            currentlyHeldItem = null;
        }

        void ClearInventoryUI()
        {
            foreach (Transform child in inventoryUIParent)
            { //Child game objects are buttons in inventoryUI
                Destroy(child.gameObject);
            }
        }

        public void ActivateInventoryItem(int inventoryIndex)
        {
            DeactivateAllInventoryItems(); //At first deactivate everything
            StartCoroutine(PlaceItemsInHands(listInventory[inventoryIndex]));
        }

        void DeactivateAllInventoryItems()
        {
            foreach (Transform child in inventoryPlayerParent) 
            {
                if (child.CompareTag("Item")) //it the are already activated it will deactivate them  
                {
                    child.gameObject.SetActive(false);
                }
            }
        }

        IEnumerator PlaceItemsInHands(Transform itemTransform)
        { //it is an ienumerator because the weapon must be placed a bit aafte the last weapon
            yield return new WaitForSeconds(timeToPlaceInHands);
            currentlyHeldItem = itemTransform;
            currentlyHeldItem.gameObject.SetActive(true); 
        }
    }
}

