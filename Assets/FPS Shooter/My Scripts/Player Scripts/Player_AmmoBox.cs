using UnityEngine;
using System.Collections;
using System.Collections.Generic;//this makes use of the lists

namespace S3
{
    public class Player_AmmoBox : MonoBehaviour
    {
        private Player_Master playerMaster;

        [System.Serializable]// makeing sure taht the list is shown in the inspector
        public class AmmoTypes
        {
            public string ammoName;
            public int ammoCurrentCarried;
            public int ammoMaxQuantity;

            public AmmoTypes(string aName,int aCurrentCarried,int aMaxQuantity) // The Constructor
            {
                ammoName = aName;
                ammoCurrentCarried = aCurrentCarried;
                ammoMaxQuantity = aMaxQuantity;
            }
        }

        public List<AmmoTypes> typesOfAmmunition = new List<AmmoTypes>(); // How to define a list



        void OnEnable()
        {
            SetInitialRefrences();
            playerMaster.EventPickedUpAmmo += PickedUpAmmo;
        }

        void OnDisable()
        {
            playerMaster.EventPickedUpAmmo -= PickedUpAmmo;
        }

        void SetInitialRefrences()
        {
            playerMaster = GetComponent<Player_Master>();
        }

        void PickedUpAmmo(string ammoName,int quantity)
        {
            for(int i = 0; i < typesOfAmmunition.Count; i++)
            {
                if(typesOfAmmunition[i].ammoName == ammoName)
                {
                    typesOfAmmunition[i].ammoCurrentCarried += quantity;
                    if (typesOfAmmunition[i].ammoCurrentCarried > typesOfAmmunition[i].ammoMaxQuantity)
                        typesOfAmmunition[i].ammoCurrentCarried = typesOfAmmunition[i].ammoMaxQuantity;

                    playerMaster.CallEventAmmoChanged();

                    break;
                }
            }
        }
    }
}

