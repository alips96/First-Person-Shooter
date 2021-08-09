//OnEnable happens before start!
using UnityEngine;
using System.Collections;
using UnityEngine.UI; //We should add this for declaring UI

namespace S3
{
    public class Player_Health : MonoBehaviour
    {
        private GameManager_Master GameManagermaster; // for game over
        private Player_Master PlayerMaster;
        public int playerHealth;
        public Text HealthText;// it is a text which is a UI 


        // Use this for initialization
        void Start()
        {
            //StartCoroutine(TestHealthDeduction());
        }

        void OnEnable()
        {
            SetInitialRefrences();
            SetUI();
            PlayerMaster.EventPlayerHealthDeduction += DeductHealth;
            PlayerMaster.EventPlayerHealthIncrease += IncreaseHealth;
        }

        void OnDisable()
        {
            PlayerMaster.EventPlayerHealthDeduction -= DeductHealth;
            PlayerMaster.EventPlayerHealthIncrease -= IncreaseHealth;
        }

        void SetInitialRefrences()
        {
            GameManagermaster = GameObject.Find("GameManager").GetComponent<GameManager_Master>();
            PlayerMaster = GetComponent<Player_Master>();
        }

        IEnumerator TestHealthDeduction() //game over test
        {
            yield return new WaitForSeconds(2);
            //DeductHealth(100);
			PlayerMaster.CallEventPlayerHealthDeduction(50);
        }

        void DeductHealth(int healthChange)
        {
            playerHealth -= healthChange;
            if(playerHealth <= 0)
            {
                playerHealth = 0;
                GameManagermaster.CallEventGameOver();
            }

            SetUI(); //anything we have done, finally we should set it to the text 
        }

        void IncreaseHealth(int healthChange)
        {
            playerHealth += healthChange;
            if(playerHealth > 100)
            {
                playerHealth = 100;
            }

            SetUI();
        }

        void SetUI()
        {
           if(HealthText != null)
            {
                HealthText.text = playerHealth.ToString();
            }
        }
    }
}

