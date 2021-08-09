using UnityEngine;
using System.Collections;

namespace S3
{
    public class SpawnerProximity : MonoBehaviour
    {
        public GameObject objectToSpawn;
        public int numbersToSpawn;
        public float proximity;
        private float checkRate;
        private float nextCheck;
        private Transform myTransform;
        private Transform playerTransform;
        private Vector3 spawnPosition;

        // Use this for initialization
        void Start()
        {
            SetInitialRefrences();
        }

        // Update is called once per frame
        void Update()
        {
            CheckDistance();
        }

        void SetInitialRefrences()
        {
            myTransform = transform;
            playerTransform = GameManager_Refrences._player.transform;
            checkRate = Random.Range(0.8f, 1.2f);
        }

        void CheckDistance()
        {
            if(Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;
                
                if(Vector3.Distance(myTransform.position,playerTransform.position) < proximity)
                {
                    SpawnObjects();
                    this.enabled = false; // the script would be disabled
                }
            }
        }

        void SpawnObjects()
        {
            for (int i = 0; i < numbersToSpawn; i++)
            {
                spawnPosition = myTransform.position + Random.insideUnitSphere * 5;
                Instantiate(objectToSpawn, spawnPosition, myTransform.rotation);
            }
        }
    }
}

