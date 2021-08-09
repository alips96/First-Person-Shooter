using UnityEngine;
using System.Collections;

namespace S3
{
    public class Gun_BurstFireIndicator : MonoBehaviour
    {
        private Gun_Master gunMaster;
        public GameObject burstFireIndicator;
        
        void OnEnable()
        {
            SetInitialRefrences();
            gunMaster.EventToggleBurstFire += ToggleIndicator;
        }

        void OnDisable()
        {
            gunMaster.EventToggleBurstFire -= ToggleIndicator;
        }

        void SetInitialRefrences()
        {
            gunMaster = GetComponent<Gun_Master>();
        }

        void ToggleIndicator()
        {
            if (burstFireIndicator != null)
                burstFireIndicator.SetActive(!burstFireIndicator.activeSelf);
        }
    }
}

