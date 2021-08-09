using UnityEngine;
using System.Collections;

namespace S3
{
    public class Item_Sounds : MonoBehaviour
    {
        private Item_Master itemMaster;
        public float defaultVolume;
        public AudioClip throwSound; // It's obvious it is for sound!

        void OnEnable()
        {
            SetInitialRefrences();
            itemMaster.EventObjectThrow += PlayThrowSound;
        }

        void OnDisable()
        {
            itemMaster.EventObjectThrow -= PlayThrowSound;
        }

        void SetInitialRefrences()
        {
            itemMaster = GetComponent<Item_Master>();
        }

        void PlayThrowSound()
        {
            if(throwSound != null)
            {
                AudioSource.PlayClipAtPoint(throwSound, transform.position, defaultVolume);
            }
        }
    }
}

