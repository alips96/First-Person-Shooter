using UnityEngine;
using System.Collections;

namespace S3
{
    public class Melee_Strike : MonoBehaviour
    {
        private Melee_Master meleeMaster;
        private float nextSwingTime;
        public int damage = 25;

        // Use this for initialization
        void Start()
        {
            SetInitialRefrences();
        }

        void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject != GameManager_Refrences._player
                && meleeMaster.isInUse &&
                Time.time > nextSwingTime)
            {
                //we put nextSwingTime to avoid hitting a lot of colliders in a short time!
                nextSwingTime = Time.time + meleeMaster.swingRate;
                collision.transform.SendMessage("ProcessDamage", damage, SendMessageOptions.DontRequireReceiver);
                meleeMaster.CallEventHit(collision, collision.transform); //For the particle system 
            }
        }

        void SetInitialRefrences()
        {
            meleeMaster = GetComponent<Melee_Master>();
        }
    }
}

