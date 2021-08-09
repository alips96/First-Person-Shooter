using UnityEngine;
using System.Collections;


namespace S3
{
    public class GameManager_TogglePause : MonoBehaviour
    {
        private GameManager_Master GameManagerMaster;
        private bool isPaused;

        void OnEnable()
        {
            SetInitialRefrences();
            GameManagerMaster.MenuToggleEvent += TogglePause;
            GameManagerMaster.InventoryUIToggleEvent += TogglePause;
        }

        void OnDisable()
        {
            GameManagerMaster.MenuToggleEvent -= TogglePause;
            GameManagerMaster.InventoryUIToggleEvent -= TogglePause;
        }

        void SetInitialRefrences()
        {
            //GameManagerMaster = GameObject.Find("GameManager").GetComponent<GameManager_Master>(); 
            GameManagerMaster = GetComponent<GameManager_Master>(); // because it is on the same gameObject we dont need to search(find) it as above
        }

        void TogglePause() // if the game was paused switch it to normal but if the game was normal pause it!
        {
            if (isPaused)
            {
                Time.timeScale = 1; //time.timescale is about passing of time
                isPaused = false;
            }
            else
            {
                Time.timeScale = 0;
                isPaused = true;
            }
        }
    }
}

