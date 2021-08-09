using UnityEngine;
using System.Collections;


namespace Chapter1
{
    public class GameManager_EventMaster : MonoBehaviour
    {
        public delegate void GeneralEvent();
        public event GeneralEvent myGeneralEvent;


        public void CallMyGeneralEvent()
        {
            if(myGeneralEvent != null)
            {
                myGeneralEvent();
            }
        }
    }
}

