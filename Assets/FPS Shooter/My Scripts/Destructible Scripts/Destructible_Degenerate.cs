using UnityEngine;
using System.Collections;

namespace S3 {

    public class Destructible_Degenerate : MonoBehaviour
    {
        private Destructible_Master destructibleMaster;
        private bool isHealthLow = false;
        public float degenRate = 1;
        private float nextDegenTime;
        public int healthLoss = 5;

        void OnEnable()
        {
            SetInitialRefrences();
            destructibleMaster.EventHealthLow += HealthLow;
            //If we click ctrl + R + R we can change the name of a method or variable everywhere
        }

        void OnDisable()
        {
            destructibleMaster.EventHealthLow -= HealthLow;
        }

        void Update()
        {
            CheckIfHealthShouldBeDegenerated();
        }

        void SetInitialRefrences()
        {
            destructibleMaster = GetComponent<Destructible_Master>();
        }

        void HealthLow()
        {
            isHealthLow = true;
        }

        void CheckIfHealthShouldBeDegenerated()
        {
            if (isHealthLow)
            {
                if(Time.time > nextDegenTime)
                {
                    nextDegenTime = Time.time + degenRate;
                    destructibleMaster.CallEventDeductHealth(healthLoss);
                }
            }
        }
    }
}

