using UnityEngine;
using System.Collections;

namespace S3
{
    public class Gun_HitEffects : MonoBehaviour
    {
        private Gun_Master gunMaster;
        public GameObject defaultHitEffect;
        public GameObject enemyHitEffect;

        void OnEnable()
        {
            SetInitialRefrences();
            gunMaster.EventShotDefault += SpawnDefaultHitEffects;
            gunMaster.EventShotEnemy += SpawnEnemyHitEffect;
        }

        void OnDisable()
        {
            gunMaster.EventShotDefault -= SpawnDefaultHitEffects;
            gunMaster.EventShotEnemy -= SpawnEnemyHitEffect;
        }

        void SetInitialRefrences()
        {
            gunMaster = GetComponent<Gun_Master>();
        }

        void SpawnDefaultHitEffects(Vector3 hitPosition,Transform hitTransform)
        {
            if (defaultHitEffect != null)
                Instantiate(defaultHitEffect, hitPosition, Quaternion.identity);
        }

        void SpawnEnemyHitEffect(Vector3 hitPosition, Transform hitTransform)
        {
            if (enemyHitEffect != null)
                Instantiate(enemyHitEffect, hitPosition, Quaternion.identity);
        }
    }
}

