using UnityEngine;
using System.Collections;


namespace Chapter1
{
    public class Spawner : MonoBehaviour
    {
        public GameObject objectToSpawn;
        public int numberOfEnemies;
        private float spawnRadius = 5;
        private Vector3 spawnPosition;
        private GameManager_EventMaster eventMasterScript;

        void OnEnable()
        {
            SetInitialRefrences();
            eventMasterScript.myGeneralEvent += SpawnObject;// Everytime event happens this line wolud be executed 
        }

        void OnDisable()
        {
            eventMasterScript.myGeneralEvent -= SpawnObject;
        }


        void SpawnObject()
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius; // that set a random postion aound the 5 units of the object which the script is attached to
                //Vector3 adjustedposition = new Vector3(spawnPosition.x, 1, spawnPosition.z);// preventing from falling into the floor but here we dont need this because it has navemesh agent!
                Instantiate(objectToSpawn, spawnPosition,Quaternion.identity);//quaternion.identity is the default rotation
            }
        }

        void SetInitialRefrences()
        {
            eventMasterScript = GameObject.Find("GameManager").GetComponent<GameManager_EventMaster>();
        }
    }
}

