using UnityEngine;
using System.Collections;

namespace Chapter1 //Trigger is usually used when we want an event happen when we enter a particular place
{
    public class TriggerExample : MonoBehaviour
    {
        private GameManager_EventMaster eventMasterScript;

        void Start()
        {
            SetInitialRefrences();
        }

        void OnTriggerEnter(Collider other)
        {
            eventMasterScript.CallMyGeneralEvent();
            Destroy(gameObject);
        }

        void SetInitialRefrences()
        {
            eventMasterScript = GameObject.Find("GameManager").GetComponent<GameManager_EventMaster>();
        }

    }
}

