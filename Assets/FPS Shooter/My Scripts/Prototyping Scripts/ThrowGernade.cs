using UnityEngine;
using System.Collections;

namespace Chapter1
{
    public class ThrowGernade : MonoBehaviour
    {
        public GameObject gernadePrefab;
        private Transform myTransform;
        public float propolsionForce;

        // Use this for initialization
        void Start()
        {
            SetInitialRefrences();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                SpawnGernade();
            }
        }

        void SetInitialRefrences()
        {
            myTransform = transform;
        }
        void SpawnGernade()
        {  
            GameObject go = (GameObject) Instantiate(gernadePrefab, myTransform.TransformPoint(0, 0, 0.5f), myTransform.rotation);//my tansform.rotation means the default rotation of myself
            go.GetComponent<Rigidbody>().AddForce(myTransform.forward * propolsionForce, ForceMode.Impulse); // adding force
            Destroy(go, 10);

        }
    }

}

