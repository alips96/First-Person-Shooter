// dar in code aval moshakhaste noghteye barkhord ro mifrestim baara tabe unja collideresh ro moshakhas mikone va ye araye
// az collidera ro por mikone pas dakhele un faghat collider darim. badesh miad be un collider force mide!
using UnityEngine;
using System.Collections;

namespace Chapter1
{
    public class GernadeExplosion : MonoBehaviour
    {
        private Collider[] hitColliders;
        private float destroyTime = 7;
        public float blastRadius;
        public float explosionForce;
        public LayerMask explosionLayers;

        void OnCollisionEnter(Collision col)
        {
            //Debug.Log(col.contacts[0].point.ToString());
            ExplosionWork(col.contacts[0].point);
            Destroy(gameObject); // It destroys the current gameObject which the script is attached to. Here it is the gernade!
        }

        void ExplosionWork(Vector3 explosionPoint) // we should give the difinite point we want to do stuff with it
        {
            hitColliders = Physics.OverlapSphere(explosionPoint, blastRadius, explosionLayers);
            // the line above indicates that the point and the circule with radios 10 is our collider which goes to the array fiiled with colliders
            //overlapsphere is just for colliders! this returns the colliders of that point with a circlude wich means the territory
            foreach (Collider hitCol in hitColliders)
            {
                if(hitCol.GetComponent<UnityEngine.AI.NavMeshAgent>() != null) // if the collider has navmesh agent then we should disable it
                {
                    hitCol.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;// this is how to disable a property
                }

                if(hitCol.GetComponent<Rigidbody>() != null)
                {
                    hitCol.GetComponent<Rigidbody>().isKinematic = false;
                    hitCol.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, explosionPoint, blastRadius, 1, ForceMode.Impulse);
                    
                }

                if (hitCol.CompareTag("Enemy"))
                {
                    Destroy(hitCol.gameObject, destroyTime);
                }

            }
        }
    }
}
