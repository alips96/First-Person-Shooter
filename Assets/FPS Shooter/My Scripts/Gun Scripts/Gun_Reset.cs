//This script is written to correct the bugs
using UnityEngine;
using System.Collections;

namespace S3
{
    public class Gun_Reset : MonoBehaviour
    {
        private Gun_Master gunMaster;
        private Item_Master itemMaster;

        void OnEnable()
        {
            SetInitialRefrences();

            if(itemMaster != null)
                itemMaster.EventObjectThrow += ResetGun;
        }

        void OnDisable()
        {
            itemMaster.EventObjectThrow -= ResetGun;
        }

        void SetInitialRefrences()
        {
            gunMaster = GetComponent<Gun_Master>();

            if (GetComponent<Item_Master>())
                itemMaster = GetComponent<Item_Master>();
        }

        void ResetGun()
        {
            gunMaster.CallEventRequsetGunReset(); //in gun_ammo script it is subscribed
        }
    }
}

