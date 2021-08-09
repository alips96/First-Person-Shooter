// This code toggles the mouse cursor on the screen
using UnityEngine;
using System.Collections;


namespace Chapter1
{
    public class GameManager_CursorToggle : MonoBehaviour
    {
        private bool isCursorLocked;

        // Use this for initialization
        void Start()
        {
            ToggleCusrorState();
        }

        // Update is called once per frame
        void Update()
        {
            CheckForInput();
            CheckIfCursorShouldBeLocked();
        }
        
        void ToggleCusrorState()
        {
            isCursorLocked = !isCursorLocked;
        }

        void CheckForInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleCusrorState();
            }
        }

        void CheckIfCursorShouldBeLocked()
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

