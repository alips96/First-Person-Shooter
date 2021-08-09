using UnityEngine;
using System.Collections;


namespace S3
{
    public class GameManager_GameOver : MonoBehaviour
    {
        private GameManager_Master GameManagerMaster;
        public GameObject PanelGameOver;

        void OnEnable()
        {
            SetInitialRefrences();
            GameManagerMaster.GameOverEvent += TurnOnGameOverPanel;
        }

        void OnDisable()
        {
            GameManagerMaster.GameOverEvent -= TurnOnGameOverPanel;
        }

        void SetInitialRefrences()
        {
            GameManagerMaster = GetComponent<GameManager_Master>();
        }

        void TurnOnGameOverPanel()
        {
            if(PanelGameOver != null)
            {
                PanelGameOver.SetActive(true);
            }
        }
    }
}

