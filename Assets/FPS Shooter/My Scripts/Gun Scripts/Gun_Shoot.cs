using UnityEngine;
using System.Collections;

namespace S3
{
    public class Gun_Shoot : MonoBehaviour
    {
        private Transform myTransform;
        private Transform camTransform;
        private Gun_Master gunMaster;
        private RaycastHit hit;
        public float range = 400;
        private float offsetFactor = 7;
        private Vector3 startPosition;
        //offsetFactor and startPosition are about a random point origin for the raycast

        void OnEnable()
        {
            SetInitialRefrences();
            gunMaster.EventPlayerInput += OpenFire;
            gunMaster.EventSpeedCaptured += SetStartOfShootingPosition;
        }

        void OnDisable()
        {
            gunMaster.EventPlayerInput -= OpenFire;
            gunMaster.EventSpeedCaptured -= SetStartOfShootingPosition;
        }

        void SetInitialRefrences()
        {
            gunMaster = GetComponent<Gun_Master>();
            myTransform = transform;
            camTransform = myTransform.parent; // the first person character is the parent of the gun
        }

        void OpenFire()
        {
           // Debug.Log("Open Fire Called");
            //TransformPoint transform from a local position into world wide position
            if(Physics.Raycast(camTransform.TransformPoint(startPosition),camTransform.forward,out hit, range))
            {
                gunMaster.CallEventShotDefault(hit.point, hit.transform);

                if (hit.transform.CompareTag(GameManager_Refrences._enemyTag))
                {
                    //Debug.Log("Shot Enemy");
                    gunMaster.CallEventShotEnemy(hit.point, hit.transform);
                }
            }
        }

        void SetStartOfShootingPosition(float playerSpeed)
        {
            //When we are starting to run the position is more random :) 
            //And when we are walking the position is sharper
            float offset = playerSpeed / offsetFactor;
            //we want to produce a radnom value in x and y but exact z
            startPosition = new Vector3(Random.Range(-offset, offset), Random.Range(-offset, offset), 1);
            //We now produced a suitable local position
        }
    }

}

