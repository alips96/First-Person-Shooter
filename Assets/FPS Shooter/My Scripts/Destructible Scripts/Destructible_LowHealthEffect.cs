using UnityEngine;
using System.Collections;

namespace S3
{
    public class Destructible_LowHealthEffect : MonoBehaviour
    {
        private Destructible_Master destructibleMaster;
        public GameObject[] lowHealthEffectGO;

        void OnEnable()
        {
            SetInitialRefrences();
            destructibleMaster.EventHealthLow += TurnOnLowHealthEffect;
        }

        void OnDisable()
        {
            destructibleMaster.EventHealthLow -= TurnOnLowHealthEffect;
        }

        void SetInitialRefrences()
        {
            destructibleMaster = GetComponent<Destructible_Master>();
        }

        void TurnOnLowHealthEffect()
        {
            if(lowHealthEffectGO.Length > 0)
            {
                foreach (GameObject go in lowHealthEffectGO)
                {
                    go.SetActive(true);
                }
            }
        }
    }
}

