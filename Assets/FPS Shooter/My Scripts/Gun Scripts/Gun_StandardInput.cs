using UnityEngine;
using System.Collections;

namespace S3
{
    public class Gun_StandardInput : MonoBehaviour
    {
        private Gun_Master gunMaster;
        private float nextAttack;
        private Transform myTransform;
        public float attackRate = 0.5f;
        public bool isAutomatic;
        public bool hasBurstFire;
        private bool isBurstFireActive;
        public string attackButtonName;
        public string reloadButtonName;
        public string burstFireButtonName;


        // Use this for initialization
        void Start()
        {
            SetInitialRefrences();
        }

        // Update is called once per frame
        void Update()
        {
            CheckIfWeaponShouldAttack();
            CheckForBurstFireToggle();
            CheckForReloadRequest();
        }

        void SetInitialRefrences()
        {
            gunMaster = GetComponent<Gun_Master>();
            myTransform = transform;
            gunMaster.isGunLoaded = true; //So the player can attempt shooting right away
        }

        void CheckIfWeaponShouldAttack()
        {
            if(Time.time > nextAttack && Time.timeScale > 0 && myTransform.root.CompareTag(GameManager_Refrences._playerTag))
            {
                if(isAutomatic && !isBurstFireActive)
                {
                    if (Input.GetButton(attackButtonName)) //get button is used when we are able to hold the button
                    {
                       // Debug.Log("Full Auto");
                        AttemptAttack();
                    }
                }
                else if(isAutomatic && isBurstFireActive)
                {
                    if (Input.GetButtonDown(attackButtonName))
                    {
                        //Debug.Log("Burst");
                        StartCoroutine(RunBurstFire()); //it attemps fire for three times
                    }
                }
                else if (!isAutomatic)
                {
                    if (Input.GetButtonDown(attackButtonName))
                    {
                        AttemptAttack();
                    }
                }
            }
        }

        void AttemptAttack()
        {
            nextAttack = Time.time + attackRate;

            if (gunMaster.isGunLoaded)
            {
                //Debug.Log("Shooting");
                gunMaster.CallEventPlayerInput();
            }
            else
            {
                gunMaster.CallEventGunNotUsable();
            }
        }

        void CheckForReloadRequest()
        {
            if(Input.GetButtonDown(reloadButtonName) && Time.timeScale > 0 && myTransform.root.CompareTag(GameManager_Refrences._playerTag))
            {
                gunMaster.CallEventRequestReload();
            }
        }

        void CheckForBurstFireToggle()
        {
            //Burst fire is on when the gun shouldn't fire continously 
            // and when the gun should fire continously the burstfire is off
            if (Input.GetButtonDown(burstFireButtonName) && Time.timeScale > 0 && myTransform.root.CompareTag(GameManager_Refrences._playerTag))
            {
                //Debug.Log("Burst fire toggled");
                isBurstFireActive = !isBurstFireActive;
                gunMaster.CallEventToggleBurstFire();
            }
        }

        IEnumerator RunBurstFire()
        {
            AttemptAttack();
            yield return new WaitForSeconds(attackRate);
            AttemptAttack();
            yield return new WaitForSeconds(attackRate);
            AttemptAttack();
        }
    }
}

