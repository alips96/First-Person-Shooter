using UnityEngine;
using System.Collections;


namespace S3
{
    public class GameManager_ToggleCursor : MonoBehaviour
    {
        private GameManager_Master GameManagerMaster;
        private bool isCursorLocked = true;

        void OnEnable()
        {
            SetInitialRefrences();
            GameManagerMaster.InventoryUIToggleEvent += ToggleCursorState;
            GameManagerMaster.MenuToggleEvent += ToggleCursorState;
        }

        void OnDisable()
        {
            GameManagerMaster.InventoryUIToggleEvent -= ToggleCursorState;
            GameManagerMaster.MenuToggleEvent -= ToggleCursorState;
        }


        // Update is called once per frame
        void Update()
        {
            CheckIFCursorShouldBeLocked();
        }

        void SetInitialRefrences()
        {
            GameManagerMaster = GetComponent<GameManager_Master>();
        }

        void ToggleCursorState()
        {
            isCursorLocked = !isCursorLocked;
        }

        void CheckIFCursorShouldBeLocked()
        {
            if (isCursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}

