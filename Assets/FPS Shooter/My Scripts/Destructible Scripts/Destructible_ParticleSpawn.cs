using UnityEngine;
using System.Collections;

namespace S3
{
    public class Destructible_ParticleSpawn : MonoBehaviour
    {
        private Destructible_Master destructibleMaster;
        public GameObject explosionEffect;

        void OnEnable()
        {
            SetInitialRefrences();
            destructibleMaster.EventDestroyMe += SpawnExplosion;
        }

        void OnDisable()
        {
            destructibleMaster.EventDestroyMe -= SpawnExplosion;
        }

        void SetInitialRefrences()
        {
            destructibleMaster = GetComponent<Destructible_Master>();
        }

        void SpawnExplosion()
        {
            if(explosionEffect != null)
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
    }
}

