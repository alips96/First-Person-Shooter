using UnityEngine;
using System.Collections;

namespace S3
{
    public class Enemy_Health : MonoBehaviour
    {
        private Enemy_Master enemyMaster;
        public int enemyHealth = 100;
        public float healthLow = 25;

        void OnEnable()
        {
            SetInitialRefrences();
            enemyMaster.EventEnemyDeductHealth += DeductHealth;
            enemyMaster.EventEnemyIncreaseHealth += IncreaseHealth;
        }

        void OnDisable()
        {
            enemyMaster.EventEnemyDeductHealth -= DeductHealth;
            enemyMaster.EventEnemyIncreaseHealth -= IncreaseHealth;
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Period)) // the '.' key
            {
                //Debug.Log("Period key pressed");
                enemyMaster.CallEventEnemyIncreaseHealth(70);
            }
        }

        void SetInitialRefrences()
        {
            enemyMaster = GetComponent<Enemy_Master>();
        }

        void DeductHealth(int healthChange)
        {
           // Debug.Log(healthChange.ToString());
            enemyHealth -= healthChange;
            if(enemyHealth <= 0)
            {
                enemyHealth = 0;
                enemyMaster.CallEventEnemyDie();
                Destroy(gameObject, Random.Range(10, 20));
            }
            CheckHealthFraction();
        }

        void CheckHealthFraction()
        {
            if (enemyHealth <= healthLow && enemyHealth > 0)
                enemyMaster.CallEventEnemyHealthLow();
            if (enemyHealth > healthLow)
                enemyMaster.CallEventEnemyHealthRecovered();
        }

        void IncreaseHealth(int healthChange)
        {
            enemyHealth += healthChange;
            if (enemyHealth > 100)
                enemyHealth = 100;

            CheckHealthFraction();
        }
    }
}

