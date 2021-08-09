using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace S3
{
    public class Gun_AmmoUI : MonoBehaviour
    {
        public InputField currentAmmoField;
        public InputField carriedAmmoField;
        private Gun_Master gunMaster;

        void OnEnable()
        {
            SetInitialRefreces();
            gunMaster.EventAmmoChanged += UpdateAmmoUI;
        }

        void OnDisable()
        {
            gunMaster.EventAmmoChanged -= UpdateAmmoUI;
        }

        void SetInitialRefreces()
        {
            gunMaster = GetComponent<Gun_Master>();
        }

        void UpdateAmmoUI(int currentAmmo,int carriedAmmo)
        {
            if(currentAmmoField != null)
                currentAmmoField.text = currentAmmo.ToString();

            if(carriedAmmoField != null)
                carriedAmmoField.text = carriedAmmo.ToString();
        }
    }

}

