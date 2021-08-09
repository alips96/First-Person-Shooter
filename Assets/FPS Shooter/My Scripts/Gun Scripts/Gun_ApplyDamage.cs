using UnityEngine;
using System.Collections;

namespace S3
{
    public class Gun_ApplyDamage : MonoBehaviour
    {
        private Gun_Master gunMaster;
        public int damage = 10;

        void OnEnable()
        {
            SetInitialRefrences();
            gunMaster.EventShotEnemy += ApplyDamage;
            gunMaster.EventShotDefault += ApplyDamage;
        }

        void OnDisable()
        {
            gunMaster.EventShotEnemy -= ApplyDamage;
            gunMaster.EventShotDefault -= ApplyDamage;
        }

        void SetInitialRefrences()
        {
            gunMaster = GetComponent<Gun_Master>();
        }

        void ApplyDamage(Vector3 hitPosition,Transform hitTransform)
        {
            //if(hitTransform.GetComponent<Enemy_TakeDamage>() != null)
            //{
            //    hitTransform.GetComponent<Enemy_TakeDamage>().ProcessDamage(damage);
            //}
            hitTransform.SendMessage("ProcessDamage", damage, SendMessageOptions.DontRequireReceiver);
            //SendMessageOptions.DontRequireReceiver doesn't need a script to be attached to
        }
    }
}

