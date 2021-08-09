using UnityEngine;
using System.Collections;

namespace Chapter1
{
    public class WalkTroughWall : MonoBehaviour
    {
        private Color myColor = new Color(05f, 1, 0.5f, 0.3f);
        private GameManager_EventMaster eventMasterScript;

        void OnEnable()
        {
            SetInitialRefrences();
            //gameObject with small g means the current GameObject which we are making script for.Here it means the wall
            eventMasterScript.myGeneralEvent += SetLayerToNotSolid;
        }
        void OnDisable()
        {
            eventMasterScript.myGeneralEvent -= SetLayerToNotSolid;
        }

        void SetLayerToNotSolid()
        {
            gameObject.layer = LayerMask.NameToLayer("Not Solid");
            GetComponent<Renderer>().material.SetColor("_Color", myColor);//That is another way of changing color but I prefer the way below!
        }

        void SetInitialRefrences()
        {
            eventMasterScript = GameObject.Find("GameManager").GetComponent<GameManager_EventMaster>();
        }
    }
}

