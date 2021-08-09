using UnityEngine;
using System.Collections;

namespace S3
{
    public class Destructible_TakeDamage : MonoBehaviour
    {
        private Destructible_Master destructibleMaster;

        // Use this for initialization
        void Start()
        {
            SetInitialRefrences();
        }

        void SetInitialRefrences()
        {
            destructibleMaster = GetComponent<Destructible_Master>();
        }

        public void ProcessDamage(int damage)
        {
            destructibleMaster.CallEventDeductHealth(damage);
        }
    }
}

