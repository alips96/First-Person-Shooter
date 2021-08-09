using UnityEngine;
using System.Collections;

namespace Chapter1
{
    public class Shoot : MonoBehaviour
    {

        private float fireRate = 0.3f;
        private float nextFire = 0f;
        private RaycastHit hit;
        private float range = 300;
        private Transform myTransform;

        // Use this for initialization
        void Start()
        {
            setInitialRefrences();
        }

        // Update is called once per frame
        void Update()
        {
            CheckForInput();
        }

        void setInitialRefrences()
        {
            myTransform = transform;
        }
        void CheckForInput()
        {
            /*if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire )
            {
                nextFire = Time.time + fireRate;
                Debug.Log("key pressed");
            }*/
            if (Input.GetButton("Fire1") && Time.time > nextFire)
            {
                Debug.DrawRay(myTransform.TransformPoint(0, 0, 1), myTransform.forward, Color.green, 3);
                if (Physics.Raycast(myTransform.TransformPoint(0, 0, 1), myTransform.forward, out hit, range))
                {
                    //if(hit.transform.tag == "Enemy")
                    if (hit.transform.CompareTag("Enemy"))
                        Debug.Log("Enemy " + hit.transform.name);
                    else
                        Debug.Log("Not an Enemy");
                }
                nextFire = Time.time + fireRate;
                //Debug.Log("key pressed");
            }
        }
    }
}
