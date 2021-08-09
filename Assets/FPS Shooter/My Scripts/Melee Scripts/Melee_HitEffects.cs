using UnityEngine;
using System.Collections;

namespace S3
{
    public class Melee_HitEffects : MonoBehaviour
    {
        private Melee_Master meleeMaster;
        public GameObject defaultHitEffect;
        public GameObject enemyHitEffect;

        void OnEnable()
        {
            SetInitalRefrences();
            meleeMaster.EventHit += SpawnHitEffect;
        }

        void OnDisable()
        {
            meleeMaster.EventHit -= SpawnHitEffect;
        }

        void SetInitalRefrences()
        {
            meleeMaster = GetComponent<Melee_Master>();
        }

        void SpawnHitEffect(Collision hitCollision,Transform hitTransform)
        {
            Quaternion quatAngle = Quaternion.LookRotation(hitCollision.contacts[0].normal); //the partice would be facing away form the normal face

            if (hitTransform.GetComponent<Enemy_TakeDamage>() != null)
                Instantiate(enemyHitEffect, hitCollision.contacts[0].point, quatAngle);

            else
                Instantiate(defaultHitEffect, hitCollision.contacts[0].point, quatAngle);
        }
    }
}

