using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;


namespace S3
{
    public class GameManager_TogglePlayer : MonoBehaviour
    {
        public FirstPersonController playerController;
        private GameManager_Master GameManagerMaster;

        void OnEnable()
        {
            SetInitialRefrences();
            GameManagerMaster.MenuToggleEvent += TogglePlayerController;
            GameManagerMaster.InventoryUIToggleEvent += TogglePlayerController;
        }

        void OnDisable()
        {
            GameManagerMaster.MenuToggleEvent -= TogglePlayerController;
            GameManagerMaster.InventoryUIToggleEvent -= TogglePlayerController;
        }

        void SetInitialRefrences()
        {
            GameManagerMaster = GetComponent<GameManager_Master>();
        }

        void TogglePlayerController()
        {
            if(playerController != null)
            {
                playerController.enabled = !playerController.enabled;
            }
            else
            {
                Debug.LogWarning("You need to assign a UI Player to the toggle menu script in the inspector.");
            }
        }
    }
}

