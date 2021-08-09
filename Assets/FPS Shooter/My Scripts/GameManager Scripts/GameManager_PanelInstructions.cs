using UnityEngine;
using System.Collections;


namespace S3
{
    public class GameManager_PanelInstructions : MonoBehaviour
    {

        public GameObject PanelInstructions;
        private GameManager_Master GameManagerMaster;

        void OnEnable()
        {
            SetInitialrefrences();
            GameManagerMaster.GameOverEvent += TurnOffPanelInstruction;
        }

        void OnDisable()
        {
            GameManagerMaster.GameOverEvent -= TurnOffPanelInstruction;
        }

        void SetInitialrefrences()
        {
            GameManagerMaster = GetComponent<GameManager_Master>();
        }

        void TurnOffPanelInstruction()
        {
            if (PanelInstructions != null)
                PanelInstructions.SetActive(false);
        }
    }
}

